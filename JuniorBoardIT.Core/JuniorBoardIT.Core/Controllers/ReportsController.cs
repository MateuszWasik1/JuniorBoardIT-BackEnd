using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.Models.ViewModels.ReportsViewModels;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Reports.Queries;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public ReportsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetReports")]
        public ReportsListViewModel GetReports(int skip, int take)
            => dispatcher.DispatchQuery<GetReportsQuery, ReportsListViewModel>(new GetReportsQuery() { Skip = skip, Take = take });

        [HttpPost]
        [Route("AddReport")]
        public void AddReport(ReportViewModel model)
            => dispatcher.DispatchCommand(new AddReportCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteReport/{rgid}")]
        public void Delete(Guid rgid)
             => dispatcher.DispatchCommand(new DeleteReportCommand() { RGID = rgid });
    }
}