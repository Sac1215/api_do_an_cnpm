using System.Text.Json.Serialization;
using api_do_an_cnpm.Enums;
using api_do_an_cnpm.Responses;
using Microsoft.AspNetCore.Mvc;

public static class ResponseHelper
{
    public static IActionResult? CheckModelStateAndReturnError(ControllerBase controller)
    {
        if (!controller.ModelState.IsValid)
        {
            var firstError = controller.ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage)
                                .FirstOrDefault();

            var errorMessage = firstError ?? "Đã có lỗi xảy ra vui lòng thử lại sau";
            var badResponse = new BaseResponse<object>(null, errorMessage, 400);
            return controller.BadRequest(badResponse);
        }

        return null;
    }

    public static IActionResult PaginatedSuccess<T>(
        ControllerBase controller,
        IEnumerable<T> data,
        int pageNumber,
        int pageSize,
        int totalItems,
        string message = "Thành công")
    {
        var paginatedResponse = new PaginatedResponse<T>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems,
            Message = message,
            StatusCode = 200,
            Code = 200
        };

        return controller.Ok(paginatedResponse);
    }

    public static IActionResult Success<T>(ControllerBase controller, T? data, string message = "Thành công", EnumActionApi action = EnumActionApi.Get)

    {

        var response = new BaseResponse<T>(data, message, (int)action);
        return controller.Ok(response);
    }
    public static IActionResult Error(ControllerBase controller, string message, int code = 500)
    {
        var errorResponse = new BaseResponse<object>(null, message, code);
        return controller.StatusCode(code, errorResponse);
    }

    // public static IActionResult ErrorElseValid(ControllerBase controller, string message, int code)
    // {
    //     var response = new BaseResponse<object>(null, message, code);
    //     return controller.StatusCode(code, response);
    // }
    public class PaginatedResponse<T>
    {
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage => PageNumber < TotalPages;

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class PageRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
