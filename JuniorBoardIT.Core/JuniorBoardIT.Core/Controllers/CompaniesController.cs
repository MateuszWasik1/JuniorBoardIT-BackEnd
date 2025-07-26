using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Recruiter, Support, Admin")]
    public class CompaniesController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public CompaniesController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetCompanies")]
        public GetCompaniesViewModel GetCompanies(int skip, int take)
            => dispatcher.DispatchQuery<GetAllCompaniesQuery, GetAllCompaniesViewModel>(new GetAllCompaniesQuery() { Skip = skip, Take = take });

        [HttpGet]
        [Route("GetComapany")]
        public JobOfferViewModel GetComapany()
            => dispatcher.DispatchQuery<GetJobOfferQuery, JobOfferViewModel>(new GetJobOfferQuery());

        [HttpPost]
        [Route("AddCompany")]
        public void AddCompany(JobOfferViewModel model)
            => dispatcher.DispatchCommand(new AddJobOfferCommand() { Model = model });

        [HttpPut]
        [Route("UpdateCompany")]
        public void UpdateCompany(JobOfferViewModel model)
            => dispatcher.DispatchCommand(new UpdateJobOfferCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteCompany/{cgid}")]
        public void DeleteCompany(Guid jogid)
            => dispatcher.DispatchCommand(new DeleteJobOfferCommand() { JOGID = jogid });
    }
}