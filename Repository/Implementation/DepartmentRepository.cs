using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SecondAPIAssignmentRepo.Data;
using SecondAPIAssignmentRepo.DTO;
using SecondAPIAssignmentRepo.Model;
using SecondAPIAssignmentRepo.Repository.Interface;
using SecondAPIAssignmentRepo.ToMap;
using System.Collections.Generic;

namespace SecondAPIAssignmentRepo.Repository.Implementation
{
    public class DepartmentRepository :IDepartmentRepository
    {
        private readonly EFDataContext dbContext;
        private readonly IMapper _iMapper;
        
        public DepartmentRepository(EFDataContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this._iMapper = mapper;
        }

        public async Task<List<DepartmentReponseDTO>> GetAllDepartments()        
        {
            List<Department> departments = await dbContext.Departments.ToListAsync();
            List<DepartmentReponseDTO> lstDepartmentReponseDTO = departments.Select(a => new DepartmentReponseDTO() { Id = a.Id, DepartmentName = a.DepartmentName }).ToList();
            //if (departments != null && departments.Count > 0)
            //{
            //    lstDepartmentReponseDTO = departments.GetListOfDepartmentResponseDTO(_iMapper);
            //}
            return lstDepartmentReponseDTO;            
        }

        public async Task<DepartmentReponseDTO> GetDepartmentById(Guid departmentId)
        {
            var department = dbContext.Departments.Include(p => p.Employees).Single(p => p.Id == departmentId);
            DepartmentReponseDTO departmentReponseDTO = _iMapper.Map<Department, DepartmentReponseDTO>(department);
            //DepartmentReponseDTO departmentReponseDTO = null;
            //if (department != null)
            //{
            //    departmentReponseDTO = department.GetDepartmentResponseDTO(_iMapper);
            //}
            return departmentReponseDTO;                        
        }
        public async Task<Department> GetndCheckDepartmentById(Guid departmentId)
        {
            var departments = dbContext.Departments.Include(p => p.Employees).Single(p => p.Id == departmentId);
            return departments;
        }
        public async Task<Department> CheckDepartmentNameExistsInDepartments(string departmentName)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(a => a.DepartmentName == departmentName);
        }

        public async Task<DepartmentReponseDTO> AddDepartment(DepartmentRequest addDepartmentRequest)
        {
            var department = _iMapper.Map<DepartmentRequest, Department>(addDepartmentRequest);
            
            await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();

            DepartmentReponseDTO departmentReponseDTO = _iMapper.Map<Department, DepartmentReponseDTO>(department);

            return departmentReponseDTO;
        }

        public async Task<DepartmentReponseDTO> UpdateDepartment(Department dept, DepartmentRequest updateDepartmentRequest)
        {
            dept.DepartmentName = updateDepartmentRequest.DepartmentName;

            dbContext.Departments.Update(dept);
            await dbContext.SaveChangesAsync();

            DepartmentReponseDTO departmentReponseDTO = _iMapper.Map<Department, DepartmentReponseDTO>(dept);

            return departmentReponseDTO;
        }

        public void DeleteDepartment(Department dept)
        {
            dbContext.Departments.Remove(dept);
            dbContext.SaveChangesAsync();
        }
    }
}
