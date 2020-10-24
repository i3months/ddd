using InMemoryInfrastructure.Persistence.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnsApplication.Users.Interactors;
using SnsApplicationPort.Users.Register;
using SnsDomain.Models.Users;
using SnsDomain.Services;

namespace SnsApplication.Tests.Users
{
    [TestClass]
    public class UserRegisterTest
    {
        [TestMethod]
        public void TestSuccessMinUserName()
        {
            var userFactory = new InMemoryUserFactory();
            var userRepository = new InMemoryUserRepository();
            var userService = new UserService(userRepository);
            var interactor = new UserRegisterInteractor(userFactory, userRepository, userService);

            // 사용자명의 최소 길이(3글자)를 만족하는 사용자 등록이 정상적으로 완료되는지 확인
            var userName = "123";
            var minUserNameInputData = new UserRegisterInputData(userName);
            var outputData = interactor.Handle(minUserNameInputData);
            Assert.IsNotNull(outputData.CreatedUserId);

            // 사용자 정보가 잘 저장됐는지 확인
            var createdUserName = new UserName(userName);
            var createdUser = userRepository.Find(createdUserName);
            Assert.IsNotNull(createdUser);
        }

        [TestMethod]
        public void TestSuccessMaxUserName()
        {
            var userFactory = new InMemoryUserFactory();
            var userRepository = new InMemoryUserRepository();
            var userService = new UserService(userRepository);
            var interactor = new UserRegisterInteractor(userFactory, userRepository, userService);

            // 사용자명의 최장 길이(20글자)를 만족하는 사용자 등록이 정상적으로 완료되는지 확인
            var userName = "12345678901234567890";
            var maxUserNameInputData = new UserRegisterInputData(userName);
            interactor.Handle(maxUserNameInputData);

            // 사용자 정보가 잘 저장됐는지 확인
            var createdUserName = new UserName(userName);
            var maxUserNameUser = userRepository.Find(createdUserName);
            Assert.IsNotNull(maxUserNameUser);
        }

        [TestMethod]
        public void TestInvalidUserNameLengthMin()
        {
            var userFactory = new InMemoryUserFactory();
            var userRepository = new InMemoryUserRepository();
            var userService = new UserService(userRepository);
            var interactor = new UserRegisterInteractor(userFactory, userRepository, userService);

            bool exceptionOccured = false;
            try
            {
                var command = new UserRegisterInputData("12");
                interactor.Handle(command);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }

        [TestMethod]
        public void TestInvalidUserNameLengthMax()
        {
            var userFactory = new InMemoryUserFactory();
            var userRepository = new InMemoryUserRepository();
            var userService = new UserService(userRepository);
            var interactor = new UserRegisterInteractor(userFactory, userRepository, userService);

            bool exceptionOccured = false;
            try
            {
                var command = new UserRegisterInputData("123456789012345678901");
                interactor.Handle(command);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }

        [TestMethod]
        public void TestAlreadyExists()
        {
            var userFactory = new InMemoryUserFactory();
            var userRepository = new InMemoryUserRepository();
            var userService = new UserService(userRepository);
            var interactor = new UserRegisterInteractor(userFactory, userRepository, userService);

            var userName = "test-user";
            userRepository.Save(new User(
                new UserId("test-id"),
                new UserName(userName),
                UserType.Normal
            ));

            bool exceptionOccured = false;
            try
            {
                var command = new UserRegisterInputData(userName);
                interactor.Handle(command);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }
    }
}
