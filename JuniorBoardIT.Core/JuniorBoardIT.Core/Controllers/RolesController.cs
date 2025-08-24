using JuniorBoardIT.Core.CQRS.Dispatcher;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Queries;
using JuniorBoardIT.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JuniorBoardIT.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public RolesController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetUserRoles")]
        public RolesViewModel GetUserRoles()
            => dispatcher.DispatchQuery<GetUserRolesQuery, RolesViewModel>(new GetUserRolesQuery());

        [HttpGet]
        [Route("GetIsUserAdmin")]
        public bool GetIsUserAdmin() 
            => dispatcher.DispatchQuery<GetIsUserAdminQuery, bool>(new GetIsUserAdminQuery());

        [HttpGet]
        [Route("GetIsUserSupport")]
        public bool GetIsUserSupport() 
            => dispatcher.DispatchQuery<GetIsUserSupportQuery, bool>(new GetIsUserSupportQuery());

        [HttpGet]
        [Route("GetIsUserPremium")]
        public bool GetIsUserPremium()
            => dispatcher.DispatchQuery<GetIsUserPremiumQuery, bool>(new GetIsUserPremiumQuery());
    }
}
