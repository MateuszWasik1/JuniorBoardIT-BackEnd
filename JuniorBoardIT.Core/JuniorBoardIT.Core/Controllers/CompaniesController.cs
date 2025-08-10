using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;

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
            => dispatcher.DispatchQuery<GetCompaniesQuery, GetCompaniesViewModel>(new GetCompaniesQuery() { Skip = skip, Take = take });

        [HttpGet]
        [Route("GetCompany")]
        public CompanyViewModel GetCompany(Guid cgid)
            => dispatcher.DispatchQuery<GetCompanyQuery, CompanyViewModel>(new GetCompanyQuery() { CGID = cgid });

        [HttpGet]
        [Route("GetCompaniesForUser")]
        public GetCompaniesForUserViewModel GetCompaniesForUser()
            => dispatcher.DispatchQuery<GetCompaniesForUserQuery, GetCompaniesForUserViewModel>(new GetCompaniesForUserQuery() );

        [HttpPost]
        [Route("AddCompany")]
        public void AddCompany(AddCompanyViewModel model)
            => dispatcher.DispatchCommand(new AddCompanyCommand() { Model = model });

        [HttpPut]
        [Route("UpdateCompany")]
        public void UpdateCompany(CompanyViewModel model)
            => dispatcher.DispatchCommand(new UpdateCompanyCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteCompany/{cgid}")]
        public void DeleteCompany(Guid cgid)
            => dispatcher.DispatchCommand(new DeleteCompanyCommand() { CGID = cgid });
    }
}