using System.Diagnostics.CodeAnalysis;

namespace ProSoft.Library.UnitTest.Utils.EntityFramework;

/// <summary>
/// Class TestAsyncEnumerator.
/// Implements the <see cref="System.Collections.Generic.IAsyncEnumerator{T}" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.Collections.Generic.IAsyncEnumerator{T}" />
[ExcludeFromCodeCoverage]
public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestAsyncEnumerator{T}"/> class.
    /// </summary>
    /// <param name="inner">The inner.</param>
    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or
    /// resetting unmanaged resources asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    /// <summary>
    /// Advances the enumerator asynchronously to the next element of the collection.
    /// </summary>
    /// <returns>A <see cref="T:System.Threading.Tasks.ValueTask`1" /> that will complete with a result of <c>true</c> if the enumerator
    /// was successfully advanced to the next element, or <c>false</c> if the enumerator has passed the end
    /// of the collection.</returns>
    public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <value>The current.</value>
    public T Current => _inner.Current;
}
