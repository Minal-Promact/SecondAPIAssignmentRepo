using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SecondAPIAssignmentRepo.Model;

namespace SecondAPIAssignmentRepo.AutomapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Employee,
                EmployeeRequest>().ReverseMap();
            CreateMap<Department,
                DepartmentRequest>().ReverseMap();
            
        }
    }
}
