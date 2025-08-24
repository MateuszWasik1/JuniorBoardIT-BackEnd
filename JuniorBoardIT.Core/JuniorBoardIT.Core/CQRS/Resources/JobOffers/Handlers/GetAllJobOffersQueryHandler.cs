using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Queries;
using JuniorBoardIT.Core.CQRS.Resources.JobOffers.Queries;
using JuniorBoardIT.Core.Models.Enums.JobOffers;
using JuniorBoardIT.Core.Models.ViewModels.JobOffersViewModels;
using JuniorBoardIT.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace JuniorBoardIT.Core.CQRS.Resources.JobOffers.Handlers
{
    public class GetAllJobOffersQueryHandler : IQueryHandler<GetAllJobOffersQuery, GetAllJobOffersViewModel>
    {

        private readonly IDataBaseContext context;
        private readonly IMapper mapper;
        private readonly IUserContext user;
        public GetAllJobOffersQueryHandler(IDataBaseContext context, IMapper mapper, IUserContext user)
        {
            this.context = context;
            this.mapper = mapper;
            this.user = user;
        }

        public GetAllJobOffersViewModel Handle(GetAllJobOffersQuery query)
        {
            var jobOffers = new List<Entities.JobOffers>();

            if (query.Favorites)
            {
                var favoriteJobOffers = context.FavoriteJobOffers.Where(x => x.FJOUGID == Guid.Parse(user.UGID)).Select(x => x.FJOJOGID).ToList();
                jobOffers = context.JobOffers.Where(x => favoriteJobOffers.Contains(x.JOGID)).ToList();
                return ReturnModel(jobOffers, query, true);
            }                

            if(query.Education == EducationEnum.Elementary)
                jobOffers = context.JobOffers.Where(x => x.JOEducation == EducationEnum.Elementary).ToList();
            else if(query.Education == EducationEnum.Secondary)
                jobOffers = context.JobOffers.Where(x => x.JOEducation <= EducationEnum.Secondary).ToList();
            else if (query.Education == EducationEnum.Vocational)
                jobOffers = context.JobOffers.Where(x => x.JOEducation == EducationEnum.Elementary || x.JOEducation == EducationEnum.Vocational).ToList();
            else if (query.Education == EducationEnum.HigherILevel)
                jobOffers = context.JobOffers.Where(x => x.JOEducation <= EducationEnum.HigherILevel).ToList();
            else if (query.Education == EducationEnum.HigherIILevel)
                jobOffers = context.JobOffers.Where(x => x.JOEducation <= EducationEnum.HigherIILevel).ToList();
            else if (query.Education == EducationEnum.All)
                jobOffers = context.JobOffers.ToList();
            else
                jobOffers = context.JobOffers.ToList();

            return ReturnModel(jobOffers, query);
        }

        public GetAllJobOffersViewModel ReturnModel(List<Entities.JobOffers> jobOffers, GetAllJobOffersQuery query, bool alwaysFavorite = false)
        {
            var allJobOffersViewModel = new List<JobOfferViewModel>();
            var count = jobOffers.Count;
            jobOffers = jobOffers.Skip(query.Skip).Take(query.Take).ToList();

            var favoriteJobOffers = new List<Entities.FavoriteJobOffers>();

            var userGID = Guid.Parse(user?.UGID ?? Guid.Empty.ToString());

            if(!alwaysFavorite)
                favoriteJobOffers = context.FavoriteJobOffers.Where(x => userGID == x.FJOUGID).AsNoTracking().ToList();

            jobOffers.ForEach(x =>
            {
                var model = mapper.Map<Entities.JobOffers, JobOfferViewModel>(x);

                if(alwaysFavorite)
                    model.JOFavorite = true;
                else
                    model.JOFavorite = favoriteJobOffers.Any(x => x.FJOJOGID == model.JOGID);

                allJobOffersViewModel.Add(model);
            });

            var model = new GetAllJobOffersViewModel()
            {
                List = allJobOffersViewModel,
                Count = count,
            };

            return model;
        }
    }
}
