using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace OS_Project
{
    class Connection
    {
        string connectionString = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=1234;Database=dbUniversity;";
        public void getData(NpgsqlCommand query)
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString);
            npgsqlConnection.Open();

            NpgsqlDataReader reader = query.ExecuteReader();

            object[] Obj = new object[5];

            if (reader.HasRows)
            {

                for (int i = 0; i <= Obj.Length;)
                {
                    foreach (IDataRecord data in reader)
                    {
                        Obj[i] = data["fio"];
                        i++;
                    }
                }


            }


            npgsqlConnection.Close();

        }
    }
}
