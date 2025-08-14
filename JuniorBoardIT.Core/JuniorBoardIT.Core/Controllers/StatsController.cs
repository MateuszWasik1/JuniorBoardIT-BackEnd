using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Stats.Queries;
using JuniorBoardIT.Core.Models.ViewModels.StatsViewModels;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public StatsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetNumberOfRecruiterPublishedOfferts")]
        public StatsBarChartViewModel GetNumberOfRecruiterPublishedOfferts(DateTime startDate, DateTime endDate)
            => dispatcher.DispatchQuery<GetNumberOfRecruiterPublishedOffertsQuery, StatsBarChartViewModel>(new GetNumberOfRecruiterPublishedOffertsQuery() { StartDate = startDate, EndDate = endDate });

        [HttpGet]
        [Route("GetNumberOfCompanyPublishedOfferts")]
        public StatsBarChartViewModel GetNumberOfCompanyPublishedOfferts(DateTime startDate, DateTime endDate, Guid cgid)
            => dispatcher.DispatchQuery<GetNumberOfCompanyPublishedOffertsQuery, StatsBarChartViewModel>(new GetNumberOfCompanyPublishedOffertsQuery() { StartDate = startDate, EndDate = endDate, CGID = cgid });

        [HttpGet]
        [Route("GetNumberOfCompaniesPublishedOfferts")]
        public StatsBarChartViewModel GetNumberOfCompaniesPublishedOfferts(DateTime startDate, DateTime endDate)
            => dispatcher.DispatchQuery<GetNumberOfCompaniesPublishedOffertsQuery, StatsBarChartViewModel>(new GetNumberOfCompaniesPublishedOffertsQuery() { StartDate = startDate, EndDate = endDate });

        [HttpGet]
        [Route("GetNumberOfActiveCompaniesOfferts")]
        public StatsBarChartViewModel GetNumberOfActiveCompaniesOfferts(DateTime date, Guid cgid)
            => dispatcher.DispatchQuery<GetNumberOfActiveCompaniesOffertsQuery, StatsBarChartViewModel>(new GetNumberOfActiveCompaniesOffertsQuery() { Date = date, CGID = cgid });

        [HttpGet]
        [Route("GetNumberOfCompanyRecruiters")]
        public StatsBarChartViewModel GetNumberOfCompanyRecruiters(Guid cgid)
           => dispatcher.DispatchQuery<GetNumberOfCompanyRecruitersQuery, StatsBarChartViewModel>(new GetNumberOfCompanyRecruitersQuery() { CGID = cgid });
    }
}