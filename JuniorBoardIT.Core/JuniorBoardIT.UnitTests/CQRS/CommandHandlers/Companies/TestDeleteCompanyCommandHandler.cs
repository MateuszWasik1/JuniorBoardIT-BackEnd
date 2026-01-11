using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers;
using JuniorBoardIT.Core.Entities;
using JuniorBoardIT.Core.Exceptions.Companies;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.Companies
{
    [TestFixture]
    public class TestDeleteCompanyCommandHandler
    {
        private Mock<IDataBaseContext> context;

        private List<Core.Entities.Companies> companies;

        private List<Core.Entities.User> users;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            companies = new List<Core.Entities.Companies> 
            {
                new Core.Entities.Companies()
                {
                    CID = 1,
                    CGID = new Guid("29dd879c-ee2f-11db-8314-0800200c9a66"),
                },
                new Core.Entities.Companies()
                {
                    CID = 2,
                    CGID = new Guid("30dd879c-ee2f-11db-8314-0800200c9a66"),
                }
            };

            users = new List<Core.Entities.User>
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    UCompanyGID = new Guid("29dd879c-ee2f-11db-8314-0800200c9a66")
                }
            };

            context.Setup(x => x.AllUsers).Returns(users.AsQueryable());

            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            context.Setup(x => x.DeleteCompany(It.IsAny<Core.Entities.Companies>())).Callback<Core.Entities.Companies>(company => companies.Remove(company));
        }

        [Test]
        public void TestDeleteCompanyCommandHandler_CompanyNotFound_ShouldThrow_CompanyNotFoundExceptions()
        {
            // Arrange
            var command = new DeleteCompanyCommand() { CGID = Guid.NewGuid() };
            var handler = new DeleteCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNotFoundExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestDeleteCompanyCommandHandler_CompanyHasRecruiters_ShouldThrow_CompanyHasRecruitersExceptions()
        {
            // Arrange
            var command = new DeleteCompanyCommand() { CGID = companies[0].CGID };
            var handler = new DeleteCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyHasRecruitersExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestDeleteCompanyCommandHandler_CompanyExistsAndHasNoRecruiters_ShouldDelete_Company()
        {
            // Arrange
            var command = new DeleteCompanyCommand() { CGID = companies[1].CGID };
            var handler = new DeleteCompanyCommandHandler(context.Object);

            // Act
            handler.Handle(command);

            // Assert
            ClassicAssert.AreEqual(1, companies.Count);
            context.Verify(x => x.DeleteCompany(It.IsAny<Core.Entities.Companies>()), Times.Once);
            context.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
