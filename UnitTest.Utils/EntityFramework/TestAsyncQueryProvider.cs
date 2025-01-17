using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace ProSoft.Library.UnitTest.Utils.EntityFramework;

/// <summary>
/// Class TestAsyncQueryProvider.
/// Implements the <see cref="IAsyncQueryProvider" />
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
/// <seealso cref="IAsyncQueryProvider" />
[ExcludeFromCodeCoverage]
public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestAsyncQueryProvider{TEntity}"/> class.
    /// </summary>
    /// <param name="inner">The inner.</param>
    public TestAsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    /// <summary>
    /// Constructs an <see cref="T:System.Linq.IQueryable" /> object that can evaluate the query represented by a specified expression tree.
    /// </summary>
    /// <param name="expression">An expression tree that represents a LINQ query.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable" /> that can evaluate the query represented by the specified expression tree.</returns>
    public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);

    /// <summary>
    /// Constructs an <see cref="T:System.Linq.IQueryable`1" /> object that can evaluate the query represented by a specified expression tree.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements of the <see cref="T:System.Linq.IQueryable`1" /> that is returned.</typeparam>
    /// <param name="expression">An expression tree that represents a LINQ query.</param>
    /// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that can evaluate the query represented by the specified expression tree.</returns>
    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);

    /// <summary>
    /// Executes the query represented by a specified expression tree.
    /// </summary>
    /// <param name="expression">An expression tree that represents a LINQ query.</param>
    /// <returns>The value that results from executing the specified query.</returns>
    public object Execute(Expression expression) => _inner.Execute(expression);

    /// <summary>
    /// Executes the strongly-typed query represented by a specified expression tree.
    /// </summary>
    /// <typeparam name="TResult">The type of the value that results from executing the query.</typeparam>
    /// <param name="expression">An expression tree that represents a LINQ query.</param>
    /// <returns>The value that results from executing the specified query.</returns>
    public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);

    /// <summary>
    /// Executes the strongly-typed query represented by a specified expression tree asynchronously.
    /// </summary>
    /// <typeparam name="TResult">The type of the t result.</typeparam>
    /// <param name="expression">The expression.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>TResult.</returns>
    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var resultType = typeof(TResult).GetGenericArguments()[0];
        var executionResult = typeof(IQueryProvider)
                              .GetMethod(
                                  name: nameof(IQueryProvider.Execute),
                                  genericParameterCount: 1,
                                  types: new[] { typeof(Expression) })
                              ?.MakeGenericMethod(resultType)
                              .Invoke(_inner, new[] { expression });

        return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                                    ?.MakeGenericMethod(resultType)
                                    .Invoke(null, new[] { executionResult });
    }
}