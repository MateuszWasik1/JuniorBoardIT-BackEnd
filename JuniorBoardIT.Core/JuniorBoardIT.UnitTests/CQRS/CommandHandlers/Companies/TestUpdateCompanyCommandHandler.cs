using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Companies.Handlers;
using JuniorBoardIT.Core.Exceptions.Companies;
using JuniorBoardIT.Core.Models.ViewModels.CompaniesViewModel;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.Companies
{
    [TestFixture]
    public class TestUpdateCompanyCommandHandler
    {

        private Mock<IDataBaseContext> context;

        private CompanyViewModel updateCompanyViewModel()
        {
            return new CompanyViewModel()
            {
                CName = "Firma",
                CDescription = "Opis",
                CEmail = "email@test.pl",
                CAddress = "Adres",
                CCity = "Miasto",
                CCountry = "Polska",
                CPostalCode = "00-000",
                CPhoneNumber = "123456789",
                CNIP = "1234567890",
                CRegon = "123456789",
                CKRS = "1234567890",
                CLI = "linkedin",
                CFoundedYear = 2000
            };
        }

        private List<Core.Entities.Companies> companies;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            companies = new List<Core.Entities.Companies>()
            {
                new Core.Entities.Companies()
                {
                    CName = "Firma2",
                    CDescription = "Opis2",
                    CEmail = "email2@test.pl",
                    CAddress = "Adres2",
                    CCity = "Miasto2",
                    CCountry = "Polska2",
                    CPostalCode = "12-000",
                    CPhoneNumber = "123456700",
                    CNIP = "1234567891",
                    CRegon = "123456780",
                    CKRS = "1234567891",
                    CLI = "linkedin1",
                    CFoundedYear = 2002
                }
            };

            context.Setup(x => x.Companies).Returns(companies.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.Companies>())).Callback<Core.Entities.Companies>(companies.Add);
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CNameEmpty_ShouldThrow_CompanyNameMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CName = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNameMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CNameOver255_ShouldThrow_CompanyNameMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CName = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNameMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CDescriptionEmpty_ShouldThrow_CompanyDescriptionMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CDescription = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyDescriptionMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CDescriptionOver2000_ShouldThrow_CompanyDescriptionMax2000Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CDescription = new string('a', 2001);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyDescriptionMax2000Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CEmailEmpty_ShouldThrow_CompanyEmailMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CEmail = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyEmailMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CEmailOver255_ShouldThrow_CompanyEmailMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CEmail = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyEmailMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CAddressEmpty_ShouldThrow_CompanyAddressMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CAddress = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyAddressMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CAddressOver255_ShouldThrow_CompanyAddressMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CAddress = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyAddressMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CCityEmpty_ShouldThrow_CompanyCityMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CCity = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyCityMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CCityOver255_ShouldThrow_CompanyCityMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CCity = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyCityMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CCountryEmpty_ShouldThrow_CompanyCountryMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CCountry = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyCountryMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CCountryOver255_ShouldThrow_CompanyCountryMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CCountry = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyCountryMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CPostalCodeEmpty_ShouldThrow_CompanyPostalCodeMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CPostalCode = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyPostalCodeMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CPostalCodeOver255_ShouldThrow_CompanyPostalCodeMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CPostalCode = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyPostalCodeMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CPhoneNumberEmpty_ShouldThrow_CompanyPhoneNumberMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CPhoneNumber = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyPhoneNumberMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CPhoneNumberOver255_ShouldThrow_CompanyPhoneNumberMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CPhoneNumber = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyPhoneNumberMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CNIPTooShort_ShouldThrow_CompanyNIPMin10Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CNIP = "123";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNIPMin10Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CNIPTooLong_ShouldThrow_CompanyNIPMax10Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CNIP = "12345678901";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyNIPMax10Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CRegonTooShort_ShouldThrow_CompanyRegonMin9Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CRegon = "123";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyRegonMin9Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CRegonTooLong_ShouldThrow_CompanyRegonMax14Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CRegon = new string('1', 15);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyRegonMax14Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CKRSTooShort_ShouldThrow_CompanyKRSMin10Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CKRS = "123";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyKRSMin10Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CKRSTooLong_ShouldThrow_CompanyKRSMax10Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CKRS = "12345678901";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyKRSMax10Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CLIEmpty_ShouldThrow_CompanyLinkedInMin0Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CLI = "";

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyLinkedInMin0Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_CLIOver255_ShouldThrow_CompanyLinkedInMax255Exceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CLI = new string('a', 256);

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyLinkedInMax255Exceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_FoundedYearTooSmall_ShouldThrow_CompanyFoundedYearMinExceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CFoundedYear = 999;

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyFoundedYearMinExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_FoundedYearTooBig_ShouldThrow_CompanyFoundedYearMinExceptions()
        {
            // Arrange
            var model = updateCompanyViewModel();
            model.CFoundedYear = DateTime.Now.Year + 2;

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            // Assert
            Assert.Throws<CompanyFoundedYearMinExceptions>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateCompanyCommandHandler_ModelIsValid_ShouldAdd_NewEntity()
        {
            // Arrange
            var model = updateCompanyViewModel();

            var command = new UpdateCompanyCommand() { Model = model };
            var handler = new UpdateCompanyCommandHandler(context.Object);

            // Act
            handler.Handle(command);

            // Assert
            ClassicAssert.AreEqual(model.CName, companies[0].CName);
            ClassicAssert.AreEqual(model.CDescription, companies[0].CDescription);
            ClassicAssert.AreEqual(model.CEmail, companies[0].CEmail);
            ClassicAssert.AreEqual(model.CAddress, companies[0].CAddress);
            ClassicAssert.AreEqual(model.CCity, companies[0].CCity);
            ClassicAssert.AreEqual(model.CCountry, companies[0].CCountry);
            ClassicAssert.AreEqual(model.CPostalCode, companies[0].CPostalCode);
            ClassicAssert.AreEqual(model.CPhoneNumber, companies[0].CPhoneNumber);
            ClassicAssert.AreEqual(model.CNIP, companies[0].CNIP);
            ClassicAssert.AreEqual(model.CRegon, companies[0].CRegon);
            ClassicAssert.AreEqual(model.CKRS, companies[0].CKRS);
            ClassicAssert.AreEqual(model.CLI, companies[0].CLI);
            ClassicAssert.AreEqual(model.CFoundedYear, companies[0].CFoundedYear);
        }
    }
}
