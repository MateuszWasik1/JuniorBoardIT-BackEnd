using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Models.Enums.JobOffers;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOffersController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public JobOffersController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetAllJobOffers")]
        public GetAllJobOffersViewModel GetAllJobOffers(int skip, int take, EducationEnum education, bool favorites)
            => dispatcher.DispatchQuery<GetAllJobOffersQuery, GetAllJobOffersViewModel>(new GetAllJobOffersQuery() { Skip = skip, Take = take, Education = education, Favorites = favorites });

        [HttpGet]
        [Route("GetJobOffer")]
        public GetJobOfferViewModel GetJobOffer(Guid jogid)
            => dispatcher.DispatchQuery<GetJobOfferQuery, GetJobOfferViewModel>(new GetJobOfferQuery() { JOGID = jogid });

        [HttpPost]
        [Route("AddJobOffer")]
        [Authorize(Roles = "Recruiter, Support, Admin")]
        public void AddJobOffer(JobOfferViewModel model)
            => dispatcher.DispatchCommand(new AddJobOfferCommand() { Model = model });

        [HttpPut]
        [Route("UpdateJobOffer")]
        [Authorize(Roles = "Recruiter, Support, Admin")]
        public void UpdateJobOffer(JobOfferViewModel model)
            => dispatcher.DispatchCommand(new UpdateJobOfferCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteJobOffer/{jogid}")]
        [Authorize(Roles = "Recruiter, Support, Admin")]
        public void DeleteJobOffer(Guid jogid)
            => dispatcher.DispatchCommand(new DeleteJobOfferCommand() { JOGID = jogid });
    }
}