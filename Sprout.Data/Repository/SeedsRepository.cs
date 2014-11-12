using Sprout.Data.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Data.Repository
{
    public class SeedsRepository
    {
        public List<Project> GetActiveSeedProjects()
        {
            var activeSeedProjects = new List<Project>();

            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SproutDatabase"]))
                using (SqlCommand cmd = new SqlCommand("GetActiveSeedProjects", conn))
                {

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return activeSeedProjects;
        }

        public ProjectSaveResults SaveSeedsProject(Project project)
        {
            var saveResults = new ProjectSaveResults()
            {
                ProjectNewId = 0,
                SaveSuccessful = false
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SproutDatabase"]))
                using (SqlCommand cmd = new SqlCommand("SaveSproutSeedsProject", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@project_name", SqlDbType.VarChar).Value = project.ProjectName;
                    cmd.Parameters.Add("@project_originator_id", SqlDbType.Int).Value = project.ProjectOriginatorId;
                    cmd.Parameters.Add("@project_stage_id", SqlDbType.Int).Value = project.StageId;
                    cmd.Parameters.Add("@title_thumb_image_link", SqlDbType.VarChar).Value = project.TitleThumbImageLink;
                    cmd.Parameters.Add("@summary", SqlDbType.VarChar).Value = project.Summary;
                    cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = project.Description;
                    cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = project.Location;
                    cmd.Parameters.Add("@active", SqlDbType.Bit).Value = project.Active;
                    cmd.Parameters.Add("@origination_date", SqlDbType.SmallDateTime).Value = project.OriginationDate;
                    cmd.Parameters.Add("@funding_goal", SqlDbType.Money).Value = project.FundingGoal;
                    cmd.Parameters.Add("@percentage_funded", SqlDbType.Decimal).Value = project.PercentageFunded;
                    cmd.Parameters.Add("@amount_funded", SqlDbType.Money).Value = project.AmountFunded;
                    cmd.Parameters.Add("@stage_id", SqlDbType.Int).Value = project.StageId;
                    cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = 9999;
                    cmd.Parameters.Add("@stage_begin_date", SqlDbType.SmallDateTime).Value = project.OriginationDate;

                    var returnParameter = cmd.Parameters.Add("@return_value", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    saveResults.ProjectNewId = (int)returnParameter.Value;

                    if (saveResults.ProjectNewId != 0)
                    {
                        saveResults.SaveSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return saveResults;
        }
    }
}