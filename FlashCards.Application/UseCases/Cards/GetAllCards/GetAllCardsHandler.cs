using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Cards.GetAllCards;

public class GetAllCardsHandler(ICardRepository cardRepository)
    : IQueryHandler<GetAllCardsQuery, IEnumerable<Card>>
{
    private readonly ICardRepository _cardRepository = cardRepository;

    public async Task<UseCaseResult<IEnumerable<Card>>> Handle(GetAllCardsQuery query)
    {
        var cards = await _cardRepository.GetAllCards();
        if (cards is null)
            return UseCaseResult<IEnumerable<Card>>.Fail(ErrorCode.NotFound, "Cards not found");
        return UseCaseResult<IEnumerable<Card>>.Ok(cards);
    }
}