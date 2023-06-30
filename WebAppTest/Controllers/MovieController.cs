using Microsoft.AspNetCore.Mvc;
using WebAppTest.Model;
using WebAppTest.DataAccess;



namespace WebAppTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        public IConfiguration _configuration;
        private MovieDB _movieDataBase;

        public MovieController(IConfiguration configuration) {
            _configuration = configuration;
            _movieDataBase= new MovieDB(_configuration.GetConnectionString("myDb1"));
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            Movie movie;

            try
            {
                movie = _movieDataBase.GetById(id);
                return Ok(movie);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            List<Movie> movies = new List<Movie>();

            try
            {
                movies=_movieDataBase.GetAll();
                return Ok(movies);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public ActionResult Add(Movie movie)

        {
            try
            {
                bool result= _movieDataBase.Add(movie);

                if (result)
                {
                    return Ok("New Movie Data Added Successfully");

                }
                else
                {
                    return BadRequest("Movie Data Not Added");

                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        public ActionResult Update(int id,Movie movie)

        {
            try
            {
                bool result=_movieDataBase.Update(id, movie);

                if (result)
                {
                    return Ok("Movie Data Updated Successfully");

                }
                else
                {
                    return BadRequest("Movie Data Not Updated");

                }
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        public ActionResult Delete(int id)

        {
            try
            {
                bool result= _movieDataBase.Delete(id);

                if (result)
                {
                    return Ok("Movie Data deleted Successfully");

                }
                else
                {
                    return BadRequest("Movie Data Not deleted");

                }

            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}