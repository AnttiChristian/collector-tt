namespace Collector.Collection;

public abstract class Entity
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;
}
