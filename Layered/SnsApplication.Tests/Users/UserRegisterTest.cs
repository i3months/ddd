using InMemoryInfrastructure.Persistence.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnsApplication.Users;
using SnsApplication.Users.Register;
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
            var userApplicationService = new UserApplicationService(userFactory, userRepository, userService);

            // 사용자명의 최소 길이(3글자)를 만족하는 사용자 등록이 정상적으로 완료되는지 확인
            var userName = "123";
            var minUserNameInputData = new UserRegisterCommand(userName);
            var outputData = userApplicationService.Register(minUserNameInputData);
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
            var userApplicationService = new UserApplicationService(userFactory, userRepository, userService);

            // 사용자명의 최장 길이(20글자)를 만족하는 사용자 등록이 정상적으로 완료되는지 확인
            var userName = "12345678901234567890";
            var maxUserNameInputData = new UserRegisterCommand(userName);
            userApplicationService.Register(maxUserNameInputData);

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
            var userApplicationService = new UserApplicationService(userFactory, userRepository, userService);

            bool exceptionOccured = false;
            try
            {
                var command = new UserRegisterCommand("12");
                userApplicationService.Register(command);
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
            var userApplicationService = new UserApplicationService(userFactory, userRepository, userService);

            bool exceptionOccured = false;
            try
            {
                var command = new UserRegisterCommand("123456789012345678901");
                userApplicationService.Register(command);
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
            var userApplicationService = new UserApplicationService(userFactory, userRepository, userService);

            var userName = "test-user";
            userRepository.Save(new User(
                new UserId("test-id"),
                new UserName(userName),
                UserType.Normal
            ));

            bool exceptionOccured = false;
            try
            {
                var command = new UserRegisterCommand(userName);
                userApplicationService.Register(command);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }
    }
}
