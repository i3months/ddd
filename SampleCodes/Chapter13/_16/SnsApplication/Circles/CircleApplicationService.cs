using System;
using System.Linq;
using _16.SnsApplication.Circles.GetRecommend;
using _16.SnsDomain.Models.Circles;
using _16.SnsDomain.Models.Users;

namespace _16.SnsApplication.Circles
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

        public CircleGetRecommendResult GetRecommend(CircleGetRecommendRequest request)
        {
            var circleRecommendSpecification = new CircleRecommendSpecification(now);
            // 리포지토리에 명세를 전달해 추천 서클을 추려냄(필터링)
            var recommendCircles = circleRepository.Find(circleRecommendSpecification)
                .Take(10)
                .ToList();

            return new CircleGetRecommendResult(recommendCircles);
        }
    }
}
