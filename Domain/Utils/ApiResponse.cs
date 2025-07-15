﻿namespace WeCare.Domain.Utils
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public ApiResponse(T? data, bool success = true, string? message = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public ApiResponse(bool success = false, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
