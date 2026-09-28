using System;

namespace PokemonJason.Core
{
    /// <summary>
    /// Persistent identity data for an entity.
    /// The GUID is the actual identity; the label is human-readable metadata.
    /// </summary>
    [Serializable]
    public readonly struct PersistentIdentity : IEquatable<PersistentIdentity>
    {
        public PersistentId Id { get; }
        public string Label { get; }

        public bool IsValid => Id.IsValid;

        public PersistentIdentity(PersistentId id, string label)
        {
            Id = id;
            Label = label ?? string.Empty;
        }

        public bool Equals(PersistentIdentity other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is PersistentIdentity other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            if (!IsValid)
            {
                return string.Empty;
            }

            return string.IsNullOrWhiteSpace(Label)
                ? Id.ToString()
                : $"{Label}__{Id}";
        }

        public static bool operator ==(PersistentIdentity left, PersistentIdentity right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PersistentIdentity left, PersistentIdentity right)
        {
            return !left.Equals(right);
        }
    }
}
