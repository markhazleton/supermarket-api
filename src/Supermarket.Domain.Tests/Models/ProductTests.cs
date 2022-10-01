using Supermarket.Domain.Models;

namespace Supermarket.Domain.Tests.Models
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var product = new Product();

            // Act


            // Assert
            Assert.IsNotNull(product);
        }
    }
}
