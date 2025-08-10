using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Abstraction.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;
using JuniorBoardIT.Core.Exceptions.Companies;

namespace JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers
{
    public class DeleteCompanyCommandHandler : ICommandHandler<DeleteCompanyCommand>
    {
        private readonly IDataBaseContext context;
        public DeleteCompanyCommandHandler(IDataBaseContext context) => this.context = context;

        public void Handle(DeleteCompanyCommand command)
        {
            var deletedCompany = context.Companies.FirstOrDefault(x => x.CGID == command.CGID);

            if (deletedCompany == null)
                throw new CompanyNotFoundExceptions("Nie znaleziono firmy!");

            var recruiters = context.AllUsers.Where(x => x.UCompanyGID == deletedCompany.CGID).Any();

            if (recruiters)
                throw new CompanyHasRecruitersExceptions("Firma posiada zatrudnionych rekruterów!");

            context.DeleteCompany(deletedCompany);
            context.SaveChanges();
        }
    }
}