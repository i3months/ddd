using System.Transactions;
using SnsApplication.Users;
using SnsApplicationPort.Circles.Join;
using SnsDomain.Models.Circles;
using SnsDomain.Models.Users;

namespace SnsApplication.Circles.Interactors
{
    public class CircleJoinInteractor : ICircleJoinInputPort
    {
        private readonly ICircleRepository circleRepository;
        private readonly IUserRepository userRepository;

        public CircleJoinInteractor(ICircleRepository circleRepository, IUserRepository userRepository)
        {
            this.circleRepository = circleRepository;
            this.userRepository = userRepository;
        }

        public CircleJoinOutputData Handle(CircleJoinInputData inputData)
        {
            using var transaction = new TransactionScope();

            var memberId = new UserId(inputData.MemberId);
            var member = userRepository.Find(memberId);
            if (member == null)
            {
                throw new UserNotFoundException(memberId, "서클에 가입할 사용자를 찾지 못했음");
            }

            var id = new CircleId(inputData.CircleId);
            var circle = circleRepository.Find(id);
            if (circle == null)
            {
                throw new CircleNotFoundException(id, "가입할 서클을 찾지 못했음");
            }

            var fullSpec = new CircleFullSpecification(userRepository);
            if (fullSpec.IsSatisfiedBy(circle))
            {
                throw new CircleFullException(id, "서클에 소속 가능한 최대 인원을 초과함");
            }

            circle.Join(member, fullSpec);
            circleRepository.Save(circle);

            transaction.Complete();

            return new CircleJoinOutputData();
        }
    }
}
