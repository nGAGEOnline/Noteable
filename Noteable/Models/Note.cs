namespace Noteable.Models;

public class Note
{
	public int Id { get; set; }

	public string? Title { get; set; }
	public string? Content { get; set; }
	public DateTime Created { get; init; } = DateTime.Now;
	public DateTime LastUpdated { get; set; } = DateTime.Now;

	public int NotebookId { get; set; }
}