using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_do_an_cnpm.Dtos;
using api_do_an_cnpm.Interfaces;
using api_do_an_cnpm.Models.api_do_an_cnpm.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api_do_an_cnpm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMapper _mapper;

        public FacilityController(IFacilityRepository facilityRepository, IMapper mapper)
        {
            _facilityRepository = facilityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var facilities = await _facilityRepository.GetAllAsync();
            var facilityDtos = _mapper.Map<IEnumerable<FacilityDTO>>(facilities);
            // return Ok(facilityDtos);
            return ResponseHelper.Success(this, facilityDtos, "Lấy danh sách thành công", Enums.EnumActionApi.Get);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var facility = await _facilityRepository.GetByIdAsync(id);
            if (facility == null)
            {
                return ResponseHelper.Error(this, "Không tìm thấy", 404);
            }
            var facilityDto = _mapper.Map<FacilityDTO>(facility);
            // return Ok(facilityDto);
            return ResponseHelper.Success(this, facilityDto, "Lấy dữ liệu thành công", Enums.EnumActionApi.Get);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacilityDTO facilityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var facility = _mapper.Map<Facility>(facilityDto);
            await _facilityRepository.AddAsync(facility);
            await _facilityRepository.SaveAllAsync();
            // return CreatedAtAction(nameof(GetById), new { id = facility.Id }, facilityDto);
            return ResponseHelper.Success(this, facilityDto, "Tạo mới thành công", Enums.EnumActionApi.Post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FacilityDTO facilityDto)
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
            var facility = await _facilityRepository.GetByIdAsync(id);
            if (facility == null)
            {
                return ResponseHelper.Error(this, "Không tìm thấy cơ sở", 404);
            }
            _mapper.Map(facilityDto, facility);
            await _facilityRepository.UpdateAsync(facility);
            await _facilityRepository.SaveAllAsync();
            // return NoContent();
            return ResponseHelper.Success<object>(this, null, "Cập nhật thành công", Enums.EnumActionApi.Update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _facilityRepository.DeleteAsync(id);
            await _facilityRepository.SaveAllAsync();
            // return NoContent();
            return ResponseHelper.Success<object>(this, null, "Xóa thành công", Enums.EnumActionApi.Delete);
        }

        [HttpDelete("DeleteMultiple")]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            foreach (var id in ids)
            {
                await _facilityRepository.DeleteAsync(id);
            }
            await _facilityRepository.SaveAllAsync();
            return ResponseHelper.Success<object>(this, null, "Xóa các cơ sở thành công", Enums.EnumActionApi.Delete);
        }
    }
}