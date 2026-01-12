using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.CQRS.Resources.User.Handlers;
using JuniorBoardIT.Core.Exceptions.Companies;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Companies
{
    [TestFixture]
    public class TestGetCompanyQueryHandler
    {
        private Mock<IDataBaseContext>? context;
        private Mock<IMapper>? mapper;

        private List<Core.Entities.Companies>? companies;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            mapper = new Mock<IMapper>();

            companies = new List<Core.Entities.Companies>()
            {
                new Core.Entities.Companies()
                {
                    CID = 1,
                    CGID = new Guid("a189857a-bf45-4c25-9644-f2408351d328"),
                    CName = "Name"
                }
            };

            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            mapper.Setup(x => x.Map<Core.Entities.Companies, CompanyViewModel>(It.IsAny<Core.Entities.Companies>()))
                  .Returns<Core.Entities.Companies>(company => new CompanyViewModel
                  {
                      CGID = company.CGID,
                      CName = company.CName,
                  });
        }

        [Test]
        public void TestGetCompanyQueryHandler_CompanyNotFound_ShouldThrow_CompanyNotFoundExceptions()
        {
            //Arrange
            var query = new GetCompanyQuery() { CGID = Guid.NewGuid() };
            var handler = new GetCompanyQueryHandler(context.Object, mapper.Object);

            //Act
            //Assert
            Assert.Throws<CompanyNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetCompanyQueryHandler_GetCompany_ShouldReturn_Company()
        {
            //Arrange
            var query = new GetCompanyQuery() { CGID = companies[0].CGID };
            var handler = new GetCompanyQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(companies[0].CGID, result.CGID);
            ClassicAssert.AreEqual(companies[0].CName, result.CName);
        }
    }
}
