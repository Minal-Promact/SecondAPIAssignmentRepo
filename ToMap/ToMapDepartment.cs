using AutoMapper;
using SecondAPIAssignmentRepo.DTO;
using SecondAPIAssignmentRepo.Model;

namespace SecondAPIAssignmentRepo.ToMap
{
    public static class ToMapDepartment
    {
        public static List<DepartmentReponseDTO> GetListOfDepartmentResponseDTO(this List<Department> lstDepartment,IMapper _iMapper)
        {
            List<DepartmentReponseDTO> lstDepartmentReponseDTO = new List<DepartmentReponseDTO>();
            lstDepartment.ForEach(d => {
                DepartmentReponseDTO departmentReponseDTO = _iMapper.Map<Department, DepartmentReponseDTO>(d);
                lstDepartmentReponseDTO.Add(departmentReponseDTO);
            });
            return lstDepartmentReponseDTO;
        }

        public static DepartmentReponseDTO GetDepartmentResponseDTO(this Department department, IMapper _iMapper)
        {            
            DepartmentReponseDTO departmentReponseDTO = _iMapper.Map<Department, DepartmentReponseDTO>(department);                
            return departmentReponseDTO;
        }
    }
}
