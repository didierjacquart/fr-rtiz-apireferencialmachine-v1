using Xunit;
using MC = BDM.ReferencialMachine.Core.Constants.MachineConstants; 

namespace BDM.ReferencialMachine.Core.UnitTest.Constants
{
    public class MachineConstantsTests
    {
        [Fact]
        public void Should_Constants_HaveTheGoodValues()
        {
            //Arrange
            var numberOfConstants = typeof(MC).GetFields().Length;

            //Act
            //Assert
            Assert.Equal("PARK", MC.PARK);
            Assert.Equal("INVENTAIRE", MC.INVENTORY);
            Assert.Equal("PHOTOVOLTAIC", MC.PHOTOVOLTAIC);
            Assert.Equal("RENTAL_MACHINE", MC.RENTAL_MACHINE);

            Assert.Equal(4, numberOfConstants);
        }
    }
}
