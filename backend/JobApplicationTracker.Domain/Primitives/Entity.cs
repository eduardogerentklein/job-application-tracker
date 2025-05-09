namespace Domain.Primitives
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public required TKey Id { get; set; }
    }
}
