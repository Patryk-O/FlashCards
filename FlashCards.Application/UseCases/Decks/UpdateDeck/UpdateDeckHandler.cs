using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Application.UseCases.Cards.UpdateCard;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Decks.UpdateDeck;

public class UpdateDeckHandler(IDeckRepository deckRepository)
    : ICommandHandler<UpdateDeckCommand, Guid>
{
    private readonly IDeckRepository _deckRepository = deckRepository;

    public async Task<UseCaseResult<Guid>> Handle(UpdateDeckCommand? command)
    {
        if(command is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Command is null");
        if (command.DeckId == Guid.Empty)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation,"Deck is required");
        
        var deck = await _deckRepository.GetAsync(command.DeckId);
        if(deck is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Deck not found");
        
        var updatedDeck = Deck.CreateNewDeck(command.title);

        await _deckRepository.UpdateAsync(command.DeckId, updatedDeck);
        return UseCaseResult<Guid>.Ok(command.DeckId);
    }
}