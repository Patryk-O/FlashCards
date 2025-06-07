using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Decks.RemoveDeck;

public class RemoveDeckHandler(IDeckRepository repository) :  ICommandHandler<RemoveDeckCommand, Guid>
{
    public async Task<UseCaseResult<Guid>> Handle(RemoveDeckCommand? command)
    {
        if(command is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Command is null");
        if (command.DeckId == Guid.Empty)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation,"Deck is required");
        var deck = await repository.GetAsync(command.DeckId);
        if(deck is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.NotFound, "Deck not found");
        
        await repository.RemoveDeckAsync(deck);
        return UseCaseResult<Guid>.Ok(deck.Id);
    }
}