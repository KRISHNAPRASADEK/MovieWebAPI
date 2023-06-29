using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebAppTest.Model;
namespace WebAppTest.DataAccess
{
    public class ActorsDB : ControllerBase
    {
        private string _connectionString;

        public ActorsDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Actor Get(int id)
        {
            Actor actor = new Actor();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand getCommand = new SqlCommand("select * from Actors where act_id=" + id, con);
                SqlDataReader getDataReader = getCommand.ExecuteReader();

                while (getDataReader.Read())
                {
                    actor.Id = getDataReader.GetInt32(0);
                    actor.Name = getDataReader.GetString(1);
                    actor.Gender = getDataReader.GetString(2);

                }
                con.Close();
            }
            return actor;
        }

        public List<Actor> GetAll()
        {

            List<Actor> actors = new List<Actor>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand getListCommand = new SqlCommand("select * from Actors", con);
                SqlDataReader getListDataReader = getListCommand.ExecuteReader();

                while (getListDataReader.Read())
                {
                    Actor actor = new Actor();

                    actor.Id = getListDataReader.GetInt32(0);
                    actor.Name = getListDataReader.GetString(1);
                    actor.Gender = getListDataReader.GetString(2);
                    actors.Add(actor);
                }
                con.Close();
            }
            return actors;
        }

        public bool Add(Actor actor)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string addDataQuery = "insert into Actors (act_name,act_gender) Values ('" + actor.Name + "','"
                    + actor.Gender + "')";
                con.Open();
                SqlCommand createCommand = new SqlCommand(addDataQuery, con);
                int result = createCommand.ExecuteNonQuery();
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

        public bool Update(int id, Actor actor)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string updateDataQuery = "UPDATE Actors SET act_name = '" + actor.Name + "',act_gender = '" + actor.Gender + "' where act_id=" + id;
                con.Open();
                SqlCommand updateCommand = new SqlCommand(updateDataQuery, con);
                int result = updateCommand.ExecuteNonQuery();
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


        public bool Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string deleteDataQuery = "DELETE FROM Actors WHERE act_id=" + id;
                con.Open();
                SqlCommand deleteCommand = new SqlCommand(deleteDataQuery, con);
                int result = deleteCommand.ExecuteNonQuery();
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
