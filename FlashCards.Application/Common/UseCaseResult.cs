using FlashCards.Domain.Entities;

namespace FlashCards.Application.Common;
public record UseCaseError(ErrorCode code, string message);
public class UseCaseResult<T>
{
    public bool Success { get; init; }
    public T? Value { get; init; }
    public UseCaseError? Error { get; init; }
    
    public static UseCaseResult<T> Ok(T value) => new() {Success = true, Value = value};
    public static UseCaseResult<T> Fail(ErrorCode code, string message) => new() { Success = false, Error = new UseCaseError(code, message)};
}