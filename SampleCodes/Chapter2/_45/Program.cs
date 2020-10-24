using System;

namespace _45
{
    class Program
    {
        void CreateUser(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length < 3) throw new ArgumentException("사용자명은 3글자 이상이어야 함", nameof(name));

            var user = new User(name);

            // ...
        }

        void UpdateUser(string id, string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length < 3) throw new ArgumentException("사용자명은 3글자 이상이어야 함", nameof(name));
            
            // ...
        }
    }
}
