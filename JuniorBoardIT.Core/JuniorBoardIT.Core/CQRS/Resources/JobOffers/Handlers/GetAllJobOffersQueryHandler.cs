using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class GetAllJobOffersQueryHandler : IQueryHandler<GetAllJobOffersQuery, GetAllJobOffersViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        public GetAllJobOffersQueryHandler(IDataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public GetAllJobOffersViewModel Handle(GetAllJobOffersQuery query)
        {
            //var usersData = context.AllUsers.ToList();

            //var usersAdmViewModel = new List<UsersAdminViewModel>();

            //var count = usersData.Count;
            //usersData = usersData.Skip(query.Skip).Take(query.Take).ToList();

            //usersData.ForEach(x => {
            //    var model = mapper.Map<Entities.User, UsersAdminViewModel>(x);
            //    usersAdmViewModel.Add(model);
            //});

            //var model = new GetUsersAdminViewModel()
            //{
            //    List = usersAdmViewModel,
            //    Count = count,
            //};

            //return model;

            return new GetAllJobOffersViewModel();
        }
    }
}
