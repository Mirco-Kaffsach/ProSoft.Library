using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProSoft.Library.Core.Models;
using ProSoft.Library.Data.Postgres;
using ProSoft.Library.Data.Postgres.Engines;
using ProSoft.Library.UnitTest.Utils;
using ProSoft.Library.UnitTest.Utils.EntityFramework;
using Xunit;

namespace ProSoft.Library.Data.UnitTests.Engines;

[ExcludeFromCodeCoverage]
public class TagEngineTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Check(bool isLogLevelEnabled)
    {
        // Arrange
        var testData = new List<Tag>
        {
            new Tag { Id = 1, Name = "TestTag1" },
            new Tag { Id = 2, Name = "TestTag2" }
        }.AsQueryable();

        // Mock DbSet<Tag>
        var mockDbSet = new Mock<DbSet<Tag>>();
        var mockDbContext = new Mock<LibraryDbContext>();

        mockDbSet.As<IQueryable<Tag>>()
                 .Setup(m => m.Provider)
                 .Returns(testData.Provider);

        mockDbSet.As<IQueryable<Tag>>()
                 .Setup(m => m.Expression)
                 .Returns(testData.Expression);
        
        mockDbSet.As<IQueryable<Tag>>()
                 .Setup(m => m.ElementType)
                 .Returns(testData.ElementType);
        
        mockDbSet.As<IQueryable<Tag>>()
                 .Setup(m => m.GetEnumerator())
                 .Returns(testData.GetEnumerator());

        mockDbSet.As<IAsyncEnumerable<Tag>>()
                 .Setup(m => m.GetAsyncEnumerator(default))
                 .Returns(new TestAsyncEnumerator<Tag>(testData.GetEnumerator()));

        mockDbSet.As<IQueryable<Tag>>()
                 .Setup(m => m.Provider)
                 .Returns(new TestAsyncQueryProvider<Tag>(testData.Provider));

        mockDbContext.Setup(c => c.Set<Tag>())
                     .Returns(mockDbSet.Object);

        var mockLogger = MockBuilder.CreateLoggerMock<TagEngine>(isLogLevelEnabled);
        var tagEngine = new TagEngine(mockLogger.Object, mockDbContext.Object);

        // Act: Call GetAll
        var result = tagEngine.GetAll().ToList();

        // Assert: Verify results
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("TestTag1", result[0].Name);
        Assert.Equal("TestTag2", result[1].Name);
    }
}
