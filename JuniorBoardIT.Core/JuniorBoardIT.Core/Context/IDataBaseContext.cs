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

        void SaveChanges();
    }
}
