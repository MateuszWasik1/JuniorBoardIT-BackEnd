using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Commands;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobOffersController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public JobOffersController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetAllJobOffers")]
        [Authorize(Roles = "Admin")]
        public GetAllJobOffersViewModel GetAllJobOffers(int skip, int take)
            => dispatcher.DispatchQuery<GetAllJobOffersQuery, GetAllJobOffersViewModel>(new GetAllJobOffersQuery() { Skip = skip, Take = take });

        [HttpGet]
        [Route("GetJobOffer")]
        [Authorize]
        public JobOfferViewModel GetJobOffer()
            => dispatcher.DispatchQuery<GetJobOfferQuery, JobOfferViewModel>(new GetJobOfferQuery());

        [HttpPost]
        [Route("AddJobOffer")]
        [Authorize(Roles = "Recruiter")]
        public void AddJobOffer(JobOfferViewModel model)
            => dispatcher.DispatchCommand(new AddJobOfferCommand() { Model = model });

        [HttpPut]
        [Route("UpdateJobOffer")]
        [Authorize(Roles = "Recruiter")]
        public void UpdateJobOffer(JobOfferViewModel model)
            => dispatcher.DispatchCommand(new UpdateJobOfferCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteJobOffer/{jogid}")]
        [Authorize(Roles = "Recruiter")]
        public void DeleteJobOffer(Guid jogid)
            => dispatcher.DispatchCommand(new DeleteJobOfferCommand() { JOGID = jogid });
    }
}