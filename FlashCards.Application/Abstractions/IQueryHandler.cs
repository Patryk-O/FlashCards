using FlashCards.Application.Common;

namespace FlashCards.Application.Abstractions;

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<UseCaseResult<TResult>> Handle(TQuery query);
}