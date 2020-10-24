using System.Transactions;
using SnsApplication.Users;
using SnsApplicationPort.Circles.Create;
using SnsDomain.Models.Circles;
using SnsDomain.Models.Users;
using SnsDomain.Services;

namespace SnsApplication.Circles.Interactors
{
    public class CircleCreateInteractor : ICircleCreateInputPort
    {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly IUserRepository userRepository;
        private readonly CircleService circleService;

        public CircleCreateInteractor(ICircleFactory circleFactory, ICircleRepository circleRepository, IUserRepository userRepository, CircleService circleService)
        {
            this.circleFactory = circleFactory;
            this.circleRepository = circleRepository;
            this.userRepository = userRepository;
            this.circleService = circleService;
        }

        public CircleCreateOutputData Handle(CircleCreateInputData inputData)
        {
            using var transaction = new TransactionScope();

            var ownerId = new UserId(inputData.OwnerId);
            var owner = userRepository.Find(ownerId);
            if (owner == null)
            {
                throw new UserNotFoundException(ownerId, "서클장이 될 사용자가 없음");
            }

            var name = new CircleName(inputData.Name);
            var circle = circleFactory.Create(name, owner);
            if (circleService.Exists(circle))
            {
                throw new CanNotRegisterCircleException(circle, "이미 등록된 서클임");
            }

            circleRepository.Save(circle);

            transaction.Complete();

            return new CircleCreateOutputData(circle.Id.Value);
        }
    }
}
