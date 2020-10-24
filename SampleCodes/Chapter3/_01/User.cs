using System;

namespace _01
{
    class User
    {
        private string name;

        public User(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length < 3) throw new ArgumentException("사용자명은 3글자 이상이어야 함", nameof(name));

            this.name = name;
        }
    }
}
