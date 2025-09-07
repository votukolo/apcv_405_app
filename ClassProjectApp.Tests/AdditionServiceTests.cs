using Xunit;
using ClassProjectApp.Services;

namespace ClassProjectApp.Tests
{
    public class AdditionServiceTests
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-1, -2, -3)]
        [InlineData(-1, 1, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(int.MaxValue, 0, int.MaxValue)]
        [InlineData(int.MinValue, 0, int.MinValue)]
        public void Add_ReturnsExpectedResult(int a, int b, int expected)
        {
            // Arrange
            var service = new AdditionService();

            // Act
            var result = service.Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}