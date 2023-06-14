using Microsoft.AspNetCore.Mvc;
using SecondAPIAssignmentRepo.Constants;
using SecondAPIAssignmentRepo.Model;
using SecondAPIAssignmentRepo.Repository.Interface;

namespace SecondAPIAssignmentRepo.Controllers
{
    [ApiController]
    [Route(Constant.Route)]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _iEmployeeRepository;

        public EmployeeController(IEmployeeRepository iEmployeeRepository)
        {
            this._iEmployeeRepository = iEmployeeRepository;
        }

        /// <summary>
        /// Get All Employee Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(Constant.GetAllEmployees)]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employee = await _iEmployeeRepository.GetAllEmployees();
                if (employee != null)
                {
                    return Ok(employee);
                }
                return NotFound(Constant.RecordNotFound);

            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }

        /// <summary>
        /// Get Employee Details By EmployeeId
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(Constant.GetEmployeeById)]
        public async Task<IActionResult> GetEmployeeById(Guid empId)
        {
            try
            {
                if (empId == Guid.Empty) return BadRequest(Constant.EnterEmployeeId);

                var employee = await _iEmployeeRepository.GetEmployeesById(empId);
                if (employee == null)
                {
                    return NotFound(Constant.TheKeyDoesNotExist);
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }

        /// <summary>
        /// 1. Check Model validation
        /// 2.Check Email is already present in the database if not then we can process request for add
        /// 3.if yes then the The record already exists 
        /// </summary>
        /// <param name="addEmployeeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Constant.AddEmployee)]
        public async Task<IActionResult> AddEmployee(EmployeeRequest addEmployeeRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var empRecord = await _iEmployeeRepository.CheckEmailExistsInEmployee(addEmployeeRequest.Email);
                if (empRecord != null)
                {
                    return Conflict(Constant.TheRecordAlreadyExists);
                }
                
                var result = await _iEmployeeRepository.AddEmployee(addEmployeeRequest);
                 
                return Created($"/{result.Id}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }

        /// <summary>
        /// 1.Check empId is empty or not
        /// 1.Check Model validation
        /// 2.Check EmpId is already present in the database if yes then we can process request for update
        /// 3.if not then The key does not exists 
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="updateEmployeeRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(Constant.UpdateEmployee)]
        public async Task<IActionResult> UpdateEmployee(Guid empId, EmployeeRequest updateEmployeeRequest)
        {
            try
            {
                if (empId == Guid.Empty) return BadRequest(Constant.EnterEmployeeId);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = await _iEmployeeRepository.GetndCheckEmployeesById(empId);

                if (employee != null)
                {
                    var result = await _iEmployeeRepository.UpdateEmployee(employee, updateEmployeeRequest);
                    return Ok(result);
                }
                return NotFound(Constant.TheKeyDoesNotExist);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }


        /// <summary>
        /// 1.Check empId is empty or not
        /// 1.Check Model validation
        /// 2.Check EmpId is already present in the database if yes then we can process request for delete
        /// 3.if not then The key does not exists
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route(Constant.DeleteEmployee)]
        public async Task<IActionResult> DeleteEmployee(Guid empId)
        {
            try
            {
                if (empId == Guid.Empty) return BadRequest(Constant.EnterEmployeeId);

                var employee = await _iEmployeeRepository.GetndCheckEmployeesById(empId);
                if (employee != null)
                {
                    _iEmployeeRepository.DeleteEmployee(employee);                    
                     return Ok();
                    
                }
                return NotFound(Constant.TheKeyDoesNotExist);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.InternalServerError, Constant.InternalServerErrorS);
            }
        }
    }
    
}
