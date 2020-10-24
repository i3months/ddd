using System.Transactions;
using _20.SnsApplication.Users.Commons;
using _20.SnsApplication.Users.Delete;
using _20.SnsApplication.Users.Get;
using _20.SnsApplication.Users.GetAll;
using _20.SnsApplication.Users.Register;
using _20.SnsApplication.Users.Update;

namespace _20.SnsApplication.Users
{
    public class UserApplicationService
    {
        private readonly IUserFactory userFactory; 
        private readonly IUserRepository userRepository;
        private readonly UserService userService;

        public UserApplicationService(IUserFactory userFactory, IUserRepository userRepository, UserService userService)
        {
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
            // 트랜잭션 범위를 생성함
            // using 문의 범위 안에서 커넥션을 열면 자동으로 트랜잭션이 시작된다
            using (var transaction = new TransactionScope())
            {
                var name = new UserName(command.Name);
                var user = userFactory.Create(name);

                if (userService.Exists(user))
                {
                    throw new CanNotRegisterUserException(user, "이미 등록된 사용자임");
                }

                userRepository.Save(user);
                // 실제 데이터에 반영하기 위해 커밋
                transaction.Complete();
            }
        }

        public void Update(UserUpdateCommand command)
        {
            using (var transaction = new TransactionScope())
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

                transaction.Complete();
            }
        }

        public void Delete(UserDeleteCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var id = new UserId(command.Id);
                var user = userRepository.Find(id);
                if (user == null)
                {
                    return;
                }

                userRepository.Delete(user);

                transaction.Complete();
            }
        }
    }
}
