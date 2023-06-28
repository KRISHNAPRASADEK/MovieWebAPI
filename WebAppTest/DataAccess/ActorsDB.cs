using Microsoft.AspNetCore.Mvc;

namespace WebAppTest.DataAccess
{
    public class ActorsDB:ControllerBase
    {
        private string _connectionString;

        public ActorsDB(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
