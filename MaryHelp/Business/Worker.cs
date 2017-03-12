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
    public class Worker : User
    {
        public void ApplyForJobs(List<Job> jobs)
        {
            Data data = null;

            try
            {
                if (CheckJobsStartingDateTime(jobs))
                {
                    if (CheckJobsStartingDateTime(jobs, GetJobsAssigned()))
                    {
                        data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString(), true);

                        foreach (Job job in jobs)
                        {
                            List<MySqlParameter> p = new List<MySqlParameter>()
                            {
                                new MySqlParameter() { ParameterName = "@p_id_job", Value = job.Id },
                                new MySqlParameter() { ParameterName = "@p_id_worker", Value = base.Id },
                                new MySqlParameter() { ParameterName = "@p_status_job", Value = job.Status },
                            };
                            data.ExecuteNonQuery("SP_UPD_JOB_STATUS", p);

                            Email.SendEmailForApproval(job.Client.EMail, job.Id);
                        }
                        data.Commit();
                    }
                    else
                        throw new ApplicationException("Your new job application conflicts with one or more jobs already applied before. Please, review your application.");
                }
                else
                    throw new ApplicationException("One job's estimated end time is bigger than another job's start time that you are trying to apply. Choose only one of them.");
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

        private List<Job> GetJobsAssigned()
        {
            List<Job> jobs = null;
            Job job = null;
            Data data = null;
            DataSet ds = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_worker", Value = this.Id },
                };

                ds = data.ExecuteQuery("SP_SEL_JOBS_APPLIED", p);

                jobs = new List<Job>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        job = new Job();
                        job.Id = row["idJob"].ToString();
                        job.DateTimeJobStarts = Convert.ToDateTime(row["dtJobStarts"].ToString());
                        job.HoursForConclusion = Convert.ToDouble(row["hoursConclusion"].ToString());

                        jobs.Add(job);
                    }
                }

                return jobs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                data.CloseConnection();
                jobs = null;
                job = null;
                data = null;
                ds = null;
            }
        }

        private bool CheckJobsStartingDateTime(List<Job> jobsWanted)
        {
            bool ret = true;

            foreach (Job job in jobsWanted)
            {
                foreach (Job job1 in jobsWanted)
                {
                    if (job != job1)
                    {
                        if (job1.DateTimeJobStarts.Date == job.DateTimeJobStarts.Date)
                        {
                            if (job.EstimatedEndTime >= job1.DateTimeJobStarts)
                            {
                                ret = false;
                                break;
                            }
                        }
                    }
                }
                if (!ret)
                    break;
            }
            return ret;
        }

        private bool CheckJobsStartingDateTime(List<Job> jobsWanted, List<Job> jobsAssigned)
        {
            bool ret = true;

            foreach (Job job in jobsWanted)
            {
                foreach (Job job1 in jobsAssigned)
                {
                    if (job != job1)
                    {
                        if (job1.DateTimeJobStarts.Date == job.DateTimeJobStarts.Date)
                        {
                            if (job.EstimatedEndTime >= job1.DateTimeJobStarts)
                            {
                                ret = false;
                                break;
                            }
                        }
                    }
                }
                if (!ret)
                    break;
            }
            return ret;
        }

        public void GiveUpJob(Job job)
        {
            throw new System.NotImplementedException();
        }

        public List<Job> GetMyHistoryByCity(int cityId)
        {
            Data data = null;
            DataRowCollection dr = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_worker", Value = this.Id },
                    new MySqlParameter() { ParameterName = "@p_id_city", Value = cityId },
                };

                dr = data.ExecuteQuery("SP_SEL_HISTORY_BY_CITY", p).Tables[0].Rows;

                return this.FillMyHistory(dr);
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
            }
        }

        public List<Job> GetMyHistoryByNeighborhood(int neighborhoodId)
        {
            Data data = null;
            DataRowCollection dr = null;

            try
            {
                data = new Data(ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString());

                List<MySqlParameter> p = new List<MySqlParameter>()
                {
                    new MySqlParameter() { ParameterName = "@p_id_worker", Value = this.Id },
                    new MySqlParameter() { ParameterName = "@p_id_city", Value = neighborhoodId },
                };

                dr = data.ExecuteQuery("SP_SEL_HISTORY_BY_NEIGHBORHOOD", p).Tables[0].Rows;

                return this.FillMyHistory(dr);
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

        private List<Job> FillMyHistory(DataRowCollection dr)
        {
            Job job = null;
            List<Job> jobs = null;

            try
            {
                jobs = new List<Job>();
                foreach (DataRow row in dr)
                {
                    job = new Job();

                    job = new Job();
                    job.Id = row["idJob"].ToString();
                    job.DateTimeJobStarts = Convert.ToDateTime(row["dtJobStarts"].ToString());

                    if (!row["dtJobEnd"].ToString().Equals(string.Empty))
                        job.DateTimeJobEnd = Convert.ToDateTime(row["dtJobEnd"].ToString());
                    else
                        job.DateTimeJobEnd = Convert.ToDateTime("01/01/0001");

                    job.Client = new Client();
                    job.Client.Name = row["ClientName"].ToString();
                    job.Client.City = new City();
                    job.Client.City.Name = row["City"].ToString();
                    job.Client.Neighborhood = new Neighborhood();
                    job.Client.Neighborhood.Name = row["Neighborhood"].ToString();
                    job.QtyRooms = Convert.ToInt32(row["qtyRooms"].ToString());
                    job.QtyBaths = Convert.ToInt32(row["qtyBaths"].ToString());
                    job.QtyWindows = Convert.ToInt32(row["qtyWindows"].ToString());
                    job.QtyMirrors = Convert.ToInt32(row["qtyMirrors"].ToString());
                    job.Vacuum = Convert.ToBoolean(row["vacuum"].ToString());
                    job.Laundry = Convert.ToBoolean(row["laundry"].ToString());
                    job.Garage = Convert.ToBoolean(row["garage"].ToString());
                    job.Dishes = Convert.ToBoolean(row["dishes"].ToString());
                    job.Status = (Job.STATUS)Convert.ToInt32(row["jobOpen"].ToString());
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

        public List<Job> GetJobsToBeDone()
        {
            throw new System.NotImplementedException();
        }
    }
}