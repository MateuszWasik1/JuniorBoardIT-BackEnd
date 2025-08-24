using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public ReportsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetReport")]
        [Authorize]
        public GetReportViewModel GetReport(Guid rgid)
            => dispatcher.DispatchQuery<GetReportQuery, GetReportViewModel>(new GetReportQuery() { RGID = rgid });

        [HttpGet]
        [Route("GetReports")]
        [Authorize]
        public GetReportsViewModel GetReports(ReportsTypeEnum reportType, int skip, int take)
            => dispatcher.DispatchQuery<GetReportsQuery, GetReportsViewModel>(new GetReportsQuery() { ReportType = reportType, Skip = skip, Take = take });

        [HttpPost]
        [Route("SaveReport")]
        public void SaveReport(ReportViewModel model)
            => dispatcher.DispatchCommand(new SaveReportCommand() { Model = model });

        [HttpPut]
        [Route("ChangeReportStatus")]
        [Authorize]
        public void ChangeReportStatus(ChangeReportStatusViewModel model)
            => dispatcher.DispatchCommand(new ChangeReportStatusCommand() { Model = model });
    }
}
