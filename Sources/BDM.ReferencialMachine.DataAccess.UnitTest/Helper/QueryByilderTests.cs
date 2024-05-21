using System;
using System.Linq;
using System.Threading.Tasks;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Helper;
using BDM.ReferencialMachine.DataAccess.Models;
using BDM.ReferencialMachine.DataAccess.UnitTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Helper
{
    public class QueryByilderTests
    {
        private readonly MachineContext _context;
        private readonly Mock<ILogger<IMachineRepository>> _logger;

        public QueryByilderTests()
        {
            var options = new DbContextOptionsBuilder<MachineContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new MachineContext(options);
            _logger = new Mock<ILogger<IMachineRepository>>();
        }

        [Fact]
        public async Task Should_Get_All_Machines_Filter_With_Not_Null_End_Date()
        {
            var dbMachines = MachineBuilder.FakeDbMachine(null).Generate(2);
            dbMachines[0].END_DATETIME_SUBSCRIPTION_PERIOD = DateTime.Now.AddDays(1);
            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            await _context.SaveChangesAsync();

            var query = _context.GetQueryForAllMachines(null);

            T_MACHINE_SPECIFICATION[] tLstMachineSpecification = null;
            await RetryHelper.Retry(_logger.Object).ExecuteAsync(async () =>
            {
                tLstMachineSpecification = await query.ToArrayAsync();
            });


            Assert.Single(tLstMachineSpecification);
            var machineResult = tLstMachineSpecification.First();

            Assert.Equal(dbMachines[1].CODE, machineResult.CODE); 
        }

        [Fact]
        public async Task Should_Get_Machine_With_All_Details_Filter_With_Not_Null_End_Date()
        {
            var dbMachines = MachineBuilder.FakeDbMachine(null).Generate(2);
            dbMachines[0].END_DATETIME_SUBSCRIPTION_PERIOD = DateTime.Now.AddDays(1);
            dbMachines[1].CODE = dbMachines[0].CODE;
            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            await _context.SaveChangesAsync();

            var query = _context.GetQueryForMachineWithAllDetails(dbMachines[0].CODE);

            T_MACHINE_SPECIFICATION[] tLstMachineSpecification = null;
            await RetryHelper.Retry(_logger.Object).ExecuteAsync(async () =>
            {
                tLstMachineSpecification = await query.ToArrayAsync();
            });


            Assert.Single(tLstMachineSpecification);
            var machineResult = tLstMachineSpecification.First();
            
            Assert.Null(machineResult.END_DATETIME_SUBSCRIPTION_PERIOD);
        }

        [Fact]
        public async Task Should_Update_Machine_Affects_Only_Machine_With_Null_End_Date()
        {
            var dbMachines = MachineBuilder.FakeDbMachine(null).Generate(2);
            dbMachines[0].END_DATETIME_SUBSCRIPTION_PERIOD = DateTime.Now.AddDays(1);
            dbMachines[1].CODE = dbMachines[0].CODE;
            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            await _context.SaveChangesAsync();

            var query = _context.GetQueryForUpdate(dbMachines[0].CODE);

            T_MACHINE_SPECIFICATION machineResult = null;
            await RetryHelper.Retry(_logger.Object).ExecuteAsync(async () =>
            {
                machineResult = await query.FirstOrDefaultAsync();
            });

            Assert.Null(machineResult.END_DATETIME_SUBSCRIPTION_PERIOD);
        }

        [Fact]
        public async Task Should_Get_Machine_List_Returns_Only_Machine_With_Null_End_Date()
        {
            var dbMachines = MachineBuilder.FakeDbMachine(null).Generate(2);
            dbMachines[0].END_DATETIME_SUBSCRIPTION_PERIOD = DateTime.Now.AddDays(1);
            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            await _context.SaveChangesAsync();

            var query = _context.GetQueryForMachineList(new [] { dbMachines[0].CODE , dbMachines[1].CODE});

            T_MACHINE_SPECIFICATION[] tLstMachineSpecification = null;
            await RetryHelper.Retry(_logger.Object).ExecuteAsync(async () =>
            {
                tLstMachineSpecification = await query.ToArrayAsync();
            });

            Assert.Single(tLstMachineSpecification);
            var machineResult = tLstMachineSpecification.First();
            
            Assert.Null(machineResult.END_DATETIME_SUBSCRIPTION_PERIOD);
            Assert.Equal(dbMachines[1].CODE, machineResult.CODE);
        }

        [Fact]
        public async Task Should_Get_Machine_Clauses_List_Returns_Only_Clauses_For_Machine_With_Null_End_Date()
        {
            var dbClauses = MachineBuilder.FakeDbEditionCLause().Generate(2);
            var dbMachines = MachineBuilder.FakeDbMachine(dbClauses).Generate(2);
            dbMachines[0].END_DATETIME_SUBSCRIPTION_PERIOD = DateTime.Now.AddDays(1);
            _context.T_MACHINE_SPECIFICATION.AddRange(dbMachines);
            await _context.SaveChangesAsync();

            var query = _context.GetQueryditionClauseLists(new[] { dbMachines[0].CODE, dbMachines[1].CODE });

            ClauseCodesByMachine[] clauseCodesByMachines = null;
            await RetryHelper.Retry(_logger.Object).ExecuteAsync(async () =>
            {
                clauseCodesByMachines = await query.ToArrayAsync();
            });

            Assert.Single(clauseCodesByMachines);
            var clauseCodesByMachineResult = clauseCodesByMachines.First();

            Assert.Equal(dbMachines[1].CODE, clauseCodesByMachineResult.MachineCode);
        }
    }
}
