using api_do_an_cnpm.Dtos.HouseDTO;
using api_do_an_cnpm.Interfaces;


using api_do_an_cnpm.Models;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_do_an_cnpm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HouseController : ControllerBase
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public HouseController(IHouseRepository houseRepository, IMapper mapper, IFacilityRepository facilityRepository, ITokenService tokenService)
        {
            _houseRepository = houseRepository;
            _mapper = mapper;
            _facilityRepository = facilityRepository;
            _tokenService = tokenService;
        }

        [Authorize(Roles = "OwnerHouse")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = _tokenService.GetUserId();
            if (string.IsNullOrEmpty(userId)) return ResponseHelper.Error(this, "Không tìm thấy user", 0);

            var houses = await _houseRepository.GetAll()
                .Include(q => q.Facilities)
                .Include(q => q.Owner)
                .Where(q => q.OwnerId == userId)

                .ToListAsync(); // Execute the query asynchronously

            var houseDtos = _mapper.Map<IEnumerable<HouseDTO>>(houses);

            return ResponseHelper.Success(this, houseDtos, "Lấy danh sách thành công", Enums.EnumActionApi.Get);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await _mapper.ProjectTo<HouseDTO>(_houseRepository.GetAll().Include(q => q.Facilities).Where(q => q.Id == id)).FirstOrDefaultAsync();
            if (house == null)
            {
                return NotFound();
            }
            return ResponseHelper.Success(this, house, "Lấy dữ liệu thành công", Enums.EnumActionApi.Get);
        }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] HouseDTO houseDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     var house = _mapper.Map<House>(houseDto);
        //     await _houseRepository.AddAsync(house);
        //     await _houseRepository.SaveAllAsync();
        //     return CreatedAtAction(nameof(GetById), new { id = house.Id }, houseDto);
        // }
        [HttpPost]
        [Authorize(Roles = "OwnerHouse")]
        public async Task<IActionResult> Create([FromBody] HouseDTO houseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var house = _mapper.Map<House>(houseDto);
            house.DateCreate = DateTime.UtcNow;
            await _houseRepository.AddAsync(house);
            await _houseRepository.SaveAllAsync();

            return ResponseHelper.Success(this, houseDto, "Tạo mới thành công", Enums.EnumActionApi.Post);
        }

        [HttpPost("Accept/{Id}")]
        public async Task<IActionResult> Accept(int Id)
        {
            try
            {
                var model = await _houseRepository.GetByIdAsync(Id);
                if (model == null) return ResponseHelper.Error(this, "Không tìm thấy ngôi nhà", 400);
                model.IsAccept = !model.IsAccept;
                await _houseRepository.UpdateAsync(model);
                await _houseRepository.SaveAllAsync();
                return ResponseHelper.Success(this, $"Đã {(model.IsAccept ? "Đã duyệt thành công" : "Đã hủy bỏ duyệt thành công")} ngôi nhà");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, "Đã có lỗi xảy ra");
            }
        }

        [HttpPost("Block/{Id}")]
        public async Task<IActionResult> Block(int Id)
        {
            try
            {
                var model = await _houseRepository.GetByIdAsync(Id);
                if (model == null) return ResponseHelper.Error(this, "Không tìm thấy ngôi nhà", 400);
                model.IsBlock = !model.IsBlock;
                await _houseRepository.UpdateAsync(model);
                await _houseRepository.SaveAllAsync();
                return ResponseHelper.Success(this, $"Đã {(model.IsAccept ? "Đã từ chối thành công" : "Đã bỏ từ chối thành công")} ngôi nhà");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Error(this, "Đã có lỗi xảy ra");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HouseDTO houseDto)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }
            var checkModelState = ResponseHelper.CheckModelStateAndReturnError(this);
            if (checkModelState != null) //? Không hợp lệ
            {
                return checkModelState;
            }
            var house = await _houseRepository.GetByIdAsync(id);
            if (house == null)
            {
                // return NotFound();
                return ResponseHelper.Error(this, "Không tìm thấy nhà", 404);
            }
            _mapper.Map(houseDto, house);
            // var oldFacility = (house.Facilities ?? []).Where(q => q.Id != 0);
            // var lstIdFacility = oldFacility.Select(q => q.Id);
            // var allFacility = await _facilityRepository.GetAllAsync();
            // var checkExist = allFacility.Where(q => lstIdFacility.Contains(q.Id)).ToList();
            // var checkNotExist = allFacility.Where(q => !lstIdFacility.Contains(q.Id)).ToList();
            // if(checkExist.Count > 0)
            // {
            //     await _facilityRepository.UpdateAsync(checkExist);
            // }
            // if(checkNotExist.Count > 0)
            // {
            //     checkNotExist.Select(q =>
            //     {
            //         q.HouseId = house.Id;
            //         return q;
            //     }).ToList();
            //     await _facilityRepository.AddAsync(checkNotExist);
            // }
            // house.Facilities = null;
            await _houseRepository.UpdateAsync(house);
            await _houseRepository.SaveAllAsync();
            // return NoContent();
            return ResponseHelper.Success<object>(this, null, "Cập nhật thành công", Enums.EnumActionApi.Update);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _houseRepository.DeleteAsync(id);
            await _houseRepository.SaveAllAsync();
            // return NoContent();
            return ResponseHelper.Success<object>(this, null, "Xóa thành công", Enums.EnumActionApi.Delete);
        }
        [HttpDelete("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            foreach (var id in ids)
            {
                await _houseRepository.DeleteAsync(id);
            }
            await _houseRepository.SaveAllAsync();
            return ResponseHelper.Success<object>(this, null, "Xóa các nhà thành công", Enums.EnumActionApi.Delete);
        }
        [HttpGet("User/GetAll/House")]
        public async Task<IActionResult> UserGetAll()
        {
            var houses = await _houseRepository.GetAllAsync();
            var houseDtos = _mapper.Map<IEnumerable<HouseDTO>>(houses);
            // return Ok(houseDtos);

            return ResponseHelper.Success(this, houseDtos, "Lấy danh sách thành công", Enums.EnumActionApi.Get);
        }
    }
}