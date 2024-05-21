using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Core.AdvancedFunctionnal;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;

[assembly: InternalsVisibleTo("BDM.ReferencialMachine.DataAccess.UnitTest")]
namespace BDM.ReferencialMachine.Core.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository; 
        private readonly IClauseByMachineRepository _clauseByMachineRepository;

        public MachineService(IMachineRepository machineRepository, IClauseByMachineRepository clauseByMachineRepository)
        {
            _machineRepository = machineRepository;
            _clauseByMachineRepository = clauseByMachineRepository;
        }

        public async Task<ICollection<MachineSpecification>> ReadAllMachinesAsync(string product)
        {
            return await _machineRepository.ReadAllMachinesAsync(product);
        }

        public async Task<MachineSpecification> ReadMachineAsync(string machineCode)
        {
            return await _machineRepository.ReadMachineAsync(machineCode);
        }

        public async Task CreateMachineAsync(MachineSpecification machineSpecification)
        {
            SearchForDuplicateEditionClause(machineSpecification);

            await _machineRepository.CreateMachineAsync(machineSpecification);
        }

        public async Task UpdateMachineAsync(string machineCode, MachineSpecification machineSpecification)
        {
            if (!string.IsNullOrEmpty(machineSpecification?.Code) && machineCode != machineSpecification.Code)
            {
                throw new BadRequestException(ExceptionConstants.MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_CODE,
                    ExceptionConstants.MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_MESSAGE);
            }

            SearchForDuplicateEditionClause(machineSpecification);

            await _machineRepository.UpdateMachineAsync(machineCode, machineSpecification);
        }
        
        private static void SearchForDuplicateEditionClause(MachineSpecification machineSpecification)
        {
            if (machineSpecification?.EditionClauses == null)
            {
                return;
            }
            
            var duplicatedCodes = machineSpecification.EditionClauses
                .GroupBy(i => i.Code)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
            
            if (!duplicatedCodes.Any())
            {
                return;
            }

            throw new BadRequestException(ExceptionConstants.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_CODE,
                ExceptionConstants.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_MESSAGE(duplicatedCodes));
        }

        public async Task<ICollection<MachineSpecification>> ReadMachineListAsync(IEnumerable<string> machineCodes)
        {
            if (machineCodes == null)
            {
                return new List<MachineSpecification>();
            }
            return await _machineRepository.ReadMachineListAsync(machineCodes.ToArray());
        }

        public async Task<ICollection<EditionClausesByMachine>> ReadMachineClausesListAsync(IEnumerable<string> machineCodes)
        {
            if (machineCodes == null)
            {
                return new List<EditionClausesByMachine>();
            }
            return await _clauseByMachineRepository.ReadMachineClausesListAsync(machineCodes);
        }
    }
}
