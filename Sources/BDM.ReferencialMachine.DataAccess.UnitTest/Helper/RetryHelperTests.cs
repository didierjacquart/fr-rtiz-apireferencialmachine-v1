using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Interfaces;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.Repositories;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Helper
{
    public class RetryHelperTests
    {
        private readonly IMachineRepository _machineRepository;
        private readonly Mock<IMapperBusinessToDatabase> _mapperBusinessToDatabase;
        private readonly Mock<MachineContext> _context;
        
        public RetryHelperTests()
        {
            var options = new DbContextOptions<MachineContext>();
            _context = new Mock<MachineContext>(options);
            _mapperBusinessToDatabase = new Mock<IMapperBusinessToDatabase>();

            _machineRepository = new MachineRepository(_context.Object, Mock.Of<IMapperDatabaseToBusiness>(), _mapperBusinessToDatabase.Object, Mock.Of<ILogger<MachineRepository>>());
        }

        [Fact]
        public async Task Should_Retry_When_DbUpdateException_Is_Caught_When_SaveChangesAsync()
        {
            _context.Setup(m => m.T_MACHINE_SPECIFICATION).Returns(new Mock<DbSet<T_MACHINE_SPECIFICATION>>().Object);

            _context.SetupSequence(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Throws(new DbUpdateException())
                .ReturnsAsync(1);

            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification(),
                    T_FAMILY = It.IsAny<IEnumerable<T_FAMILY>>(),
                    T_EDITION_CLAUSE = It.IsAny<IEnumerable<T_EDITION_CLAUSE>>()
                });

            await _machineRepository.CreateMachineAsync(MachineSpecification_Sample.GetMachineSpecification());

            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Should_Retry_When_SqlException_Is_Caught_When_SaveChangesAsync()
        {
            var sqlException = RuntimeHelpers.GetUninitializedObject(typeof(SqlException)) as SqlException;

            _context.Setup(m => m.T_MACHINE_SPECIFICATION).Returns(new Mock<DbSet<T_MACHINE_SPECIFICATION>>().Object);
            _context.SetupSequence(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Throws(sqlException)
                .ReturnsAsync(1);

            _mapperBusinessToDatabase.Setup(x => x.Map(It.IsAny<MachineSpecification>()))
                .Returns(new MachineAndRelatedObjects
                {
                    T_MACHINE_SPECIFICATION = T_MACHINE_SPECIFICATION_Sample.GetDatabaseMachineSpecification(),
                    T_FAMILY = It.IsAny<IEnumerable<T_FAMILY>>(),
                    T_EDITION_CLAUSE = It.IsAny<IEnumerable<T_EDITION_CLAUSE>>()
                });

            await _machineRepository.CreateMachineAsync(MachineSpecification_Sample.GetMachineSpecification());

            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        }
    }
}
