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
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Province Province { get; set; }

        public List<City> GetCitiesByProvince(int idProvince)
        {
            Data data = null;
            DataSet ds;
            List<City> cities = null;
            City city = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_province", MySqlDbType = MySqlDbType.Int16, Size = 0, Direction = ParameterDirection.Input, Value = idProvince },
                };

                ds = data.ExecuteQuery("SP_SEL_CITY_BY_PROVINCE", p);

                cities = new List<City>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    city = new City();
                    city.Id = Convert.ToInt16(dr["Id"].ToString());
                    city.Name = dr["Name"].ToString();

                    cities.Add(city);
                }

                return cities;
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
                cities = null;
                city = null;
                ds = null;
            }
        }
    }
}