using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using MaryHelp.DataLayer;
using MaryHelp.Util;

namespace MaryHelp.Business
{
    [Serializable()]
    public class Job : List<Job>
    {
        //This must became a domain table
        public enum STATUS { FINISHED = 0, OPEN = 1, APPROVAL = 2, APPROVED = 3, CANCELED_BY_WORKER = 4, CANCELED_BY_CLIENT = 5, REPROVED = 6};
        //*******
        
        //This information should be in the database, but because of the time...
        private const int _DISHES = 30; //30 min.
        private const int _VACUUM = 60; //1 hour
        private const int _GARAGE = 90; //1.5 hour
        private const int _LAUNDRY = 60; //1 hour
        private const int _ROOM = 60; //1 hour
        private const int _WINDOW = 15; //15 min.
        private const int _BATH = 90; //1.5 hour
        private const int _MIRROR = 5; //5 min.
        private const double _RATE = 22.00;
        //*******

        public Client Client { get; set; }
        public string Id { get; set; }
        public DateTime DateTimeJobCreation { get; set; }
        public DateTime DateTimeJobStarts { get; set; }
        public DateTime DateTimeJobEnd { get; set; }
        public double HoursForConclusion { get; set; }

        public DateTime EstimatedEndTime
        {
            get
            {
                return this.DateTimeJobStarts.AddHours(Utilities.ConvertTimeBase60To100(this.HoursForConclusion));
            }
        }

        public int QtyRooms { get; set; }
        public int QtyBaths { get; set; }
        public int QtyWindows { get; set; }
        public int QtyMirrors { get; set; }
        public bool Vacuum { get; set; }
        public bool Laundry { get; set; }
        public bool Garage { get; set; }
        public bool Dishes { get; set; }
        public decimal Price { get; set; }
        public STATUS Status { get; set; }
        public Worker Worker { get; set; }

        private List<Job> FillListOfJobs(DataRowCollection dr)
        {
            Job job = null;
            List<Job> jobs = null;

            try
            {
                jobs = new List<Job>();
                foreach (DataRow row in dr)
                {
                    job = new Job();

                    job.Id = row["idJob"].ToString();

                    job.Client = new Client();
                    job.Client.Neighborhood = new Neighborhood();
                    job.Client.Neighborhood.Name = row["NeighborhoodName"].ToString();
                    job.DateTimeJobCreation = Convert.ToDateTime(row["dtJobCreation"].ToString()).Date;
                    job.HoursForConclusion = Convert.ToDouble(row["hoursConclusion"].ToString());
                    job.DateTimeJobStarts = Convert.ToDateTime(row["dtJobStarts"].ToString()).Date;
                    job.QtyRooms = Convert.ToInt32(row["qtyRooms"].ToString());
                    job.QtyBaths = Convert.ToInt32(row["qtyBaths"].ToString());
                    job.QtyWindows = Convert.ToInt32(row["qtyWindows"].ToString());
                    job.QtyMirrors = Convert.ToInt32(row["qtyMirrors"].ToString());
                    job.Vacuum = Convert.ToBoolean(row["vacuum"].ToString());
                    job.Laundry = Convert.ToBoolean(row["laundry"].ToString());
                    job.Garage = Convert.ToBoolean(row["garage"].ToString());
                    job.Dishes = Convert.ToBoolean(row["dishes"].ToString());
                    job.Status = (STATUS)Convert.ToInt32(row["jobOpen"].ToString());
                    job.Price = Convert.ToDecimal(row["price"].ToString());

                    jobs.Add(job);
                }
                return jobs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                job = null;
                jobs = null;
            }
        }

        public List<Job> GetJobsByNeighborhood(Neighborhood neighborhood, string userId)
        {
            Data data = null;
            DataRowCollection dr;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_neighborhood_id", Value = neighborhood.Id },
                    new MySqlParameter() { ParameterName = "@p_user_id", Value = userId },
                };

                dr = data.ExecuteQuery("SP_SEL_JOBS_BY_NEIGHBORHOOD", p).Tables[0].Rows;

                return FillListOfJobs(dr);
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
                dr = null;
            }
        }

        public string GetClientEmail()
        {
            Data data = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_job", Value = this.Id },
                };

                return data.ExecuteScalar("SP_SEL_CLIENT_EMAIL_BY_JOBID", p);
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

        public Worker GetWorkerDetails()
        {
            Data data = null;
            DataRowCollection dr;
            Worker worker = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_job", Value = this.Id },
                };

                dr = data.ExecuteQuery("SP_SEL_WORKER_DETAILS_BY_JOBID", p).Tables[0].Rows;

                if (dr.Count > 0)
                {
                    worker = new Worker();
                    worker.Name = dr[0]["Name"].ToString();
                    worker.LastName = dr[0]["LastName"].ToString();
                    worker.Address1 = dr[0]["Address1"].ToString();
                    worker.Address2 = dr[0]["Address2"].ToString();
                    worker.ZipCode = dr[0]["ZipCode"].ToString();

                    worker.City = new City();
                    worker.City.Name = dr[0]["CityName"].ToString();

                    worker.Neighborhood = new Neighborhood();
                    worker.Neighborhood.Name = dr[0]["NeighborhoodName"].ToString();
                }

                return worker;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr = null;
                worker = null;
                data.CloseConnection();
                data = null;
            }
        }

        public void LoadJobDetails()
        {
            Data data = null;
            DataRowCollection dr;
            Worker worker = null;
            Client client = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_job", Value = this.Id },
                };

                dr = data.ExecuteQuery("SP_SEL_JOB_DETAILS", p).Tables[0].Rows;

                if (dr.Count > 0)
                {
                    this.DateTimeJobStarts = Convert.ToDateTime(dr[0]["dtJobStarts"].ToString());
                    this.HoursForConclusion = Utilities.ConvertTimeBase100To60(Convert.ToDouble(dr[0]["hoursConclusion"].ToString()));
                    this.Price = Convert.ToDecimal(dr[0]["Price"].ToString());

                    worker = new Worker();
                    worker.Name = dr[0]["WorkerName"].ToString();
                    worker.LastName = dr[0]["WorkerLastName"].ToString();
                    worker.Address1 = dr[0]["WorkerAddress1"].ToString();
                    worker.Address2 = dr[0]["WorkerAddress2"].ToString();
                    worker.EMail = dr[0]["WorkerEMail"].ToString();

                    worker.City = new City();
                    worker.City.Name = dr[0]["WorkerCityName"].ToString();

                    worker.Neighborhood = new Neighborhood();
                    worker.Neighborhood.Name = dr[0]["WorkerNeighborhoodName"].ToString();

                    this.Worker = worker;

                    client = new Client();
                    client.Name = dr[0]["ClientName"].ToString();
                    client.LastName = dr[0]["ClientLastName"].ToString();
                    client.Address1 = dr[0]["ClientAddress1"].ToString();
                    client.Address2 = dr[0]["ClientAddress2"].ToString();
                    client.EMail = dr[0]["ClientEMail"].ToString();

                    client.City = new City();
                    client.City.Name = dr[0]["ClientCityName"].ToString();

                    client.Neighborhood = new Neighborhood();
                    client.Neighborhood.Name = dr[0]["ClientNeighborhoodName"].ToString();

                    this.Client = client;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr = null;
                data.CloseConnection();
                data = null;
                worker = null;
                client = null;
            }
        }

        public List<Job> GetJobsByCity(City city, string userId)
        {
            Data data = null;
            DataRowCollection dr;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_city_id", Value = city.Id },
                    new MySqlParameter() { ParameterName = "@p_user_id", Value = userId },
                };

                dr = data.ExecuteQuery("SP_SEL_JOBS_BY_CITY", p).Tables[0].Rows;

                return FillListOfJobs(dr);
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
                dr = null;
            }
        }

        public double[] CalculatePriceAndTime()
        {
            int totalMinutes = 0;
            double[] ret = new double[2];
            string hour = string.Empty;
            string minutes = string.Empty;

            try
            {
                if (Dishes)
                    totalMinutes += _DISHES;

                if (Laundry)
                    totalMinutes += _LAUNDRY;

                if (Garage)
                    totalMinutes += _GARAGE;

                if (Vacuum)
                    totalMinutes += _VACUUM;

                totalMinutes += (QtyBaths * _BATH);
                totalMinutes += (QtyRooms * _ROOM);
                totalMinutes += (QtyWindows * _WINDOW);
                totalMinutes += (QtyMirrors * _MIRROR);

                hour = Convert.ToInt32(totalMinutes / 60).ToString();
                minutes = Convert.ToDecimal(totalMinutes % 60).ToString();

                ret[0] = Convert.ToDouble(hour + "." + minutes);
                ret[1] = ret[0] * _RATE;

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}