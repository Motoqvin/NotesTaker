using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Repositories;
public interface IHttpLogRepository
{
    public Task InsertAsync(HttpLog log);
}