using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MaryHelp.DataLayer;

namespace MaryHelp.Business
{
    [Serializable()]
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Province> GetAll()
        {
            Data data = null;
            DataSet ds;
            List<Province> provinces = null;
            Province province = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                ds = data.ExecuteQuery("SP_SEL_PROVINCE");

                provinces = new List<Province>();
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    province = new Province();
                    province.Id = Convert.ToInt16(dr["Id"].ToString());
                    province.Name = dr["Name"].ToString();
                    provinces.Add(province);
                }

                return provinces;
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
                provinces = null;
                province = null;
                ds = null;
            }
        }
    }
}