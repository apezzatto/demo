using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using MaryHelp.DataLayer;

namespace MaryHelp.Business
{
    [Serializable()]
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public Province Province { get; set; }
        public City City { get; set; }
        public Neighborhood Neighborhood { get; set; }

        public User() { }

        public User(string id, string name, string middlename, string lastname, string address1, string address2, Province province, City city, Neighborhood neighborhood, string zipcode, string email, string password)
        {
            this.Id = id;
            this.Name = name;
            this.MiddleName = middlename;
            this.LastName = lastname;
            this.Address1 = address1;
            this.Address2 = address2;
            this.Province = province;
            this.City = city;
            this.Neighborhood = neighborhood;
            this.ZipCode = zipcode;
            this.EMail = email;
            this.Password = password;
        }

        public void LoadProfile()
        {
            Data data = null;
            DataRowCollection dr;
            Province province;
            City city;
            Neighborhood neighborhood;           

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_user", MySqlDbType = MySqlDbType.VarChar, Size = 36, Direction = ParameterDirection.Input, Value = this.Id },
                };

                dr = data.ExecuteQuery("SP_SEL_USER_PROFILE", p).Tables[0].Rows;

                Name = dr[0]["Name"].ToString();
                MiddleName = dr[0]["MiddleName"].ToString();
                LastName = dr[0]["LastName"].ToString();
                Address1 = dr[0]["Address1"].ToString();
                Address2 = dr[0]["Address2"].ToString();
                ZipCode = dr[0]["ZipCode"].ToString();
                EMail = dr[0]["EMail"].ToString();
                Password = dr[0]["Password"].ToString();

                province = new Province();
                province.Id = Convert.ToInt16(dr[0]["province_Id"].ToString());
                Province = province;

                city = new City();
                city.Id = Convert.ToInt16(dr[0]["city_Id"].ToString());
                City = city;

                neighborhood = new Neighborhood();
                neighborhood.Id = Convert.ToInt16(dr[0]["neighborhood_Id"].ToString());
                Neighborhood = neighborhood;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CloseConnection();
                data = null;
                dr = null;
                province = null;
                city = null;
                neighborhood = null;
            }
        }

        public void SignUp()
        {
            Data data = null;

            try
            {
                #region Create Account
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_name", MySqlDbType = MySqlDbType.VarChar, Size = 20, Direction = ParameterDirection.Input, Value = this.Name },
                    new MySqlParameter() { ParameterName = "@p_middlename", MySqlDbType = MySqlDbType.VarChar, Size = 30, Direction = ParameterDirection.Input, Value = this.MiddleName },
                    new MySqlParameter() { ParameterName = "@p_lastname", MySqlDbType = MySqlDbType.VarChar, Size = 30, Direction = ParameterDirection.Input, Value = this.LastName },
                    new MySqlParameter() { ParameterName = "@p_address1", MySqlDbType = MySqlDbType.VarChar, Size = 100, Direction = ParameterDirection.Input, Value = this.Address1 },
                    new MySqlParameter() { ParameterName = "@p_address2", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.Address2 },
                    new MySqlParameter() { ParameterName = "@p_id_province", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.Province.Id },
                    new MySqlParameter() { ParameterName = "@p_id_city", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.City.Id },
                    new MySqlParameter() { ParameterName = "@p_id_neighborhood", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.Neighborhood.Id },
                    new MySqlParameter() { ParameterName = "@p_zipcode", MySqlDbType = MySqlDbType.VarChar, Size = 6, Direction = ParameterDirection.Input, Value = this.ZipCode },
                    new MySqlParameter() { ParameterName = "@p_email", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.EMail },
                    new MySqlParameter() { ParameterName = "@p_password", MySqlDbType = MySqlDbType.VarChar, Size = 8, Direction = ParameterDirection.Input, Value = this.Password }
                };

                data.ExecuteNonQuery("SP_INS_USER", p);
                data.CloseConnection();
                #endregion

                #region Log In
                this.LogIn(this.EMail, this.Password);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CloseConnection();
                data = null;
            }
        }

        public void SaveProfile()
        {
            Data data = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_user", MySqlDbType = MySqlDbType.VarChar, Size = 36, Direction = ParameterDirection.Input, Value = this.Id },
                    new MySqlParameter() { ParameterName = "@p_name", MySqlDbType = MySqlDbType.VarChar, Size = 20, Direction = ParameterDirection.Input, Value = this.Name },
                    new MySqlParameter() { ParameterName = "@p_middlename", MySqlDbType = MySqlDbType.VarChar, Size = 30, Direction = ParameterDirection.Input, Value = this.MiddleName },
                    new MySqlParameter() { ParameterName = "@p_lastname", MySqlDbType = MySqlDbType.VarChar, Size = 30, Direction = ParameterDirection.Input, Value = this.LastName },
                    new MySqlParameter() { ParameterName = "@p_address1", MySqlDbType = MySqlDbType.VarChar, Size = 100, Direction = ParameterDirection.Input, Value = this.Address1 },
                    new MySqlParameter() { ParameterName = "@p_address2", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.Address2 },
                    new MySqlParameter() { ParameterName = "@p_id_province", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.Province.Id },
                    new MySqlParameter() { ParameterName = "@p_id_city", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.City.Id },
                    new MySqlParameter() { ParameterName = "@p_id_neighborhood", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.Neighborhood.Id },
                    new MySqlParameter() { ParameterName = "@p_zipcode", MySqlDbType = MySqlDbType.VarChar, Size = 6, Direction = ParameterDirection.Input, Value = this.ZipCode },
                    new MySqlParameter() { ParameterName = "@p_email", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = this.EMail },
                    new MySqlParameter() { ParameterName = "@p_password", MySqlDbType = MySqlDbType.VarChar, Size = 8, Direction = ParameterDirection.Input, Value = this.Password }
                };

                data.ExecuteNonQuery("SP_UPD_USER", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CloseConnection();
                data = null;
            }
        }

        public void LogIn(string p_email, string p_password)
        {
            Data data = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_email", MySqlDbType = MySqlDbType.VarChar, Size = 50, Direction = ParameterDirection.Input, Value = p_email },
                    new MySqlParameter() { ParameterName = "@p_password", MySqlDbType = MySqlDbType.VarChar, Size = 8, Direction = ParameterDirection.Input, Value = p_password }
                };

                this.Id = data.ExecuteScalar("SP_SEL_USER_BY_EMAIL_PASSWORD", p);

                if (this.Id == null)
                    throw new ApplicationException("E-mail not found or password is incorrect.");

                data.CloseConnection();

                this.LoadProfile();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CloseConnection();
                data = null;
            }
        }
     }
}