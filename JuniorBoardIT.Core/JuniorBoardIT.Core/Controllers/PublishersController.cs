using Microsoft.AspNetCore.Mvc;
using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.Models.ViewModels.PublishersViewModels;
using JuniorBoardIT.Core.CQRS.Resources.Publishers.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Publishers.Queries;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public PublishersController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetPublisher")]
        public PublisherViewModel GetPublisher(Guid pgid)
            => dispatcher.DispatchQuery<GetPublisherQuery, PublisherViewModel>(new GetPublisherQuery() { PGID = pgid });

        [HttpGet]
        [Route("GetPublishers")]
        public PublishersListViewModel GetPublishers(int skip, int take)
            => dispatcher.DispatchQuery<GetPublishersQuery, PublishersListViewModel>(new GetPublishersQuery() { Skip = skip, Take = take });

        [HttpPost]
        [Route("AddPublisher")]
        public void AddPublisher(PublisherViewModel model)
            => dispatcher.DispatchCommand(new AddPublisherCommand() { Model = model });

        [HttpPut]
        [Route("UpdatePublisher")]
        public void UpdatePublisher(PublisherViewModel model)
            => dispatcher.DispatchCommand(new UpdatePublisherCommand() { Model = model });

        [HttpDelete]
        [Route("DeletePublisher/{pgid}")]
        public void Delete(Guid pgid)
             => dispatcher.DispatchCommand(new DeletePublisherCommand() { PGID = pgid });
    }
}