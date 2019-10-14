using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace rr_program
{
    class UniStructure
    {
        public int id_spec, id_group, id_inst;
        public string name_spec, name_group;
        public Connection con;
        public Query queryString = new Query();

        public UniStructure() { }

        public UniStructure(int id_spec, string name_spec, int id_inst) //for specialties
        {
            this.id_spec = id_spec;
            this.name_spec = name_spec;
            this.id_inst = id_inst;
        }

        public UniStructure(int id_group, string name_group) //for groups
        {
            this.id_group = id_group;
            this.name_group = name_group;
        }

        public List<UniStructure> specialitiesData()
        {
            con = new Connection(queryString.getSpecialtiesString());
            try
            {
                NpgsqlDataReader reader = con.getReader();
                List<UniStructure> specialities = new List<UniStructure>();

                if (reader.HasRows)
                {
                    foreach (IDataRecord data in reader)
                    {
                        id_spec = Convert.ToInt32(data["id_specialty"]);
                        name_spec = (data["name"]).ToString();
                        id_inst = Convert.ToInt32(data["institut_id"]);

                        specialities.Add(new UniStructure(id_spec, name_spec, id_inst));
                    }

                    return specialities;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public List<UniStructure> groupsData(int id_s)
        {
            con = new Connection(queryString.getGroupsString(id_s));
            try
            {
                NpgsqlDataReader reader = con.getReader();
                List<UniStructure> groups = new List<UniStructure>();

                if (reader.HasRows)
                {
                    foreach (IDataRecord data in reader)
                    {
                        id_group = Convert.ToInt32(data["id_group"]);
                        name_group = (data["name"]).ToString();
                        groups.Add(new UniStructure(id_group, name_group));
                    }

                    return groups;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }
    }
}
