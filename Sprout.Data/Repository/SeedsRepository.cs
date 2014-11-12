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
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime? lastPledgeDate = null;

                        if (reader["last_pledge_date"] is DBNull)
                        {
                            lastPledgeDate = null;
                        }
                        else
                        {
                            lastPledgeDate = Convert.ToDateTime(reader["last_pledge_date"]);
                        }

                        var project = new Project()
                        {
                            Active = Convert.ToBoolean(reader["active"]),
                            AmountFunded = Math.Round(Convert.ToDecimal(reader["amount_funded"]), 2),
                            Description = reader["description"].ToString(),
                            FundingGoal = Math.Round(Convert.ToDecimal(reader["funding_goal"]), 2),
                            LastPledgeDate = lastPledgeDate,
                            Location = reader["location"].ToString(),
                            OriginationDate = Convert.ToDateTime(reader["origination_date"]),
                            PercentageFunded = Math.Round(Convert.ToDecimal(reader["percentage_funded"]), 2),
                            ProjectId = Convert.ToInt32(reader["project_id"]),
                            ProjectName = reader["project_name"].ToString(),
                            ProjectOriginator = reader["project_originator"].ToString(),
                            ProjectOriginatorId = Convert.ToInt32(reader["project_originator_id"]),
                            StageId = Convert.ToInt32(reader["project_stage_id"]),
                            Summary = reader["summary"].ToString(),
                            TitleThumbImageLink = reader["title_thumb_image_link"].ToString()
                        };

                        activeSeedProjects.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return activeSeedProjects;
        }

        public Project GetActiveSeedProjectById(int project_id)
        {
            var project = new Project();

            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SproutDatabase"]))
                using (SqlCommand cmd = new SqlCommand("GetActiveSeedProjectsById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@project_id", SqlDbType.Int).Value = project_id;
                    cmd.Connection.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime? lastPledgeDate = null;

                        if (reader["last_pledge_date"] is DBNull)
                        {
                            lastPledgeDate = null;
                        }
                        else
                        {
                            lastPledgeDate = Convert.ToDateTime(reader["last_pledge_date"]);
                        }

                        project = new Project()
                        {
                            Active = Convert.ToBoolean(reader["active"]),
                            AmountFunded = Math.Round(Convert.ToDecimal(reader["amount_funded"]), 2),
                            Description = reader["description"].ToString(),
                            FundingGoal = Math.Round(Convert.ToDecimal(reader["funding_goal"]), 2),
                            LastPledgeDate = lastPledgeDate,
                            Location = reader["location"].ToString(),
                            OriginationDate = Convert.ToDateTime(reader["origination_date"]),
                            PercentageFunded = Math.Round(Convert.ToDecimal(reader["percentage_funded"]), 2),
                            ProjectId = Convert.ToInt32(reader["project_id"]),
                            ProjectName = reader["project_name"].ToString(),
                            ProjectOriginator = reader["project_originator"].ToString(),
                            ProjectOriginatorId = Convert.ToInt32(reader["project_originator_id"]),
                            StageId = Convert.ToInt32(reader["project_stage_id"]),
                            Summary = reader["summary"].ToString(),
                            TitleThumbImageLink = reader["title_thumb_image_link"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return project;
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