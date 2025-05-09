namespace Domain.Primitives;

public interface IEntity<TKey>
{
    TKey Id { get; }
}
