using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public ApplicationsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetApplications")]
        public ApplicationsViewModel GetApplications(int skip, int take, Guid ugid)
            => dispatcher.DispatchQuery<GetApplicationsQuery, ApplicationsViewModel>(new GetApplicationsQuery() { Skip = skip, Take = take, UGID = ugid });

        [HttpPost]
        [Route("AddApplication")]
        public void AddApplication(AddApplicationViewModel model)
            => dispatcher.DispatchCommand(new AddApplicationCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteApplication/{agid}")]
        public void DeleteApplication(Guid agid)
            => dispatcher.DispatchCommand(new DeleteApplicationCommand() { AGID = agid });
    }
}
