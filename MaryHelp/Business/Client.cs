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
    public class Client : User
    {
        public void CreateJob(Job job)
        {
            Data data = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_idClient", Value = job.Client.Id },
                    new MySqlParameter() { ParameterName = "@p_dtJobStarts", Value = job.DateTimeJobStarts },
                    new MySqlParameter() { ParameterName = "@p_dtJobFinishes", Value = job.EstimatedEndTime },
                    new MySqlParameter() { ParameterName = "@p_hoursConclusion", Value = job.HoursForConclusion },
                    new MySqlParameter() { ParameterName = "@p_qtyRooms", Value = job.QtyRooms },
                    new MySqlParameter() { ParameterName = "@p_qtyBaths", Value = job.QtyBaths },
                    new MySqlParameter() { ParameterName = "@p_qtyWindows", Value = job.QtyWindows },
                    new MySqlParameter() { ParameterName = "@p_qtyMirrors", Value = job.QtyMirrors },
                    new MySqlParameter() { ParameterName = "@p_vacuum", Value = job.Vacuum },
                    new MySqlParameter() { ParameterName = "@p_laundry", Value = job.Laundry },
                    new MySqlParameter() { ParameterName = "@p_garage", Value = job.Garage },
                    new MySqlParameter() { ParameterName = "@p_dishes", Value = job.Dishes },
                    new MySqlParameter() { ParameterName = "@p_price", Value = job.Price }
                };

                data.ExecuteNonQuery("SP_INS_JOB", p);
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
            }
        }

        public void ApproveCandidate(Job job)
        {
            Data data = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString(), true);

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_job", Value = job.Id },
                };
                data.ExecuteNonQuery("SP_UPD_APPROVE_WORKER", p);

                Email.SendApprovedEmail(job.Worker.EMail, job.Client.Name + " " + job.Client.LastName, job.Client.Address1 + " " + job.Client.Address2 + " - " + job.Client.City.Name + ", " + job.Client.Neighborhood.Name, job.DateTimeJobStarts.ToShortDateString(), job.DateTimeJobStarts.ToShortTimeString(), job.HoursForConclusion.ToString(), job.Price.ToString());

                data.Commit();
            }
            catch (Exception ex)
            {
                data.Rollback();
                throw ex;
            }
            finally
            {
                data.CloseConnection();
                data = null;
            }
        }

        public void ReproveCandidate(Job job)
        {
            Data data = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString(), true);

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_job", Value = job.Id },
                };
                data.ExecuteNonQuery("SP_UPD_REPROVE_WORKER", p);

                Email.SendReprovedEmail(job.Worker.EMail, job.Client.Name + " " + job.Client.LastName, job.DateTimeJobStarts.ToShortDateString(), job.DateTimeJobStarts.ToShortTimeString(), job.HoursForConclusion.ToString());

                data.Commit();
            }
            catch (Exception ex)
            {
                data.Rollback();
                throw ex;
            }
            finally
            {
                data.CloseConnection();
                data = null;
            }
        }

        public void CancelJob(Job Job)
        {
            throw new System.NotImplementedException();
        }


        public List<Job> GetJobsCreated()
        {
            throw new System.NotImplementedException();
        }
    }
}