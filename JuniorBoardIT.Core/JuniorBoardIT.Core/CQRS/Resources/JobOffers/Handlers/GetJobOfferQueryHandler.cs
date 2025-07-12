using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.CQRS.Resources.User.Handlers
{
    public class GetJobOfferQueryHandler : IQueryHandler<GetJobOfferQuery, JobOfferViewModel>
    {
        private readonly IDataBaseContext context;
        private readonly IUserContext user;
        private readonly IMapper mapper;
        public GetJobOfferQueryHandler(IDataBaseContext context, IUserContext user, IMapper mapper)
        {
            this.context = context;
            this.user = user;
            this.mapper = mapper;
        }

        public JobOfferViewModel Handle(GetJobOfferQuery query)
        {
            //var userData = context.User.FirstOrDefault(x => x.UID == user.UID);

            //if (userData == null)
            //    throw new UserNotFoundExceptions("Nie znaleziono użytkownika!");

            //var model = mapper.Map<Entities.User, UserViewModel>(userData);

            //return model;

            return new JobOfferViewModel();
        }
    }
}
