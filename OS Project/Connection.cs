using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace rr_program
{
    class Connection:IDisposable
    {
        private string _conString = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=1234;Database=dbUniversity;";
        private NpgsqlConnection _npgsqlConnection;
        private NpgsqlCommand _query;
        public string qString;
        public NpgsqlDataReader reader;        

        public Connection(string qString) //query string
        {
            this.qString = qString;
        }

        private NpgsqlConnection NpgsqlConn() //connect to DB
        {
            NpgsqlConnection сon= null;

            try
            {
                сon = new NpgsqlConnection(_conString);
                сon.Open();

                return сon;
            }
            catch
            {
                Dispose();
                return null;
            }
            finally
            {
                //if(сon != null)
                //{
                //    сon.Close();
                //}                
            }
            
        }       

        public NpgsqlDataReader getReader()
        {
            _npgsqlConnection = NpgsqlConn(); //connection to db
            _query = new NpgsqlCommand(qString, _npgsqlConnection); //New query
            reader = _query.ExecuteReader(); //read query result


            if (reader.HasRows)
            {
                return reader;
            }

            return null;

        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
