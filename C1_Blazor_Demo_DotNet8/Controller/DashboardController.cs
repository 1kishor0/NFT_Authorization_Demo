using C1_Blazor_Demo_DotNet8.Models.ViewModels;
using C1_Blazor_Demo_DotNet8.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace C1_Blazor_Demo_DotNet8.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _DashboardRepository;
        private readonly IConfiguration _configuration;

        public DashboardController(DashboardRepository RepositoryDashboard, IConfiguration configuration)
        {

            _DashboardRepository = RepositoryDashboard;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetReworkdata")]
        public async Task<IActionResult> GetReworkdata([FromQuery] string Log_id)
        {
            string userID = "sakib31";
            string branchId = "0031";

            try
            {
                var result = await _DashboardRepository.GetReworkdata(Log_id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
      
        [HttpGet]
        [Route("GetPendingFuncNM")]
        public async Task<IActionResult> GetPendingFuncNM()
        {
            string userID = "sakib31";
            string branchId = "0031";

            try
            {
                var result = await _DashboardRepository.GetPendingFuncNM(userID, branchId);
                return Ok(result);
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpGet]
        [Route("GetRequestQueue")]
        public async Task<IActionResult> GetRequestQueue([FromQuery] string FunctionID)
        {
            string userID = "sakib31";
            string branchId = "0031";

            try
            {
                var result = await _DashboardRepository.GetRequestQueue(userID, branchId, FunctionID);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("send_back")]
        public IActionResult send_back(int log_id, string branch_id, string remarks)
        {
            try
            {
                var result = _DashboardRepository.send_back(log_id,branch_id,remarks);

                if (result != null)
                {
                    return Ok(new { message = "Send Back successfully." });
                }
                else
                {
                    return BadRequest("Failed to add customer.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");

            }
        }
        [HttpPost]
        [Route("update_rework_flag")]
        public IActionResult update_rework_flag(string log_id, string column_name, string rework_flag)
        {
            var result = _DashboardRepository.update_rework_flag(log_id, column_name, rework_flag);
            if (result != null)
            {
                return Ok(new { message = "Customer added successfully." });
            }
            else
            {
                return BadRequest("Failed to add customer.");
            }

        }


        //monim code
        [HttpPost]
        [Route("add_customer")]
        public IActionResult Addcustomer(CustomerModel objmodel)
        {
            if (objmodel == null)
            {
                return BadRequest("Invalid user data.");
            }
            var result = _DashboardRepository.AddCustomer(objmodel);

            if (result != null)
            {
                return Ok(new { message = "Customer added successfully." });
            }
            else
            {
                return BadRequest("Failed to add customer.");
            }
        }

        //Rumman Code
        [HttpGet]
        [Route("get_list_by_funcID")]

        public async Task<IActionResult> get_list_by_funcID()
        {


            var result = _DashboardRepository.get_list_by_funcID();

            return Ok(result);
        }

        [HttpPost]
        [Route("get_auth_log_list")]

        public IActionResult get_auth_log_list([FromBody] string functionID)
        {

            if (functionID == null)
            {
                return BadRequest("Invalid user data.");
            }
            var result = _DashboardRepository.get_auth_log_list(functionID.ToString());

            return Ok(result);
        }

        [HttpPost]
        [Route("get_log_details_extended")]

        public IActionResult get_log_details_extended([FromBody] string log_id)
        {

            if (log_id == null)
            {
                return BadRequest("Invalid user data.");
            }
            var result = _DashboardRepository.get_log_details_extended(log_id.ToString());

            return Ok(result);
        }




    }
}
