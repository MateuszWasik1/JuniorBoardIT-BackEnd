using AutoMapper;
using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Queries;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Companies
{
    [TestFixture]
    public class TestGetCompaniesForUserQueryHandler
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
                    CGID = new Guid("b189857a-bf45-4c25-9644-f2408351d328"),
                    CName = "Name1"
                },
                new Core.Entities.Companies()
                {
                    CID = 2,
                    CGID = new Guid("c189857a-bf45-4c25-9644-f2408351d328"),
                    CName = "Name2"
                }
            };

            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            mapper.Setup(x => x.Map<Core.Entities.Companies, GetCompanyForUserViewModel>(It.IsAny<Core.Entities.Companies>()))
                  .Returns<Core.Entities.Companies>(company => new GetCompanyForUserViewModel
                  {
                      CGID = company.CGID,
                      CName = company.CName
                  });
        }

        [Test]
        public void TestGetCompaniesForUserQueryHandler_GetAllCompanies_ShouldReturn_AllCompanies()
        {
            //Arrange
            var query = new GetCompaniesForUserQuery() { };
            var handler = new GetCompaniesForUserQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(2, result.List.Count);
            ClassicAssert.AreEqual(companies[0].CGID, result.List[0].CGID);
            ClassicAssert.AreEqual(companies[0].CName, result.List[0].CName);
            ClassicAssert.AreEqual(companies[1].CGID, result.List[1].CGID);
            ClassicAssert.AreEqual(companies[1].CName, result.List[1].CName);
        }
    }
}
