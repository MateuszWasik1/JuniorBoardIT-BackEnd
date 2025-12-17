using JuniorBoardIT.Core.Context;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Handlers;
using JuniorBoardIT.Core.CQRS.Resources.Roles.Queries;
using JuniorBoardIT.Core.Models.Enums;
using JuniorBoardIT.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace JuniorBoardIT.UnitTests.CQRS.QueryHandler.Roles
{
    [TestFixture]
    public class TestGetIsUserSupportQueryHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext> user;

        private List<Core.Entities.User> users;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            user = new Mock<IUserContext>();

            users = new List<Core.Entities.User>()
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    URID = (int) RoleEnum.User,
                },
                new Core.Entities.User()
                {
                    UID = 2,
                    URID = (int) RoleEnum.Premium,
                },
                new Core.Entities.User()
                {
                    UID = 3,
                    URID = (int) RoleEnum.Recruiter,
                },
                new Core.Entities.User()
                {
                    UID = 4,
                    URID = (int) RoleEnum.Support,
                },
                 new Core.Entities.User()
                {
                    UID = 5,
                    URID = (int) RoleEnum.Admin,
                },
            };

            context.Setup(x => x.User).Returns(users.AsQueryable());
        }

        [Test]
        public void TestGetIsUserSupportQueryHandler_UserNotFound_ShouldReturn_False()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(9);

            var query = new GetIsUserSupportQuery();
            var handler = new GetIsUserSupportQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestGetIsUserSupportQueryHandler_UserIsUser_ShouldReturn_False()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(1);

            var query = new GetIsUserSupportQuery();
            var handler = new GetIsUserSupportQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestGetIsUserSupportQueryHandler_UserIsPremium_ShouldReturn_False()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(2);

            var query = new GetIsUserSupportQuery();
            var handler = new GetIsUserSupportQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestGetIsUserSupportQueryHandler_UserIsRecruiter_ShouldReturn_False()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(3);

            var query = new GetIsUserSupportQuery();
            var handler = new GetIsUserSupportQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestGetIsUserSupportQueryHandler_UserIsSupport_ShouldReturn_False()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(4);

            var query = new GetIsUserSupportQuery();
            var handler = new GetIsUserSupportQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void TestGetIsUserSupportQueryHandler_UserIsAdmin_ShouldReturn_False()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(5);
            
            var query = new GetIsUserSupportQuery();
            var handler = new GetIsUserSupportQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }
    }
}