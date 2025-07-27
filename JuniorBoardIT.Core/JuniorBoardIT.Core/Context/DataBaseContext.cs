using Microsoft.EntityFrameworkCore;
using JuniorBoardIT.Core.Entities;
using JuniorBoardIT.Core.Services;

namespace JuniorBoardIT.Core.Context
{
    public class DataBaseContext : IDataBaseContext
    {
        private DataContext dataContext;
        private IUserContext user;

        public DataBaseContext(DataContext dataContext, IUserContext user)
        {
            this.dataContext = dataContext;
            this.user = user;
        }

        #region User
        public IQueryable<User> User => dataContext.User;
        public void CreateOrUpdate(User user)
        {
            if (user.UID == default)
                dataContext.User.Add(user);
            else
                dataContext.Entry(user).State = EntityState.Modified;
        }
        public void DeleteUser(User user) => dataContext.User.Remove(user);
        #endregion

        #region Roles
        public IQueryable<Roles> Roles => dataContext.AppRoles;
        #endregion

        #region Users
        public IQueryable<User> AllUsers => dataContext.User;
        #endregion

        #region JobOffers
        public IQueryable<JobOffers> JobOffers => dataContext.JobOffers;
        public void CreateOrUpdate(JobOffers jobOffer)
        {
            if (jobOffer.JOID == default)
                dataContext.JobOffers.Add(jobOffer);
            else
                dataContext.Entry(jobOffer).State = EntityState.Modified;
        }
        public void DeleteJobOffer(JobOffers jobOffer) => dataContext.JobOffers.Remove(jobOffer);
        #endregion

        #region Reports
        public IQueryable<Reports> Reports => dataContext.Reports;
        public void CreateOrUpdate(Reports report)
        {
            if (report.RID == default)
                dataContext.Reports.Add(report);
            else
                dataContext.Entry(report).State = EntityState.Modified;
        }
        public void DeleteReport(Reports report) => dataContext.Reports.Remove(report);
        #endregion

        #region Bugs
        public IQueryable<Bugs> Bugs => dataContext.Bugs.Where(x => x.BUID == user.UID);
        public IQueryable<Bugs> AllBugs => dataContext.Bugs;
        public void CreateOrUpdate(Bugs bug)
        {
            if (bug.BID == default)
                dataContext.Bugs.Add(bug);
            else
                dataContext.Entry(bug).State = EntityState.Modified;
        }
        #endregion

        #region BugsNotes
        public IQueryable<BugsNotes> BugsNotes => dataContext.BugsNotes.Where(x => x.BNUID == user.UID);
        public IQueryable<BugsNotes> AllBugsNotes => dataContext.BugsNotes;
        public void CreateOrUpdate(BugsNotes bugNote)
        {
            if (bugNote.BNID == default)
                dataContext.BugsNotes.Add(bugNote);
            else
                dataContext.Entry(bugNote).State = EntityState.Modified;
        }
        public void DeleteBugNote(BugsNotes bugNote) => dataContext.BugsNotes.Remove(bugNote);
        #endregion

        #region Companies
        public IQueryable<Companies> Companies => dataContext.Companies;
        public void CreateOrUpdate(Companies company)
        {
            if (company.CID == default)
                dataContext.Companies.Add(company);
            else
                dataContext.Entry(company).State = EntityState.Modified;
        }
        public void DeleteCompany(Companies company) => dataContext.Companies.Remove(company);
        #endregion

        public void SaveChanges() => dataContext.SaveChanges();
        public void Dispose() => dataContext.Dispose();
    }
}
