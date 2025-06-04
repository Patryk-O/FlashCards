using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Cards.GetCardById;

public class GetCardByIdHandler(ICardRepository cardRepository)
    : IQueryHandler<GetCardByIdQuery, Card>
{
    private readonly ICardRepository _cardRepository = cardRepository;

    public async Task<UseCaseResult<Card>> Handle(GetCardByIdQuery? query)
    {
        if(query is null)
            return UseCaseResult<Card>.Fail(ErrorCode.Validation, "Command is null");
        if (query.CardId == Guid.Empty)
            return UseCaseResult<Card>.Fail(ErrorCode.Validation,"CardId is required");
        
        var card = await _cardRepository.GetCard(query.CardId);
        return card is null ? UseCaseResult<Card>.Fail(ErrorCode.Validation, "Card not found") : UseCaseResult<Card>.Ok(card);
    }
}