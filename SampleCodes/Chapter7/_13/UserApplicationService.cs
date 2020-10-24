namespace _13
{
    public class UserApplicationService
    {
        private readonly IUserRepository userRepository;
        // 새로운 속성이 추가됨
        private readonly IFooRepository fooRepository;

        public UserApplicationService()
        {
            this.userRepository = ServiceLocator.Resolve<IUserRepository>();
            // ServiceLocator를 통해 필요한 인스턴스를 받음
            this.fooRepository = ServiceLocator.Resolve<IFooRepository>();
        }

        public UserData Get(string userId)
        {
            var targetId = new UserId(userId);
            var user = userRepository.Find(targetId);

            if (user == null)
            {
                return null;
            }

            var userData = new UserData(user);
            return userData;
        }
    }
}
