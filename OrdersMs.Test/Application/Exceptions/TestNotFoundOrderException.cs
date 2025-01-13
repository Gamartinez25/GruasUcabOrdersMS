using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersMS.Application.Exceptions;

namespace OrdersMs.Test.Application.Exceptions
{
    public class TestNotFoundOrderException
    {
        [Fact]
        public void Constructor_ShouldSetMessage()
        {
            // Arrange
            string expectedMessage = "Order not found";

            // Act
            var exception = new NotFoundOrderException(expectedMessage);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
