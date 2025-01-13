using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersMS.Domain.Services;

namespace OrdersMs.Test.Domain
{
    public class TestObtenerCoordenadas
    {
        [Fact]
        public void SepararCoordenadas_InvalidInput_ThrowsArgumentException()
        {
            // Arrange
            var input = "10.5050;66.9188"; // Separador incorrecto

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ObtenerCoordenadas.SepararCoordenadas(input));
        }
        [Fact]
        public void SepararCoordenadas_NonNumericInput_ThrowsArgumentException()
        {
            // Arrange
            var input = "abc,def"; // No numérico

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ObtenerCoordenadas.SepararCoordenadas(input));
        }

    }
}
