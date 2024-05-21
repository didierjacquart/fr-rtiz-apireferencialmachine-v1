using System.Reflection;
using Xunit;
using EC = BDM.ReferencialMachine.Core.Constants.ExceptionConstants;

namespace BDM.ReferencialMachine.Core.UnitTest.Constants
{
    public class ExceptionConstantsTest
    {
        [Fact]
        public void Should_Constants_HaveTheGoodValues()
        {
            //Arrange
            {
                var exceptionConstantsType = typeof(EC);
                var fields = exceptionConstantsType.GetFields(BindingFlags.Public | BindingFlags.Static);

                //Act
                // Assert
                Assert.Equal("Database update failure", EC.DB_UPDATE_EXCEPTION_MESSAGE);
                Assert.Equal("X01", EC.NOT_AUTHORIZED_PRICING_CODE_VALUE_CODE);
                Assert.Equal("The value for pricingRate.Code (INC) is not supported. The supported values are INC, GTL, BDI, ADE.", EC.NOT_AUTHORIZED_PRICING_CODE_VALUE_MESSAGE("INC"));
                Assert.Equal("X02", EC.NO_MATCHING_FAMILY_EXCEPTION_CODE);
                Assert.Equal("The specified family (family code:FAM001, subFamily code:SUB001) does not exist in database", EC.NO_MATCHING_FAMILY_EXCEPTION_MESSAGE("FAM001", "SUB001"));
                Assert.Equal("X03", EC.MACHINE_NOT_FOUND_EXCEPTION_CODE);
                Assert.Equal("Machine with the code M001 has not been found", EC.MACHINE_NOT_FOUND_EXCEPTION_MESSAGE("M001"));
                Assert.Equal("The machines with the codes in this list (M001,M002,M003) were not found", EC.MACHINES_NOT_FOUND_EXCEPTION_MESSAGE("M001,M002,M003"));
                Assert.Equal("X04", EC.CLAUSE_NOT_FOUND_EXCEPTION_CODE);
                Assert.Equal("The clause with the code C001 has not been found", EC.CLAUSE_NOT_FOUND_EXCEPTION_MESSAGE("C001"));
                Assert.Equal("X05", EC.FAMILY_ALREADY_EXIST_EXCCEPTION_CODE);
                Assert.Equal("A familiy with code FAM001 and one or more Sufamilies codes already exist. Check existing SubFamilies.", EC.FAMILY_ALREADY_EXIST_EXCCEPTION_MESSAGE("FAM001"));
                Assert.Equal("X06", EC.FAMILY_NOT_FOUND_EXCEPTION_CODE);
                Assert.Equal("No family found for the code FAM001 and the subFamily code specified", EC.FAMILY_NOT_FOUND_EXCEPTION_MESSAGE("FAM001"));
                Assert.Equal("No family found for the code FAM001 and the subFamily code SUB001", EC.FAMILY_NOT_FOUND_EXCEPTION_MESSAGE("FAM001", "SUB001"));
                Assert.Equal("X07", EC.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_CODE);
                Assert.Equal("Some machine are attached to this family, so it can't be deleted", EC.MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_MESSAGE);
                Assert.Equal("X08", EC.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_CODE);
                Assert.Equal("Family is not well formed. It must at least have a code and a name", EC.FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_MESSAGE);
                Assert.Equal("X09", EC.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_CODE);
                Assert.Equal("Clause is not well formed. It must have a code and a label", EC.CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_MESSAGE);
                Assert.Equal("X10", EC.CLAUSE_ALREADY_EXIST_EXCCEPTION_CODE);
                Assert.Equal("A clause with the code C001 already exists", EC.CLAUSE_ALREADY_EXIST_EXCCEPTION_MESSAGE("C001"));
                Assert.Equal("X11", EC.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_CODE);
                Assert.Equal("The clause code provided in the request does not correspond to the code in the object.", EC.CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_MESSAGE);
                Assert.Equal("X12", EC.MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_CODE);
                Assert.Equal("The machine code provided in the request does not correspond to the code in the object.", EC.MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_MESSAGE);
                Assert.Equal("X13", EC.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE);
                Assert.Equal("X14", EC.DUPLICATED_CODES_SENT_IN_REQUEST_CODE);
                Assert.Equal("X16", EC.MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_CODE);
                Assert.Equal("X15", EC.NO_MATCHING_CLAUSE_EXCEPTION_CODE);
                Assert.Equal("The specified edition clause (edition clause code:C001) does not exist in database", EC.NO_MATCHING_CLAUSE_EXCEPTION_MESSAGE("C001"));
                Assert.Equal("There are too many machines codes in the request. The limit is set to 10", EC.TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(10));
                Assert.Equal("There are duplicated machines codes in the request. The duplicated codes are in this list (M001,M002,M003)", EC.DUPLICATED_CODES_SENT_IN_REQUEST_MESSAGE("M001,M002,M003"));
                Assert.Equal("X17", EC.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_CODE);
                Assert.Equal("The clauses with the codes M001,M002,M003 are duplicated in the input", EC.MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_MESSAGE(new[] { "M001", "M002", "M003" }));

                int numberOfConstants = fields.Length;
                //Ce code vérifie que toutes les constantes de la classe ExceptionConstants ont les valeurs attendues en utilisant uniquement des assertions Assert.Equal
                Assert.Equal(23, numberOfConstants);
            }

        }
    }
}
