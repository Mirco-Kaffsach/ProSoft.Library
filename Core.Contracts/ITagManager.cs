using ProSoft.Library.Core.Models;

namespace ProSoft.Library.Core.Contracts;

public interface ITagManager : IDisposable
{
    Task<IQueryable<Tag>> GetTagListAsync(CancellationToken cancellationToken);

    Task<Tag?> GetTagAsync(Guid systemId, CancellationToken cancellationToken);
}