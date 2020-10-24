using System;

namespace _21
{
    public class UserId
    {
        public UserId(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("value가 null이거나 빈 문자열임");

            Value = value;
        }

        public string Value { get; }
    }
}
