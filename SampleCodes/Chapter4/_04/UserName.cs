using System;

namespace _04
{
    class UserName
    {
        private readonly string value;

        public UserName(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 3) throw new ArgumentException("사용자명은 3글자 이상이어야 함", nameof(value));

            this.value = value;
        }

        public string Value => value;
    }
}
