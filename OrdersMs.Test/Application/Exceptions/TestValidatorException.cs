using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersMS.Application.Exceptions;

namespace OrdersMs.Test.Application.Exceptions
{
    public class TestValidatorException
    {
        [Fact]
        public void Constructor_WithMessage_ShouldSetMessage()
        {
            // Arrange
            string expectedMessage = "Validation failed";

            // Act
            var exception = new ValidatorException(expectedMessage);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Null(exception.InnerException);
        }
        [Fact]
        public void Constructor_WithMessageAndInnerException_ShouldSetProperties()
        {
            // Arrange
            string expectedMessage = "Validation failed";
            var innerException = new Exception("Inner exception");

            // Act
            var exception = new ValidatorException(expectedMessage, innerException);

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }

    }
}
