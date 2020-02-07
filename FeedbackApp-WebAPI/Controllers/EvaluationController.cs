using FeedbackApp_WebAPI.DBAccess;
using FeedbackApp_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackApp_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EvaluationController : Controller
    {
        [HttpGet("{pin}")]
        public IActionResult GetEvaluation(string pin)
        {
            System.Console.WriteLine("Get PIN");
            try
            {
                var result = SQLiteFunctions.SelectEvaluation(pin);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message + "/r/nStackTrace:" + ex.StackTrace);
                return BadRequest("PIN inválido");
            }
        }

        [HttpPost("{history}")]
        public IActionResult PostHistory([FromBody]User user)
        {
            System.Console.WriteLine("Get History");
            try
            {
                var result = SQLiteFunctions.SelectHistoryEvaluations(user.Email);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message + "/r/nStackTrace:" + ex.StackTrace);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult PostEvaluation([FromBody]Evaluation evaluation)
        {
            System.Console.WriteLine("Post Evaluation");
            try
            {
                if (SQLiteFunctions.InsertEvaluation(evaluation) > 0)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message + "/r/nStackTrace:" + ex.StackTrace);
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult PutEvaluation([FromBody]Evaluation evaluation)
        {
            System.Console.WriteLine("Put Evaluation");
            try
            {
                if (SQLiteFunctions.UpdateEvaluation(evaluation) > 0)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message + "/r/nStackTrace:" + ex.StackTrace);
                return BadRequest();
            }
        }

        [HttpGet("delete")]
        public IActionResult GetExcludeAllHistory()
        {
            System.Console.WriteLine("Exclude all evaluations");
            try
            {
                if (SQLiteFunctions.DeleteAllHistory())
                    return Ok("Histórico deletado com sucesso!");
                else
                    return BadRequest();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message + "/r/nStackTrace:" + ex.StackTrace);
                return BadRequest();
            }
        }
    }
}