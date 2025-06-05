using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Application.UseCases.Decks.GetAllDecks;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Decks.GetDeckByID;

public class GetDeckByIdHandler(IDeckRepository deckRepository)
    : IQueryHandler<GetDeckByIdQuery, Deck>
{
    public async Task<UseCaseResult<Deck>> Handle(GetDeckByIdQuery? query)
    {
        var result = await deckRepository.GetAsync(query.deckId);
        if (result  is null)
            return UseCaseResult<Deck>.Fail(ErrorCode.NotFound, "Deck not found");
        
        return UseCaseResult<Deck>.Ok(result);
    }
}