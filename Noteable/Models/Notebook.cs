namespace Noteable.Models;

public class Notebook
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public List<Note> Notes { get; set; } = new();
	
	public DateTime Created { get; init; } = DateTime.Now;
	public DateTime LastUpdated { get; set; } = DateTime.Now;
}