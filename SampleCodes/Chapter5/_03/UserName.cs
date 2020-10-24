using System;

namespace _03
{
    class UserName
    {
        public UserName(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 3) throw new ArgumentException("사용자명은 3글자 이상이어야 함", nameof(value));

            Value = value;
        }

        public string Value { get; }
    }
}
