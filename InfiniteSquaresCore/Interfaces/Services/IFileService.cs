using InfiniteSquaresCore.Responses;

namespace InfiniteSquaresCore.Interfaces.Services;

public interface IFileService
{
    Task<ResponseResult> SaveFileAsync<T>(string filepath, T data);
    Task<ResponseResult> ReadFromFileAsync<T>(string filepath);
    Task<ResponseResult> DeleteFileAsync(string filepath);
}
