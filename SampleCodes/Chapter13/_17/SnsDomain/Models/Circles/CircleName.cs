using System;

namespace _17.SnsDomain.Models.Circles
{
    public class CircleName : IEquatable<CircleName>
    {
        public CircleName(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 3) throw new ArgumentException("서클명은 3글자 이상이어야 함", nameof(value));
            if (value.Length > 20) throw new ArgumentException("서클명은 20글자 이하이어야 함", nameof(value));

            Value = value;
        }

        public string Value { get; }

        public bool Equals(CircleName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CircleName) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}