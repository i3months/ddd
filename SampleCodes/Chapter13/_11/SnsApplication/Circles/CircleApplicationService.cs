using System;
using _11.SnsApplication.Circles.GetRecommend;
using _11.SnsApplication.Circles.Join;
using _11.SnsApplication.Users;
using _11.SnsDomain.Models.Circles;
using _11.SnsDomain.Models.Users;

namespace _11.SnsApplication.Circles
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
            // 리포지토리에 모두 맡기면 된다
            var recommendCircles = circleRepository.FindRecommended(now);
            return new CircleGetRecommendResult(recommendCircles);
        }
    }
}
