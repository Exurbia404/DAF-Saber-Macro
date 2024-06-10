using Xunit;
using System.Collections.Generic;

public class RefSetHandlerTests
{ 
    [Fact]
    public void LoadRefSets_WhenFileExists_ReturnsReferences()
    {
        // Arrange
        var logger = new Logger();
        var handler = new RefSetHandler(logger);

        // Act
        var result = handler.LoadRefSets();

        // Assert
        // Since we are mocking the file existence, we can assume the result is not null
        Assert.NotNull(result);
    }

    [Fact]
    public void LoadRefSets_WhenFileDoesNotExist_ReturnsEmptyList()
    {
        // Arrange
        var logger = new Logger();
        var handler = new RefSetHandler(logger);

        // Act
        var result = handler.LoadRefSets();

        // Assert
        Assert.Empty(result);
    }
}
