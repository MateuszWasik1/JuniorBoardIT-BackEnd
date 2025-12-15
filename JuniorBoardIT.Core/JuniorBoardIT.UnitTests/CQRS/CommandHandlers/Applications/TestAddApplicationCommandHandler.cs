using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Commands;
using JuniorBoardIT.Core.CQRS.Resources.Applications.Handlers;
using JuniorBoardIT.Core.Models.ViewModels.ApplicationsViewModels;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.CommandHandlers.Applications
{
    [TestFixture]
    public class TestAddApplicationCommandHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext>? user;

        private List<Core.Entities.Applications> applications;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            user = new Mock<IUserContext>();

            applications = new List<Core.Entities.Applications>()
            {
                new Core.Entities.Applications()
                {
                    AID = 1,
                    AGID = new Guid("30dd879c-ee2f-11db-8314-0800200c9a66"),
                },
            };

            context.Setup(x => x.Applications).Returns(applications.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.Applications>())).Callback<Core.Entities.Applications>(application => applications.Add(application));
        }

        [Test]
        public void AddApplicationCommandHandler_ApplicationIsCorrect_ShouldAdd_NewApplication()
        {
            //Arrange 
            var model = new AddApplicationViewModel()
            {
                AJOGID = new Guid("33dd879c-ee2f-11db-8314-0800200c9a66"),
            };

            var command = new AddApplicationCommand() { Model = model };
            var handler = new AddApplicationCommandHandler(context.Object, user.Object);

            user.Setup(x => x.UGID).Returns("31dd879c-ee2f-11db-8314-0800200c9a66");

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(2, applications.Count);
            ClassicAssert.IsTrue(applications[1].AGID.ToString().Length >= 0);
            ClassicAssert.AreEqual("31dd879c-ee2f-11db-8314-0800200c9a66", applications[1].AUGID.ToString());
            ClassicAssert.AreEqual(new Guid("33dd879c-ee2f-11db-8314-0800200c9a66"), applications[1].AJOGID);
            ClassicAssert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), applications[1].AApplicationDate.ToString("yyyy-MM-dd hh:mm:ss"));
        }

        [Test]
        public void AddApplicationCommandHandler_ApplicationIsCorrect_UserGUID_IsNull_ShouldAdd_NewApplication_WithAUGID_null()
        {
            //Arrange 
            var model = new AddApplicationViewModel()
            {
                AJOGID = new Guid("33dd879c-ee2f-11db-8314-0800200c9a66"),
            };

            var command = new AddApplicationCommand() { Model = model };
            var handler = new AddApplicationCommandHandler(context.Object, user.Object);

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(2, applications.Count);
            ClassicAssert.IsTrue(applications[1].AGID.ToString().Length >= 0);
            ClassicAssert.AreEqual("", applications[1].AUGID.ToString());
            ClassicAssert.AreEqual(new Guid("33dd879c-ee2f-11db-8314-0800200c9a66"), applications[1].AJOGID);
            ClassicAssert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), applications[1].AApplicationDate.ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }
}
