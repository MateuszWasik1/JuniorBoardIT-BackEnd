using System.Security.Claims;

namespace JuniorBoardIT.Core.Services
{
    public interface IUserContext
    {
        ClaimsPrincipal? User { get; }
        int UID { get; }
        string? UGID { get; }
    }
}
