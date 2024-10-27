using InfiniteSquaresCore.Responses;

namespace InfiniteSquaresCore.Interfaces.Services;

public interface IFileService
{
    Task<ResponseResult> SaveFileAsync<T>(string filePath, T data);
    Task<ResponseResult> ReadFromFileAsync<T>(string filePath);
    Task<ResponseResult> DeleteFileAsync(string filePath);
}
