using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersMS.Application.Exceptions;

namespace OrdersMs.Test.Application.Exceptions
{
    public class TestIdsOrdenNoCoincidenException
    {
        [Fact]
        public void Constructor_ShouldSetMessage()
        {
            // Arrange
            string expectedMessage = "Los IDs de la orden no coinciden.";

            // Act
            var exception = new IdsOrdenNoCoincidenException(expectedMessage);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
