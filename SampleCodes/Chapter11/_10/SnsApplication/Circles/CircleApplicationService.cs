using System.Transactions;
using _10.SnsApplication.Circles.Create;
using _10.SnsApplication.Circles.Join;
using _10.SnsApplication.Users;
using _10.SnsDomain.Models.Circles;
using _10.SnsDomain.Models.Users;

namespace _10.SnsApplication.Circles
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

        public void Create(CircleCreateCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var ownerId = new UserId(command.UserId);
                var owner = userRepository.Find(ownerId);
                if (owner == null)
                {
                    throw new UserNotFoundException(ownerId, "서클장이 될 사용자가 없음");
                }

                var name = new CircleName(command.Name);
                var circle = circleFactory.Create(name, owner);
                if (circleService.Exists(circle))
                {
                    throw new CanNotRegisterCircleException(circle, "이미 등록된 서클임");
                }

                circleRepository.Save(circle);
                transaction.Complete();
            }
        }

        public void Join(CircleJoinCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var memberId = new UserId(command.UserId);
                var member = userRepository.Find(memberId);
                if (member == null)
                {
                    throw new UserNotFoundException(memberId, "사용자를 찾지 못했음");
                }

                var id = new CircleId(command.CircleId);
                var circle = circleRepository.Find(id);
                if (circle == null)
                {
                    throw new CircleNotFoundException(id, "서클을 찾지 못했음");
                }

                // 서클에 소속된 사용자가 서클장을 포함 30명 이하인지 확인
                if (circle.Members.Count >= 29)
                {
                    throw new CircleFullException(id);
                }

                // 가입 처리
                circle.Members.Add(member);
                circleRepository.Save(circle);
                transaction.Complete();
            }
        }
    }
}
