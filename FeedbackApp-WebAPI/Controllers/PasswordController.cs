using FeedbackApp_WebAPI.DBAccess;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PasswordController : Controller
    {
        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            System.Console.WriteLine("Password Recovery");
            try
            {
                var result = SQLiteFunctions.RecoverPassword(email);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message + "/r/nStackTrace:" + ex.StackTrace);
                return BadRequest("PIN inválido");
            }
        }
    }
}
