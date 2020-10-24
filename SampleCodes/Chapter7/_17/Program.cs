using Microsoft.Extensions.DependencyInjection;

namespace _17
{
    class Program
    {
        static void Main(string[] args)
        {
            // IoC Container
            var serviceCollection = new ServiceCollection();
            // 의존 관계 해소를 위한 설정 등록
            serviceCollection.AddTransient<IUserRepository, InMemoryUserRepository>();
            serviceCollection.AddTransient<UserApplicationService>();

            // IoC Container를 통해 필요한 인스턴스를 받아옴
            var provider = serviceCollection.BuildServiceProvider();
            var userApplicationService = provider.GetService<UserApplicationService>();
        }
    }
}
