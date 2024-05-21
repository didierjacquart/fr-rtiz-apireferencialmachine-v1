using System.Linq;
using BDM.ReferencialMachine.DataAccess.Context;
using BDM.ReferencialMachine.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BDM.ReferencialMachine.DataAccess.Helper
{
    public static class QueryBuilder
    {
        public static IQueryable<T_MACHINE_SPECIFICATION> GetQueryForAllMachines(this MachineContext context, string product)
        {
            return context!.T_MACHINE_SPECIFICATION.AsNoTracking()
                .Include(x => x.T_FAMILY)
                .WhereIf(!string.IsNullOrEmpty(product), machine => machine.PRODUCT == product)
                .Where(machine => machine.END_DATETIME_SUBSCRIPTION_PERIOD == null)
                .Select(machine => new T_MACHINE_SPECIFICATION
                {
                    CODE = machine.CODE,
                    PRODUCT = machine.PRODUCT,
                    DESCRIPTION = machine.DESCRIPTION,
                    IS_DELEGATED = machine.IS_DELEGATED,
                    IS_UNREFERENCED = machine.IS_UNREFERENCED,
                    IS_EXCLUDED = machine.IS_EXCLUDED,
                    IS_OUT_OF_MACHINE_INSURANCE = machine.IS_OUT_OF_MACHINE_INSURANCE,
                    KEYWORDS = machine.KEYWORDS,
                    NAME = machine.NAME,
                    LABEL = machine.LABEL,
                    SCORE = machine.SCORE,
                    T_FAMILY = machine.T_FAMILY
                });
        }

        public static IQueryable<T_MACHINE_SPECIFICATION> GetQueryForMachineWithAllDetails(this MachineContext context, string machineCode)
        {
            return context!.T_MACHINE_SPECIFICATION.AsNoTracking()
                    .Include(x => x.T_FAMILY)
                    .Include(x => x.T_PRICING_RATE)
                    .Where(x =>
                        x.CODE == machineCode &&
                        x.END_DATETIME_SUBSCRIPTION_PERIOD == null);
        }

        public static IQueryable<T_MACHINE_SPECIFICATION> GetQueryForUpdate(this MachineContext context, string machineCode)
        {
            return context!.T_MACHINE_SPECIFICATION
                .Include(x => x.T_FAMILY)
                .Include(x => x.T_PRICING_RATE)
                .Where(x =>
                    x.CODE == machineCode &&
                    x.END_DATETIME_SUBSCRIPTION_PERIOD == null);
        }

        public static IQueryable<T_MACHINE_SPECIFICATION> GetQueryForMachineList(this MachineContext context, string[] machineCodes)
        {
            return context!.T_MACHINE_SPECIFICATION.AsNoTracking()
                .Include(x => x.T_PRICING_RATE)
                .Include(x => x.T_FAMILY)
                .Where(x =>
                    machineCodes.Contains(x.CODE) && 
                    x.END_DATETIME_SUBSCRIPTION_PERIOD == null);
        }

        public static IQueryable<ClauseCodesByMachine> GetQueryditionClauseLists(this MachineContext context, string[] machineCodes)
        {
            var separator = new[] { '|' };
            return context!.T_MACHINE_SPECIFICATION
                .AsNoTracking()
                .Where(x =>
                    machineCodes.Contains(x.CODE) && 
                    !string.IsNullOrEmpty(x.EDITION_CLAUSE_CODES) && 
                    x.END_DATETIME_SUBSCRIPTION_PERIOD == null )
                .Select(x =>
                    new ClauseCodesByMachine
                    {
                        MachineCode = x.CODE,
                        EditionClauseCodes = x.EDITION_CLAUSE_CODES.Split(separator)
                    });
        }
    }
}
