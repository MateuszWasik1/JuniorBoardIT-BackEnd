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
        public StatsBarChartViewModel GetNumberOfCompanyPublishedOfferts(DateTime startDate, DateTime endDate)
            => dispatcher.DispatchQuery<GetNumberOfCompanyPublishedOffertsQuery, StatsBarChartViewModel>(new GetNumberOfCompanyPublishedOffertsQuery() { StartDate = startDate, EndDate = endDate });

        [HttpGet]
        [Route("GetNumberOfCompaiesPublishedOfferts")]
        public StatsBarChartViewModel GetNumberOfCompaiesPublishedOfferts(DateTime startDate, DateTime endDate)
            => dispatcher.DispatchQuery<GetNumberOfCompaiesPublishedOffertsQuery, StatsBarChartViewModel>(new GetNumberOfCompaiesPublishedOffertsQuery() { StartDate = startDate, EndDate = endDate });

        [HttpGet]
        [Route("GetNumberOfActiveCompaniesOfferts")]
        public StatsBarChartViewModel GetNumberOfActiveCompaniesOfferts(DateTime date)
            => dispatcher.DispatchQuery<GetNumberOfActiveCompaniesOffertsQuery, StatsBarChartViewModel>(new GetNumberOfActiveCompaniesOffertsQuery() { Date = date });

        [HttpGet]
        [Route("GetNumberOfCompanyRecruiters")]
        public StatsBarChartViewModel GetNumberOfCompanyRecruiters(DateTime date)
           => dispatcher.DispatchQuery<GetNumberOfCompanyRecruitersQuery, StatsBarChartViewModel>(new GetNumberOfCompanyRecruitersQuery() { Date = date });
    }
}