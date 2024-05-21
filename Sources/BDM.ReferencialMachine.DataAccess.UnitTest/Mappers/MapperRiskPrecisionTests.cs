using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Models;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Mappers
{
    public class MapperRiskPrecisionTests
    {
        private readonly Fixture _fixture;
        private readonly IMapperRiskPrecision _mapper;

        public MapperRiskPrecisionTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _mapper = new MapperRiskPrecision();

        }

        [Fact]
        public void Should_Map_ReturnsRiskPrecisions_When_Nominal()
        {

            //Arrange
            var dbRiskPrecisions = _fixture.Build<T_RISK_PRECISION>().CreateMany(1).ToArray();

            //Act
            var riskPrecisionResult = _mapper.Map(dbRiskPrecisions);
            
            //Assert
            Assert.Single(riskPrecisionResult);
            Assert.Equal(dbRiskPrecisions.First().CODE, riskPrecisionResult.FirstOrDefault()?.Code);
            Assert.Equal(dbRiskPrecisions.First().LABEL, riskPrecisionResult.FirstOrDefault()?.Label);
            Assert.Equal(dbRiskPrecisions.First().DETAIL, riskPrecisionResult.FirstOrDefault()?.Detail);
            Assert.Null(riskPrecisionResult.FirstOrDefault()?.MachineCode);
        }

        [Fact]
        public void Should_Map_ReturnsNull_When_InputIsNull()
        {
            //Act
            var riskPrecisionResult = _mapper.Map(null);

            //Assert
            Assert.Null(riskPrecisionResult);
        }

        [Fact]
        public void Should_Map_ReturnsNull_When_InputIsEmpty()
        {
            //Act
            var riskPrecisionResult = _mapper.Map(new List<T_RISK_PRECISION>());

            //Assert
            Assert.Null(riskPrecisionResult);
        }
    }
}
