using Microsoft.AspNetCore.Mvc;
using WebAppTest.Model;
using WebAppTest.DataAccess;



namespace WebAppTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorsController : ControllerBase
    {
        private IConfiguration _configuration;
        private ActorsDB _actorsDataBase;

        public ActorsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _actorsDataBase = new ActorsDB(_configuration.GetConnectionString("myDb1"));
        }
        
        [HttpGet]
        public ActionResult GetById(int id)
        {
            Actor actor;

            try
            {
                actor = _actorsDataBase.GetById(id);
                return Ok(actor);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            List<Actor> actors = new List<Actor>();

            try
            {
                actors = _actorsDataBase.GetAll();
                return Ok(actors);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public ActionResult Add(Actor actor)

        {
            try
            {
                bool result = _actorsDataBase.Add(actor);

                if (result)
                {
                    return Ok("New Actor Data Added Successfully");

                }
                else
                {
                    return BadRequest("Actor Data Not Added");

                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        public ActionResult Update(int id, Actor actor)

        {
            try
            {
                bool result = _actorsDataBase.Update(id, actor);

                if (result)
                {
                    return Ok("Actor Data Updated Successfully");

                }
                else
                {
                    return BadRequest("Actor Data Not Updated");

                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        public ActionResult DeleteActorData(int id)

        {
            try
            {
                bool result = _actorsDataBase.Delete(id);

                if (result)
                {
                    return Ok("Actor Data deleted Successfully");

                }
                else
                {
                    return BadRequest("Actor Data Not deleted");

                }

            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}