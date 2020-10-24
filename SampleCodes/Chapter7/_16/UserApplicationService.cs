namespace _16
{
    public class UserApplicationService
    {
        private readonly IUserRepository userRepository;
        // IFooRepository에 대한 의존 관계가 새로 추가됨
        private readonly IFooRepository fooRepository;

        // 생성자 메서드를 통해 의존 관계를 주입함
        public UserApplicationService(IUserRepository userRepository, IFooRepository fooRepository)
        {
            this.userRepository = userRepository;
            this.fooRepository = fooRepository;
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
