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
    public class TestGetCompaniesQueryHandler
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
                    CName = "Name1",
                    CEmail = "Email",
                    CNIP = "NIP",
                    CKRS = "KRS",
                    CRegon = "Regon"
                },
                new Core.Entities.Companies()
                {
                    CID = 2,
                    CName = "Name2",
                    CEmail = "Email",
                    CNIP = "NIP",
                    CKRS = "KRS",
                    CRegon = "Regon"
                },
                new Core.Entities.Companies()
                {
                    CID = 3,
                    CName = "Name3",
                    CEmail = "Email",
                    CNIP = "NIP",
                    CKRS = "KRS",
                    CRegon = "Regon"
                },
                new Core.Entities.Companies()
                {
                    CID = 4,
                    CName = "Test",
                    CEmail = "Email",
                    CNIP = "NIP",
                    CKRS = "KRS",
                    CRegon = "Regon"
                },
                new Core.Entities.Companies()
                {
                    CID = 5,
                    CName = "Name5",
                    CEmail = "Test",
                    CNIP = "NIP",
                    CKRS = "KRS",
                    CRegon = "Regon"
                },
                new Core.Entities.Companies()
                {
                    CID = 6,
                    CName = "Name6",
                    CEmail = "Email",
                    CNIP = "Test",
                    CKRS = "KRS",
                    CRegon = "Regon"
                },
                new Core.Entities.Companies()
                {
                    CID = 7,
                    CName = "Name7",
                    CEmail = "Email",
                    CNIP = "NIP",
                    CKRS = "Test",
                    CRegon = "Regon"
                },
                new Core.Entities.Companies()
                {
                    CID = 8,
                    CName = "Name8",
                    CEmail = "Email",
                    CNIP = "NIP",
                    CKRS = "KRS",
                    CRegon = "Test"
                },
            };

            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            mapper.Setup(x => x.Map<Core.Entities.Companies, CompanyViewModel>(It.IsAny<Core.Entities.Companies>()))
                  .Returns<Core.Entities.Companies>(company => new CompanyViewModel
                  {
                      CName = company.CName,
                      CEmail = company.CEmail,
                      CNIP = company.CNIP,
                      CKRS = company.CKRS,
                      CRegon = company.CRegon
                  });
        }

        [Test]
        public void TestGetCompaniesQueryHandler_GetCompanies_Skip0_Take1_ShouldReturn_OneCompany()
        {
            //Arrange
            var query = new GetCompaniesQuery() { Skip = 0, Take = 1 };
            var handler = new GetCompaniesQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(8, result.Count);
            ClassicAssert.AreEqual(1, result.List.Count);
        }

        [Test]
        public void TestGetCompaniesQueryHandler_GetCompanies_Skip1_Take1_ShouldReturn_OneComapny()
        {
            //Arrange
            var query = new GetCompaniesQuery() { Skip = 1, Take = 1 };
            var handler = new GetCompaniesQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(8, result.Count);
            ClassicAssert.AreEqual(1, result.List.Count);
        }

        [Test]
        public void TestGetCompaniesQueryHandler_GetCompanies_ShouldReturn_AllThreeUser()
        {
            //Arrange
            var query = new GetCompaniesQuery() { Skip = 0, Take = 10, Name = "Test" };
            var handler = new GetCompaniesQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(8, result.Count);
            ClassicAssert.AreEqual(5, result.List.Count);
            ClassicAssert.IsTrue(result.List.Any(x => x.CName == "Test"));
            ClassicAssert.IsTrue(result.List.Any(x => x.CEmail == "Test"));
            ClassicAssert.IsTrue(result.List.Any(x => x.CNIP == "Test"));
            ClassicAssert.IsTrue(result.List.Any(x => x.CKRS == "Test"));
            ClassicAssert.IsTrue(result.List.Any(x => x.CRegon == "Test"));
        }

        [Test]
        public void TestGetCompaniesQueryHandler_GetAllCompenies_ShouldReturn_AllCompanies()
        {
            //Arrange
            var query = new GetCompaniesQuery() { Skip = 0, Take = 10 };
            var handler = new GetCompaniesQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(8, result.Count);
            ClassicAssert.AreEqual(8, result.List.Count);
        }
    }
}
