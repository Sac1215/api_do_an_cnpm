using System.Collections.Generic;
using System.Threading.Tasks;
using api_do_an_cnpm.Models;

namespace api_do_an_cnpm.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<AppUser?> GetByIdAsync(string id);
        Task<AppUser?> GetByUserNameAsync(string userName);
        Task AddAsync(AppUser appUser);
        Task UpdateAsync(AppUser appUser);
        Task DeleteAsync(string id);
        Task<bool> SaveAllAsync();
    }
}

//     /*
//     /?!
// Các đặc điểm chính của IEnumerable<T>:
// Duyệt qua các phần tử: IEnumerable<T> cho phép bạn duyệt qua các phần tử của một tập hợp bằng cách sử dụng vòng lặp foreach.
// Không hỗ trợ truy cập ngẫu nhiên: IEnumerable<T> không hỗ trợ truy cập ngẫu nhiên đến các phần tử bằng chỉ số (index).
// Chỉ đọc: IEnumerable<T> chỉ cung cấp khả năng đọc các phần tử, không cho phép thêm, xóa hoặc sửa đổi các phần tử trong tập hợp
//     */

//     /* Tóm tắt
// IEnumerable<T>: Đại diện cho một tập hợp các đối tượng có thể được duyệt qua.
// Duyệt qua các phần tử: Sử dụng vòng lặp foreach để duyệt qua các phần tử của tập hợp.
// Chỉ đọc: Chỉ cung cấp khả năng đọc các phần tử, không cho phép thêm, xóa hoặc sửa đổi các phần tử trong tập hợ
//     */
// }