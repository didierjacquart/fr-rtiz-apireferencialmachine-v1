using System.Collections.Generic;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Mappers
{
    public class MapperEditionClausesTests
    {
        private readonly IMapperEditionClauses _mapper;

        public MapperEditionClausesTests()
        {
            _mapper = new MapperEditionClauses();
        }

        [Fact]
        public void Should_Map_EditionClause_To_DbObject()
        {
            var result = _mapper.Map(EditionClauses_Sample.GetEditionClauses());
            var expected = T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses();

            Assert.NotNull(result);

            foreach (var tExpectedEditionClause in expected)
            {
                var currentEditionClause = result.FirstOrDefault(x => x.CODE == tExpectedEditionClause.CODE);
                Assert.NotNull(currentEditionClause);
                Assert.Equal(tExpectedEditionClause.LABEL, currentEditionClause.LABEL);
                Assert.Equal(tExpectedEditionClause.DESCRIPTION, currentEditionClause.DESCRIPTION);
                Assert.Equal(tExpectedEditionClause.TYPE, currentEditionClause.TYPE);
            }
        }

        [Fact]
        public void Should_NotMap_EditionClause_To_DbObject_When_Input_NUll()
        {
            var result = _mapper.Map((IEnumerable<EditionClause>)null);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_Map_DBObject_To_EditionClause()
        {
            var result = _mapper.Map(T_EDITION_CLAUSE_Sample.GetDatabaseEditionClauses());
            var expected = EditionClauses_Sample.GetEditionClauses();

            Assert.NotNull(result);

            foreach (var expectedEditionClause in expected)
            {
                var currentEditionClause = result.FirstOrDefault(x => x.Code == expectedEditionClause.Code);
                Assert.NotNull(currentEditionClause);
                Assert.Equal(expectedEditionClause.Label, currentEditionClause.Label);
                Assert.Equal(expectedEditionClause.Description, currentEditionClause.Description);
                Assert.Equal(expectedEditionClause.Type, currentEditionClause.Type);
            }
        }

        [Fact]
        public void Should_NotMapDatabase_When_Input_Is_Null()
        {
            var result = _mapper.Map((List<T_EDITION_CLAUSE>)null);

            Assert.Null(result);
        }

        [Fact]
        public void Should_NotMapDatabase_When_Input_Contains_Null_Elements()
        {
            var result = _mapper.Map(new List<T_EDITION_CLAUSE>
            {
                null,
                new T_EDITION_CLAUSE{ CODE = "CODE"}
            });
            Assert.Single(result);
        }
    }
}
