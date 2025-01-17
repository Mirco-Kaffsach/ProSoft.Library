using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ProSoft.Library.UnitTest.Utils.EntityFramework;

/// <summary>
/// Class TestAsyncEnumerable.
/// Implements the <see cref="System.Linq.EnumerableQuery{T}" />
/// Implements the <see cref="System.Collections.Generic.IAsyncEnumerable{T}" />
/// Implements the <see cref="System.Linq.IQueryable{T}" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.Linq.EnumerableQuery{T}" />
/// <seealso cref="System.Collections.Generic.IAsyncEnumerable{T}" />
/// <seealso cref="System.Linq.IQueryable{T}" />
[ExcludeFromCodeCoverage]
public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TestAsyncEnumerable{T}"/> class.
    /// </summary>
    /// <param name="enumerable">A collection to associate with the new instance.</param>
    public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestAsyncEnumerable{T}"/> class.
    /// </summary>
    /// <param name="expression">An expression tree to associate with the new instance.</param>
    public TestAsyncEnumerable(Expression expression) : base(expression)
    {
    }

    /// <summary>
    /// Returns an enumerator that iterates asynchronously through the collection.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> that may be used to cancel the asynchronous iteration.</param>
    /// <returns>An enumerator that can be used to iterate asynchronously through the collection.</returns>
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    /// <summary>
    /// Gets the query provider that is associated with this data source.
    /// </summary>
    /// <value>The provider.</value>
    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
}
