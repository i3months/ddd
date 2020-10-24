using System.Transactions;
using _12.SnsApplication.Circles.Create;
using _12.SnsApplication.Circles.Invite;
using _12.SnsApplication.Circles.Join;
using _12.SnsApplication.Users;
using _12.SnsDomain.Models.CircleInvitations;
using _12.SnsDomain.Models.Circles;
using _12.SnsDomain.Models.Users;

namespace _12.SnsApplication.Circles
{
    public class CircleApplicationService
    {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly ICircleInvitationRepository circleInvitationRepository;
        private readonly CircleService circleService;
        private readonly IUserRepository userRepository;

        public CircleApplicationService(
            ICircleFactory circleFactory,
            ICircleRepository circleRepository,
            ICircleInvitationRepository circleInvitationRepository,
            CircleService circleService,
            IUserRepository userRepository)
        {
            this.circleFactory = circleFactory;
            this.circleRepository = circleRepository;
            this.circleInvitationRepository = circleInvitationRepository;
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

        public void Invite(CircleInviteCommand command)
        {
            using (var transaction = new TransactionScope())
            {
                var fromUserId = new UserId(command.FromUserId);
                var fromUser = userRepository.Find(fromUserId);
                if (fromUser == null)
                {
                    throw new UserNotFoundException(fromUserId, "초대한 사용자를 찾지 못했음");
                }

                var invitedUserId = new UserId(command.InvitedUserId);
                var invitedUser = userRepository.Find(invitedUserId);
                if (invitedUser == null)
                {
                    throw new UserNotFoundException(invitedUserId, "초대받은 사용자를 찾지 못했음");
                }

                var circleId = new CircleId(command.CircleId);
                var circle = circleRepository.Find(circleId);
                if (circle == null)
                {
                    throw new CircleNotFoundException(circleId, "서클을 찾지 못했음");
                }

                // 서클에 소속된 사용자가 서클장을 포함 30명 이하인지 확인
                if (circle.Members.Count >= 29)
                {
                    throw new CircleFullException(circleId);
                }

                var circleInvitation = new CircleInvitation(circle, fromUser, invitedUser);
                circleInvitationRepository.Save(circleInvitation);
                transaction.Complete();
            }
        }

    }
}
