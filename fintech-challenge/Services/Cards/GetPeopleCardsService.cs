using FintechChallenge.Exceptions;
using FintechChallenge.Helpers;
using FintechChallenge.Models;
using FintechChallenge.Repositories;

namespace FintechChallenge.Services
{
    public class GetPeopleCardsService : IGetPeopleCardsService
    {
        private readonly ICardsRepository _cardsRepository;
        private readonly IPeopleRepository _peopleRepository;

        public GetPeopleCardsService(IPeopleRepository peopleRepository, ICardsRepository cardsRepository)
        {
            _peopleRepository = peopleRepository;
            _cardsRepository = cardsRepository;
        }

        public async Task<PaginatedResult<CreateCardResponse>> GetPeopleCards(Guid peopleId, int take, int skip)
        {
            var people = await _peopleRepository.GetPeopleById(peopleId);

            if (people == null)
            {
                throw new NotFoundException("People not found");
            }

            var peopleCards = await _cardsRepository.GetCardsByPeopleId(peopleId);

            int currentPage = (skip / take) + 1;

            var pagedCards = peopleCards
            .Skip(skip)
            .Take(take)
            .ToList();

            var paginationInfo = new PaginationInfo
            {
                ItemsPerPage = take,
                CurrentPage = currentPage
            };

            var formattedCards = pagedCards.Select(card => new CreateCardResponse(
                Id: card.Id,
                Type: card.Type.ToString().ToLower(),
                Number: card.CardNumber.Substring(card.CardNumber.Length - 4),
                Cvv: card.Cvv,
                CreatedAt: card.CreatedAt,
                UpdatedAt: card.UpdatedAt
                )).ToList();


            var result = new PaginatedResult<CreateCardResponse>("cards", formattedCards, paginationInfo);

            return result;
        }
    }
}