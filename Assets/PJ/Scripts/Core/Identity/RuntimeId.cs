using System;

namespace PokemonJason.Core
{
    /// <summary>
    /// Identifies an entity only within the current runtime session.
    /// </summary>
    [Serializable]
    public readonly struct RuntimeId : IEquatable<RuntimeId>
    {
        private readonly string value;

        private RuntimeId(string value)
        {
            this.value = value;
        }

        public bool IsValid => !string.IsNullOrEmpty(value);

        public static RuntimeId CreateNew()
        {
            return new RuntimeId(Guid.NewGuid().ToString("N"));
        }

        public bool Equals(RuntimeId other)
        {
            return string.Equals(value, other.value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is RuntimeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value == null ? 0 : StringComparer.Ordinal.GetHashCode(value);
        }

        public override string ToString()
        {
            return value ?? string.Empty;
        }

        public static bool operator ==(RuntimeId left, RuntimeId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RuntimeId left, RuntimeId right)
        {
            return !left.Equals(right);
        }
    }
}
