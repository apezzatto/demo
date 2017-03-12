using MaryHelp.Business;
using System;

public partial class approve : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Job job = null;

        try
        {
            if (Request.QueryString.Count > 0)
            {
                job = new Job();
                job.Id = Request.QueryString["jobId"].ToString();
                job.LoadJobDetails();

                if (job.Worker != null)
                {
                    workerdetails.Visible = true;
                    lblName.Text = job.Worker.Name + " " + job.Worker.LastName;
                    lblAddress.Text = job.Worker.Address1 + " " + job.Worker.Address2;
                    lblEmail.Text = job.Worker.EMail;
                    lblCity.Text = job.Worker.City.Name;
                    lblNeighborhood.Text = job.Worker.Neighborhood.Name;

                    lblWhen.Text = job.DateTimeJobStarts.ToShortDateString();
                    lblTime.Text = job.DateTimeJobStarts.ToShortTimeString();
                    lblHours.Text = job.HoursForConclusion.ToString().Replace('.',':') + " hours";
                    lblFinishTime.Text = job.EstimatedEndTime.ToString();
                    lblPrice.Text = "CAD$" + job.Price.ToString();

                    ViewState["job"] = job;
                }
                else
                {
                    workerdetails.Visible = false;
                    lblMessage.Text = "Ooooops...Looks like you have already took an action about this job. Just relax and let the mess with us! ;)";
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        finally
        {
            job = null;
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        Client client = null;

        try
        {
            client = new Client();
            client.ApproveCandidate((Job)ViewState["job"]);
            lblMessage.Text = "CONGRATULATION! You have approved your candidate. Now relax, and let the mess with us! ;)";
            workerdetails.Visible = false;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        finally
        {
            client = null;
        }
    }

    protected void btnDeny_Click(object sender, EventArgs e)
    {
        Client client = null;

        try
        {
            client = new Client();
            client.ReproveCandidate((Job)ViewState["job"]);
            lblError.Text = ":( We are sorry for seeing you having to reprove this candidate! Maybe next time, right? ;)";
            workerdetails.Visible = false;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        finally
        {
            client = null;
        }
    }
}