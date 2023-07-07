using Noteable.Models;

namespace Noteable.Services;

public interface INoteRepository
{
	Task<List<Notebook>> GetNotebooksAsync();
	Task<Notebook?> GetNotebookAsync(int id);
	Task<Notebook> AddNotebookAsync(Notebook notebook);
	Task<Notebook> UpdateNotebookAsync(Notebook notebook);
	Task DeleteNotebookAsync(int id);

	Task<List<Note>> GetNotesAsync(int notebookId);
	Task<Note> GetNoteAsync(int id);
	Task<Note> AddNoteAsync(Note note);
	Task<Note> UpdateNoteAsync(Note note);
	Task DeleteNoteAsync(int id);
}