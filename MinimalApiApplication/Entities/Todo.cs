namespace MinimalApiApplication.Entities;

public class Todo : BaseEntity
{
    public string Name { get; set; }
    public bool IsComplete { get; set; }
}
