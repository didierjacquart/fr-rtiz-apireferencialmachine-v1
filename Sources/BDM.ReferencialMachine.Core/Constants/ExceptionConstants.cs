using System.Collections.Generic;

namespace BDM.ReferencialMachine.Core.Constants
{
    public static class ExceptionConstants
    {
        public static readonly string DB_UPDATE_EXCEPTION_MESSAGE = "Database update failure";

        public static readonly string NOT_AUTHORIZED_PRICING_CODE_VALUE_CODE = "X01";
        public static string NOT_AUTHORIZED_PRICING_CODE_VALUE_MESSAGE(string code) => $"The value for pricingRate.Code ({code}) is not supported. The supported values are INC, GTL, BDI, ADE.";
        public static readonly string NO_MATCHING_FAMILY_EXCEPTION_CODE = "X02";
        public static string NO_MATCHING_FAMILY_EXCEPTION_MESSAGE(string code, string subcode) => $"The specified family (family code:{code}, subFamily code:{subcode}) does not exist in database";
        public static readonly string MACHINE_NOT_FOUND_EXCEPTION_CODE = "X03";
        public static string MACHINE_NOT_FOUND_EXCEPTION_MESSAGE(string machineCode) => $"Machine with the code {machineCode} has not been found";
        public static string MACHINES_NOT_FOUND_EXCEPTION_MESSAGE(string machineCodes) => $"The machines with the codes in this list ({machineCodes}) were not found";
        public static readonly string CLAUSE_NOT_FOUND_EXCEPTION_CODE = "X04";
        public static string CLAUSE_NOT_FOUND_EXCEPTION_MESSAGE(string clauseCode) => $"The clause with the code {clauseCode} has not been found";

        public static readonly string FAMILY_ALREADY_EXIST_EXCCEPTION_CODE = "X05";
        public static string FAMILY_ALREADY_EXIST_EXCCEPTION_MESSAGE(string code) => $"A familiy with code {code} and one or more Sufamilies codes already exist. Check existing SubFamilies.";
        public static readonly string FAMILY_NOT_FOUND_EXCEPTION_CODE = "X06";
        public static string FAMILY_NOT_FOUND_EXCEPTION_MESSAGE(string code) => $"No family found for the code {code} and the subFamily code specified";
        public static string FAMILY_NOT_FOUND_EXCEPTION_MESSAGE(string code, string subCode) => $"No family found for the code {code} and the subFamily code {subCode}";

        public static readonly string MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_CODE ="X07";
        public static readonly string MACHINE_ATTTACHED_TO_FAMILY_EXCEPTION_MESSAGE = "Some machine are attached to this family, so it can't be deleted";

        public static readonly string FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_CODE = "X08";
        public static readonly string FAMILY_CODE_AND_NAME_MANDATORY_EXCEPTION_MESSAGE= "Family is not well formed. It must at least have a code and a name";

        public static readonly string CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_CODE = "X09";
        public static readonly string CLAUSE_CODE_AND_LABEL_MANDATORY_EXCEPTION_MESSAGE = "Clause is not well formed. It must have a code and a label";

        public static readonly string CLAUSE_ALREADY_EXIST_EXCCEPTION_CODE = "X10";
        public static string CLAUSE_ALREADY_EXIST_EXCCEPTION_MESSAGE(string code) => $"A clause with the code {code} already exists";
        public static readonly string CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_CODE = "X11";
        public static readonly string CLAUSE_CODE_DIFFERENT_FROM_OBJECT_EDITION_CLAUSE_EXCEPTION_MESSAGE = "The clause code provided in the request does not correspond to the code in the object.";
        public static readonly string MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_CODE = "X12";
        public static readonly string MACHINE_CODE_DIFFERENT_FROM_OBJECT_MACHINE_CLAUSE_EXCEPTION_MESSAGE = "The machine code provided in the request does not correspond to the code in the object.";
        public static readonly string TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_CODE = "X13";
        public static readonly string NO_MATCHING_CLAUSE_EXCEPTION_CODE = "X15";
        public static string NO_MATCHING_CLAUSE_EXCEPTION_MESSAGE(string clauseCode) => $"The specified edition clause (edition clause code:{clauseCode}) does not exist in database";
        public static string TOO_MANY_MACHINES_CODES_SENT_EXCEPTION_MESSAGE(int maxCodeSended) 
                            => $"There are too many machines codes in the request. The limit is set to {maxCodeSended}";
        public static readonly string DUPLICATED_CODES_SENT_IN_REQUEST_CODE = "X14";
        public static readonly string MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_CODE = "X16";
        public static string MACHINE_ATTTACHED_TO_EDITION_CLAUSE_EXCEPTION_MESSAGE(string code) => $"Some machine are attached to the clause with code {code}, so it can't be deleted";

        public static string DUPLICATED_CODES_SENT_IN_REQUEST_MESSAGE(string duplicatedCodes) 
                            => $"There are duplicated machines codes in the request. The duplicated codes are in this list ({duplicatedCodes})";
        public static readonly string MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_CODE = "X17";
        public static string MANY_CLAUSE_WITH_SAME_CODES_EXCEPTION_MESSAGE(IEnumerable<string> codes) => $"The clauses with the codes {string.Join(',', codes)} are duplicated in the input";
    }
}
