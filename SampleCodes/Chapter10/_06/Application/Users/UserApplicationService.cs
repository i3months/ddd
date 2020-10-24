using System.Data.SqlClient;
using System.Linq;
using _06.Application.Users.Commons;
using _06.Application.Users.Delete;
using _06.Application.Users.Get;
using _06.Application.Users.GetAll;
using _06.Application.Users.Register;
using _06.Application.Users.Update;
using _06.Domain.Models.Users;

namespace _06.Application.Users
{
    public class UserApplicationService
    {
        private readonly SqlConnection connection;
        private readonly IUserFactory userFactory; 
        private readonly IUserRepository userRepository;
        private readonly UserService userService;

        public UserApplicationService(SqlConnection connection, IUserFactory userFactory, IUserRepository userRepository, UserService userService)
        {
            this.connection = connection;
            this.userFactory = userFactory;
            this.userRepository = userRepository;
            this.userService = userService;
        }

        public UserGetResult Get(UserGetCommand command)
        {
            var id = new UserId(command.Id);
            var user = userRepository.Find(id);
            if (user == null)
            {
                throw new UserNotFoundException(id, "사용자를 찾지 못했음");
            }

            var data = new UserData(user);

            return new UserGetResult(data);
        }

        public UserGetAllResult GetAll()
        {
            var users = userRepository.FindAll();
            var userModels = users.Select(x => new UserData(x)).ToList();
            return new UserGetAllResult(userModels);
        }

        public void Register(UserRegisterCommand command)
        {
            // 커넥션을 통해 트랜잭션을 시작
            using (var transaction = connection.BeginTransaction())
            {
                var name = new UserName(command.Name);
                var user = userFactory.Create(name);

                if (userService.Exists(user))
                {
                    throw new CanNotRegisterUserException(user, "이미 등록된 사용자임");
                }

                userRepository.Save(user);
                // 처리가 완료되면 커밋
                transaction.Commit();
            }
        }

        public void Update(UserUpdateCommand command)
        {
            var id = new UserId(command.Id);
            var user = userRepository.Find(id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }

            if (command.Name != null)
            {
                var name = new UserName(command.Name);
                user.ChangeName(name);

                if (userService.Exists(user))
                {
                    throw new CanNotRegisterUserException(user, "이미 등록된 사용자임");
                }
            }

            userRepository.Save(user);
        }

        public void Delete(UserDeleteCommand command)
        {
            var id = new UserId(command.Id);
            var user = userRepository.Find(id);
            if (user == null)
            {
                return;
            }

            userRepository.Delete(user);
        }
    }
}
