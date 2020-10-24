using System;
using System.Collections.Generic;
using System.Text;
using _16.Domain.Models.Users;
using _16.Domain.Shared;

namespace _16
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        // 인메모리 리포지토리를 사용함
        private InMemoryUserRepository userRepository;

        public IUserRepository UserRepository
        {
            get => userRepository ?? (userRepository = new InMemoryUserRepository());
        }

        public void Commit()
        {
            userRepository?.Commit();
        }
    }
}
