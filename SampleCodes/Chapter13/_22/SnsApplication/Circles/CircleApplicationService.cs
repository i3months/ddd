using System;
using System.Linq;
using _22.SnsApplication.Circles.GetSummaries;
using _22.SnsDomain.Models.Circles;
using _22.SnsDomain.Models.Users;

namespace _22.SnsApplication.Circles
{
    public class CircleApplicationService
    {
        private readonly ICircleFactory circleFactory;
        private readonly ICircleRepository circleRepository;
        private readonly CircleService circleService;
        private readonly IUserRepository userRepository;
        private readonly DateTime now;

        public CircleApplicationService(
            ICircleFactory circleFactory,
            ICircleRepository circleRepository,
            CircleService circleService,
            IUserRepository userRepository,
            DateTime now)
        {
            this.circleFactory = circleFactory;
            this.circleRepository = circleRepository;
            this.circleService = circleService;
            this.userRepository = userRepository;
            this.now = now;
        }

        public CircleGetSummariesResult GetSummaries(CircleGetSummariesCommand command)
        {
            // 아직은 데이터를 받아오지 않았다
            var all = circleRepository.FindAll();
            // 야기서는 페이징 처리 조건만 부여한 것으로 데이터를 받지는 않았다
            var chunk = all
                .Skip((command.Page - 1) * command.Size)
                .Take(command.Size);
            // 이 시점에서 처음으로 컬렉션의 요소에 접근했으므로 조건에 따라 데이터를 받아온다
            var summaries = chunk
                .Select(x =>
                {
                    var owner = userRepository.Find(x.Owner);
                    return new CircleSummaryData(x.Id.Value, owner.Name.Value);
                })
                .ToList();
            return new CircleGetSummariesResult(summaries);
        }
    }
}
