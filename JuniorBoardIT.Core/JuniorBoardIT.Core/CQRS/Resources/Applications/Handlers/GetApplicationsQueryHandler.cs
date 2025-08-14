using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Queries;
using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;
using JuniorBoardIT.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace JuniorBoardIT.Core.CQRS.Resources.Applications.Handlers
{
    public class GetApplicationsQueryHandler : IQueryHandler<GetApplicationsQuery, ApplicationsViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        private readonly IUserContext user;
        public GetApplicationsQueryHandler(IDataBaseContext context, IMapper mapper, IUserContext user)
        {
            this.context = context;
            this.mapper = mapper;
            this.user = user;
        }

        public ApplicationsViewModel Handle(GetApplicationsQuery query)
        {
            var applications = new List<Entities.Applications>();
            
            if(query.UGID != null && query.UGID != Guid.Empty)
            {
                applications = context.Applications.Where(x => x.AUGID == query.UGID).AsNoTracking().ToList();
            } else
            {
                applications = context.Applications.Where(x => x.AUGID == Guid.Parse(user.UGID)).AsNoTracking().ToList();
            }


            var applicationsViewModel = new List<ApplicationViewModel>();
            var count = applications.Count;
            applications = applications.Skip(query.Skip).Take(query.Take).ToList();

            applications.ForEach(x =>
            {
                var model = mapper.Map<Entities.Applications, ApplicationViewModel>(x);
                applicationsViewModel.Add(model);
            });

            var model = new ApplicationsViewModel()
            {
                List = applicationsViewModel,
                Count = count,
            };

            return model;
        }
    }
}
