using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;
using FlashCards.Domain.ValueObject;

namespace FlashCards.Application.UseCases.Decks.CreateDeck;

public class CreateDeckHandler : ICommandHandler<CreateDeckCommand, Guid>
{
    private readonly IDeckRepository _repository;
    public CreateDeckHandler(IDeckRepository repository)
    {
        _repository = repository;
    }

    public async Task<UseCaseResult<Guid>> Handle(CreateDeckCommand command)
    {
        if(command is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Command is null");
        if(command.Title is null)
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Title is null");
        if(string.IsNullOrWhiteSpace((command.Title)))
            return UseCaseResult<Guid>.Fail(ErrorCode.Validation, "Title is empty");
        
        var deck = Deck.CreateNewDeck(command.Title);
        await _repository.AddDeckAsync(deck);
        return UseCaseResult<Guid>.Ok(deck.Id);
    }
}