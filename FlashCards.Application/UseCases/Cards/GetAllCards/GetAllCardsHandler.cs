using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Cards.GetAllCards;

public class GetAllCardsHandler(ICardRepository cardRepository)
    : IQueryHandler<GetAllCardsQuery, IReadOnlyCollection<Card>>
{
    private readonly ICardRepository _cardRepository = cardRepository;

    public async Task<UseCaseResult<IReadOnlyCollection<Card>>> Handle(GetAllCardsQuery query)
    {
        var cards = await _cardRepository.GetAllCardsAsync();
        if (cards is null)
            return UseCaseResult<IReadOnlyCollection<Card>>.Fail(ErrorCode.NotFound, "Cards not found");
        return UseCaseResult<IReadOnlyCollection<Card>>.Ok(cards);
    }
}