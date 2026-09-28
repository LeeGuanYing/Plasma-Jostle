using System;

namespace PokemonJason.Core
{
    /// <summary>
    /// Stable identity of an entity across runtime sessions.
    /// Equality is determined by the GUID value only.
    /// </summary>
    [Serializable]
    public readonly struct PersistentId : IEquatable<PersistentId>
    {
        private readonly string value;

        private PersistentId(string value)
        {
            this.value = value;
        }

        public bool IsValid => !string.IsNullOrEmpty(value);

        public static PersistentId CreateNew()
        {
            return new PersistentId(Guid.NewGuid().ToString("N"));
        }

        public static bool TryParse(string value, out PersistentId id)
        {
            if (Guid.TryParse(value, out Guid guid))
            {
                id = new PersistentId(guid.ToString("N"));
                return true;
            }

            id = default;
            return false;
        }

        public bool Equals(PersistentId other)
        {
            return string.Equals(value, other.value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            return obj is PersistentId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value == null ? 0 : StringComparer.Ordinal.GetHashCode(value);
        }

        public override string ToString()
        {
            return value ?? string.Empty;
        }

        public static bool operator ==(PersistentId left, PersistentId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PersistentId left, PersistentId right)
        {
            return !left.Equals(right);
        }
    }
}
