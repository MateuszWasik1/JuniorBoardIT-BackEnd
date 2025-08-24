using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.FavoriteJobOffers.Commands;
using JuniorBoardIT.Core.Models.ViewModels.FavoriteJobOffersViewModel;
using Microsoft.AspNetCore.Authorization;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteJobOffersController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public FavoriteJobOffersController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpPost]
        [Route("AddFavoriteJobOffer")]
        public void AddFavoriteJobOffer(AddFavoriteJobOfferViewModel model)
            => dispatcher.DispatchCommand(new AddFavoriteJobOfferCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteFavoriteJobOffer/{fjogid}")]
        public void DeleteFavoriteJobOffer(Guid fjogid)
            => dispatcher.DispatchCommand(new DeleteFavoriteJobOfferCommand() { FJOGID = fjogid });
    }
}