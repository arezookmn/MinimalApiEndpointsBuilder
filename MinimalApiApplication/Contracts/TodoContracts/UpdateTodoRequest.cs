namespace MinimalApiApplication.Contracts.TodoContracts;

public record UpdateTodoRequest(string Name, bool IsComplete);
