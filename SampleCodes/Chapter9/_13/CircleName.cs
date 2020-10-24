using System;

namespace _13
{
    public class CircleName
    {
        public CircleName(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 3) throw new ArgumentException("서클명은 3글자 이상이어야 함");
            if (value.Length < 20) throw new ArgumentException("서클명은 20글자 이하이어야 함");

            Value = value;
        }

        public string Value { get; }
    }
}
