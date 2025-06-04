using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Application.UseCases.Cards.CreateCard;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Cards.RemoveCard;

public class RemoveCardInDeckHandler(IDeckRepository deckRepository, ICardRepository cardRepository)
    : ICommandHandler<RemoveCardCommand, Guid>
{
    private readonly IDeckRepository _deckRepository = deckRepository;
    private readonly ICardRepository _cardRepository = cardRepository;

    public async Task<UseCaseResult<Guid>> Handle(RemoveCardCommand? command)
    {
        if(command is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Command is null");
        if (command.CardId == Guid.Empty)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation,"CardId is required");
        
        var card = await _cardRepository.GetCardByIdAsync(command.CardId);
        if (card is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Card not found");
        var deck = await _deckRepository.GetAsync(card.DeckId);
        if (deck is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Deck not found");
        
        await _deckRepository.RemoveCardFromDeckAsync(deck, card);
        await _cardRepository.RemoveCardAsync(card);
        return UseCaseResult<Guid>.Ok(card.Id);
    }
}