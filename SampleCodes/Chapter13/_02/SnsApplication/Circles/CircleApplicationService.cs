using System.Transactions;
using System.Linq;
using _02.SnsApplication.Circles.Join;
using _02.SnsApplication.Users;
using _02.SnsDomain.Models.Circles;
using _02.SnsDomain.Models.Users;

namespace _02.SnsApplication.Circles
{
    public class CircleApplicationService
    {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly CircleService circleService;
        private readonly IUserRepository userRepository;

        public CircleApplicationService(
            ICircleFactory circleFactory,
            ICircleRepository circleRepository,
            CircleService circleService,
            IUserRepository userRepository)
        {
            this.circleFactory = circleFactory;
            this.circleRepository = circleRepository;
            this.circleService = circleService;
            this.userRepository = userRepository;
        }

        public void Join(CircleJoinCommand command)
        {
            var circleId = new CircleId(command.CircleId);
            var circle = circleRepository.Find(circleId);

            var users = userRepository.Find(circle.Members);
            // 서클에 소속된 프리미엄 사용자의 수에 따라 최대 인원이 결정됨
            var premiumUserNumber = users.Count(user => user.IsPremium);
            var circleUpperLimit = premiumUserNumber < 10 ? 30 : 50;
            if (circle.CountMembers() >= circleUpperLimit)
            {
                throw new CircleFullException(circleId);
            }

            var memberId = new UserId(command.UserId);
            var member = userRepository.Find(memberId);
            if (member == null)
            {
                throw new UserNotFoundException(memberId, "사용자를 찾지 못했음");
            }

            circle.Join(member);
            circleRepository.Save(circle);
        }
    }
}
