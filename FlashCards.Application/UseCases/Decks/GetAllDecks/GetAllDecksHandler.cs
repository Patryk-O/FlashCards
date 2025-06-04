using FlashCards.Application.Abstractions;
using FlashCards.Application.Common;
using FlashCards.Domain.Entities;
using FlashCards.Domain.Repositories;

namespace FlashCards.Application.UseCases.Decks.GetAllDecks;

public class GetAllDecksHandler(IDeckRepository deckRepository)
    : IQueryHandler<GetAllDecksQuery, IReadOnlyCollection<Deck>>
{
    public async Task<UseCaseResult<IReadOnlyCollection<Deck>>> Handle(GetAllDecksQuery query)
    {
        var result = await deckRepository.GetAllAsync();
        if (result.Count == 0)
            return UseCaseResult<IReadOnlyCollection<Deck>>.Fail(ErrorCode.NotFound, "Decks not found");
        
        return UseCaseResult<IReadOnlyCollection<Deck>>.Ok(result);
    }
}