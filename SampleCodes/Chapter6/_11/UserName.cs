using System;

namespace _11
{
    public class UserName
    {
        public UserName(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 3) throw new ArgumentException("사용자명은 3글자 이상이어야 함。", nameof(value));
            if (value.Length > 20) throw new ArgumentException("사용자명은 20글자 이하이어야 함", nameof(value));

            Value = value;
        }

        public string Value { get; }
    }
}
