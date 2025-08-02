using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteJobOffersController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public FavoriteJobOffersController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpPost]
        [Route("AddFavoriteJobOffer/{jogid}")]
        public void AddFavoriteJobOffer(Guid jogid)
            => dispatcher.DispatchCommand(new AddFavoriteJobOfferCommand() { JOGID = jogid });

        [HttpDelete]
        [Route("DeleteFavoriteJobOffer/{fjogid}")]
        public void DeleteFavoriteJobOffer(Guid fjogid)
            => dispatcher.DispatchCommand(new DeleteFavoriteJobOfferCommand() { FJOGID = fjogid });
    }
}