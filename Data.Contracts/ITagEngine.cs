using ProSoft.Library.Core.Models;

namespace ProSoft.Library.Data.Contracts;

public interface ITagEngine : IDisposable
{
    IQueryable<Tag> GetAll();
    Task<Tag?> GetAsync(Guid systemId, CancellationToken cancellationToken);
}