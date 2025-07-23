using JuniorBoardIT.Core.Entities;

namespace JuniorBoardIT.Core.Context
{
    public interface IDataBaseContext : IDisposable
    {
        #region User
        IQueryable<User> User { get; }
        void CreateOrUpdate(User user);
        void DeleteUser(User user);
        #endregion

        #region Roles
        IQueryable<Roles> Roles { get; }
        #endregion

        #region Users
        IQueryable<User> AllUsers { get; }
        #endregion

        #region JobOffers
        IQueryable<JobOffers> JobOffers { get; }
        void CreateOrUpdate(JobOffers jobOffers);
        void DeleteJobOffer(JobOffers jobOffers);
        #endregion

        #region Reports
        IQueryable<Reports> Reports { get; }
        void CreateOrUpdate(Reports report);
        void DeleteReport(Reports report);
        #endregion

        #region Bugs
        IQueryable<Bugs> Bugs { get; }
        IQueryable<Bugs> AllBugs { get; }
        void CreateOrUpdate(Bugs bug);
        #endregion

        #region BugsNotes
        IQueryable<BugsNotes> BugsNotes { get; }
        IQueryable<BugsNotes> AllBugsNotes { get; }
        void CreateOrUpdate(BugsNotes bugNote);
        void DeleteBugNote(BugsNotes bugNote);
        #endregion

        void SaveChanges();
    }
}
