using Microsoft.AspNetCore.Mvc;
using SecondAPIAssignmentRepo.Constants;
using SecondAPIAssignmentRepo.Model;
using SecondAPIAssignmentRepo.Repository.Implementation;
using SecondAPIAssignmentRepo.Repository.Interface;

namespace SecondAPIAssignmentRepo.Controllers
{
    [ApiController]
    [Route(Constant.Route)]
    public class DepartmentController : Controller
    {        
        private readonly IDepartmentRepository _iDepartmentRepository;

        public DepartmentController(IDepartmentRepository iDepartmentRepository)
        {
            this._iDepartmentRepository = iDepartmentRepository;            
        }

        [HttpGet]
        [Route(Constant.GetAllDepartments)]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _iDepartmentRepository.GetAllDepartments();
                if (departments != null)
                {
                    return Ok(departments);
                }
                return NotFound(Constant.RecordNotFound);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, "Internal server error");
            }
        }
        [HttpPost]
        [Route(Constant.AddDepartment)]
        public async Task<IActionResult> AddDepartment(DepartmentRequest departmentRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var deptRecord = await _iDepartmentRepository.CheckDepartmentNameExistsInDepartments(departmentRequest.DepartmentName);
                if (deptRecord != null)
                {
                    return Conflict(Constant.TheRecordAlreadyExists);
                }

                var result = await _iDepartmentRepository.AddDepartment(departmentRequest);
                return Created($"/{result.Id}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, "Internal server error");
            }
        }

        [HttpPut]
        [Route(Constant.UpdateDepartment)]
        public async Task<IActionResult> UpdateDepartment(Guid departmentId, DepartmentRequest updateDepartmentRequest)
        {
            try
            {
                if (departmentId == Guid.Empty) return BadRequest(Constant.EnterDepartmentId);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var department = await _iDepartmentRepository.GetDepartmentById(departmentId);

                if (department != null)
                {
                    var result = await _iDepartmentRepository.UpdateDepartment(department, updateDepartmentRequest);
                    return Ok(result);
                }
                return NotFound(Constant.TheKeyDoesNotExist);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, "Internal server error");
            }
        }

        [HttpDelete]
        [Route(Constant.DeleteDepartment)]
        public async Task<IActionResult> DeleteDepartment(Guid departmentId)
        {
            try
            {
                if (departmentId == Guid.Empty) return BadRequest(Constant.EnterDepartmentId);

                var department = await _iDepartmentRepository.GetDepartmentById(departmentId);
                if (department != null)
                {
                    _iDepartmentRepository.DeleteDepartment(department);
                    var deptDel = await _iDepartmentRepository.GetDepartmentById(departmentId);
                    if (deptDel == null)
                    {
                        return Ok();
                    }
                }
                return NotFound(Constant.TheKeyDoesNotExist);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, "Internal server error");
            }
        }

    }
}
