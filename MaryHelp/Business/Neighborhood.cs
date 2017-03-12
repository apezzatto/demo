using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using MaryHelp.DataLayer;

namespace MaryHelp.Business
{
    [Serializable()]
    public class Neighborhood
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public City City { get; set; }

        public List<Neighborhood> GetNeighborhoodByCity(int idCity)
        {
            Data data = null;
            DataSet ds;
            List<Neighborhood> neighborhoods = null;
            Neighborhood neighborhood = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_city", MySqlDbType = MySqlDbType.Int16, Size = 0, Direction = ParameterDirection.Input, Value = idCity },
                };

                ds = data.ExecuteQuery("SP_SEL_NEIGHBORHOOD_BY_CITY", p);

                neighborhoods = new List<Neighborhood>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    neighborhood = new Neighborhood();
                    neighborhood.Id = Convert.ToInt16(dr["Id"].ToString());
                    neighborhood.Name = dr["Name"].ToString();

                    neighborhoods.Add(neighborhood);
                }

                return neighborhoods;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (data != null)
                    data.CloseConnection();

                data = null;
                neighborhoods = null;
                neighborhood = null;
                ds = null;
            }
        }
    }
}