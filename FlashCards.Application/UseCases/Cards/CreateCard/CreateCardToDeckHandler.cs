using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Cards.CreateCard;

public class CreateCardToDeckHandler(IDeckRepository deckRepository, ICardRepository cardRepository)
    : ICommandHandler<CreateCardToDeckCommand, Guid>
{
    private readonly IDeckRepository _deckRepository = deckRepository;
    private readonly ICardRepository _cardRepository = cardRepository;

    public async Task<UseCaseResult<Guid>> Handle(CreateCardToDeckCommand? command)
    {
        if(command is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Command is null");
        if (command.DeckId == Guid.Empty)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation,"Deck is required");
        if (command.Question is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Question is required");
        if (command.Answer is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Answer is required");
        
        
        var deck = await _deckRepository.GetAsync(command.DeckId);
        if(deck is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Deck not found");
        
        var card = Card.CreateNewCard(deck.Id, command.Question, command.Answer);
        await _cardRepository.AddCardAsync(card);
        return UseCaseResult<Guid>.Ok(card.Id);
    }
}