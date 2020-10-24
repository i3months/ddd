using System;
using _01_to_03.Application.Users;
using _01_to_03.Application.Users.Register;
using _01_to_03.InMemoryInfrastructure.Users;
using _01_to_03.Models.Users;
using Microsoft.Extensions.DependencyInjection;

namespace _01_to_03
{
    class Program
    {
        private static ServiceProvider serviceProvider;

        public static void Main(string[] args)
        {
            Startup();
            while (true)
            {
                Console.WriteLine("Input user name");
                Console.Write(">");
                var input = Console.ReadLine();
                var userApplicationService = serviceProvider.GetService<UserApplicationService>();
                var command = new UserRegisterCommand(input);
                userApplicationService.Register(command);

                Console.WriteLine("-------------------------");
                Console.WriteLine("user created:");
                Console.WriteLine("-------------------------");
                Console.WriteLine("user name:");
                Console.WriteLine("- " + input);
                Console.WriteLine("-------------------------");
                Console.WriteLine("continue? (y/n)");
                Console.Write(">");
                var yesOrNo = Console.ReadLine();
                if (yesOrNo == "n")
                {
                    break;
                }
            }
        }

        private static void Startup()
        {
            // IoC Container
            var serviceCollection = new ServiceCollection();
            // 의존 관계 등록 (주석으로 보충 설명)
            // IUserRepository가 처음 필요해지면 InMemoryUserRepository를 생성해
            // 전달함 (생성된 인스턴스는 이후로도 다시 씀)
            serviceCollection.AddSingleton<IUserRepository, InMemoryUserRepository>();
            // UserService가 필요해지면 매번 인스턴스를 생성해 전달함
            serviceCollection.AddTransient<UserService>();
            // UserApplicationService가 필요해지면 매번 인스턴스를 생성해 전달함
            serviceCollection.AddTransient<UserApplicationService>();
            // 의존 관계 해소를 위한 프로바이더 생성
            // 프로그램은 serviceProvider에 의존 관계 해소를 요청함
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
