using Noteable.Models;

namespace Noteable.Services;

public class InMemoryNoteRepository : INoteRepository
{
	private List<Notebook> Notebooks { get; set; } = new ();
	private int _nextNotebookId = 1;
	private int _nextNoteId = 1;

	public async Task<Notebook?> GetNotebookAsync(int id)
	{
		var notebook = Notebooks.FirstOrDefault(n => n.Id == id);
		return await Task.FromResult(notebook);
	}
	public async Task<List<Notebook>> GetNotebooksAsync() => await Task.FromResult(Notebooks);
	public async Task<Notebook> AddNotebookAsync(Notebook notebook)
	{
		var notebookToAdd = new Notebook
		{
			Id = _nextNotebookId++,
			Name = notebook.Name,
			Created = DateTime.Now,
			LastUpdated = DateTime.Now
		};
		
		var existingNotebook = Notebooks.FirstOrDefault(n => n.Id == notebookToAdd.Id);
		if (existingNotebook != null)
			throw new Exception("Notebook already exists");
		
		Notebooks.Add(notebookToAdd);
		return await Task.FromResult(notebookToAdd);
	}
	public async Task<Notebook> UpdateNotebookAsync(Notebook notebook)
	{
		var existingNotebook = Notebooks.FirstOrDefault(n => n.Id == notebook.Id);
		if (existingNotebook == null)
			throw new Exception("Notebook does not exist");
		
		existingNotebook.Name = notebook.Name;
		existingNotebook.LastUpdated = DateTime.Now;
		existingNotebook.Notes = notebook.Notes;
		return await Task.FromResult(existingNotebook);
	}
	public async Task DeleteNotebookAsync(int id)
	{
		var existingNotebook = Notebooks.FirstOrDefault(n => n.Id == id);
		if (existingNotebook == null)
			throw new Exception("Notebook does not exist");
		
		await Task.Run(() => Notebooks.Remove(existingNotebook));
	}

	public async Task<Note> GetNoteAsync(int id)
	{
		var notes = Notebooks.SelectMany(n => n.Notes);
		var existingNote = notes.FirstOrDefault(n => n.Id == id);
		if (existingNote == null)
			throw new Exception("Note does not exist");

		return await Task.FromResult(existingNote);
	}
	public async Task<List<Note>> GetNotesAsync(int notebookId)
	{
		var existingNotebook = Notebooks.FirstOrDefault(n => n.Id == notebookId);
		if (existingNotebook == null)
			throw new Exception("Notebook does not exist");
		
		return await Task.FromResult(existingNotebook.Notes);
	}
	public async Task<Note> AddNoteAsync(Note note)
	{
		var notebook = Notebooks.FirstOrDefault(n => n.Id == note.NotebookId);
		if (notebook is null)
			throw new Exception("Notebook not found");

		note.Id = _nextNoteId++;
		note.LastUpdated = DateTime.Now;

		var existingNote = notebook.Notes.FirstOrDefault(n => n.Id == note.Id);
		if (existingNote != null)
			throw new Exception($"Note with id: [{existingNote.Id}] already exists");

		notebook.Notes.Add(note);
		return await Task.FromResult(note);
	}
	public async Task<Note> UpdateNoteAsync(Note note)
	{
		var existingNote = Notebooks.SelectMany(n => n.Notes).FirstOrDefault(n => n.Id == note.Id);
		if (existingNote == null)
			throw new Exception("Note does not exist");
		
		existingNote.Title = note.Title;
		existingNote.Content = note.Content;
		existingNote.LastUpdated = DateTime.Now;
		return await Task.FromResult(existingNote);
	}
	public async Task DeleteNoteAsync(int id)
	{
		var existingNote = Notebooks.SelectMany(n => n.Notes).FirstOrDefault(n => n.Id == id);
		if (existingNote == null)
			throw new Exception("Note does not exist");
		
		var notebook = Notebooks.FirstOrDefault(b => b.Id == existingNote.NotebookId);
		if (notebook == null)
			throw new Exception("Notebook does not exist");
		
		notebook.Notes.Remove(existingNote);
		await Task.CompletedTask;
	}
}