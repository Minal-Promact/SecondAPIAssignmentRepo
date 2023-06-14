using Microsoft.AspNetCore.Hosting.Server;

namespace SecondAPIAssignmentRepo.Constants
{
    public static class Constant
    {
        public const string Route = "api/[controller]/";

        public const string GetAllEmployees = "GetAllEmployees";
        public const string GetEmployeeById = "GetEmployeeById";
        public const string AddEmployee = "AddEmployee";
        public const string UpdateEmployee = "UpdateEmployee";        
        public const string DeleteEmployee = "DeleteEmployee";

        public const string EnterEmployeeId = "Enter EmployeeId";
        public const string CouldNotFetchEmployeeData = "Could not fetch employee data.";

        public const string GetAllDepartments = "GetAllDepartments";
        public const string GetDepartmentById = "GetDepartmentById";
        public const string AddDepartment = "AddDepartment";
        public const string UpdateDepartment = "UpdateDepartment";        
        public const string DeleteDepartment = "DeleteDepartment";

        public const string RecordNotFound = "Record not found..";
        public const string CouldNotFetchDepartmentData = "Could not fetch department data.";
        public const string EnterDepartmentId = "Enter DepartmentId";
        public const string TheKeyDoesNotExist = "The key does not exist";
        public const string IncorrectRequest = "Incorrect Request";
        public const string TheKeyAlreadyExists = "The key already exists";
        public const string TheRecordAlreadyExists = "The Record already exists";

        public const int InternalServerError = 500;
        public const string InternalServerErrorS = "Internal server error";
    }
}
