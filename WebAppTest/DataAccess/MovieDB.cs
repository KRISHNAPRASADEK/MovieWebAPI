using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebAppTest.Model;
namespace WebAppTest.DataAccess
{
    public class MovieDB
    {
        private string _connectionString;

        public MovieDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Movie GetById(int id)
        {
            Movie movie = new Movie();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand getAMovieCommand = new SqlCommand("select * from Movies where movie_id=" + id, con);
                SqlDataReader getAMovieDataReader = getAMovieCommand.ExecuteReader();

                while (getAMovieDataReader.Read())
                {
                    movie.Id = getAMovieDataReader.GetInt32(0);
                    movie.Title = getAMovieDataReader.GetString(1);
                    movie.Year = getAMovieDataReader.GetInt32(2);
                    movie.Language = getAMovieDataReader.GetString(3);
                    movie.ProducerID = getAMovieDataReader.GetInt32(4);
                 
                }
                con.Close();
            }
            return movie;
        }

        public List<Movie> GetAll()
        {  

        List<Movie> movies = new List<Movie>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand getMoviesListCommand = new SqlCommand("select * from Movies", con);
                SqlDataReader getMovieListDataReader = getMoviesListCommand.ExecuteReader();

                while (getMovieListDataReader.Read())
                {
                    Movie movie = new Movie();

                    movie.Id = getMovieListDataReader.GetInt32(0);
                    movie.Title = getMovieListDataReader.GetString(1);
                    movie.Year = getMovieListDataReader.GetInt32(2);
                    movie.Language = getMovieListDataReader.GetString(3);
                    movie.ProducerID = getMovieListDataReader.GetInt32(4);
                    movies.Add(movie);
                }
                con.Close();
            }
            return movies;
        }

        public bool Add(Movie movie) 
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string addMovieDataQuery = "insert into Movies (movie_title,movie_year,movie_lang,prod_id) Values ('" + movie.Title + "','"
                    + movie.Year + "','" + movie.Language + "','" + movie.ProducerID + "')";
                con.Open();
                SqlCommand createMovieCommand = new SqlCommand(addMovieDataQuery, con);
                int result = createMovieCommand.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {
                    return true;

                }
                else
                {
                    return false;

                }
            }
        }

        public bool Update(int id, Movie movie)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string updateMovieDataQuery = "UPDATE Movies SET movie_title = '" + movie.Title + "',movie_year=" + movie.Year + ",movie_lang='" + movie.Language + "',prod_id=" + movie.ProducerID + "where movie_id=" + id;
                con.Open();
                SqlCommand updateMovieCommand = new SqlCommand(updateMovieDataQuery, con);
                int result = updateMovieCommand.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {
                    return true;

                }
                else
                {
                    return false;

                }
            }
        }


        public bool Delete(int id) {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string deleteMovieDataQuery = "DELETE FROM Movies WHERE movie_id=" + id;
                con.Open();
                SqlCommand deleteMovieCommand = new SqlCommand(deleteMovieDataQuery, con);
                int result = deleteMovieCommand.ExecuteNonQuery();
                con.Close();

                if (result == 1)
                {
                    return true;

                }
                else
                {
                    return false;

                }
            }
        }


    }
}
