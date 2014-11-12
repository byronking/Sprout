using Sprout.Data.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Data.Repository
{
    public static class UserRepository
    {
        public static SproutUser GetActiveSproutUserById(int sprout_user_id)
        {
            var user = new SproutUser();

            try
            {
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["SproutDatabase"]))
                using (SqlCommand cmd = new SqlCommand("GetActiveSproutUserById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sprout_user_id", SqlDbType.Int).Value = sprout_user_id;
                    cmd.Connection.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new SproutUser()
                        {
                            Active = Convert.ToBoolean(reader["active"]),
                            Bio = reader["bio"].ToString(),
                            CompanyId = Convert.ToInt32(reader["company_id"]),
                            CompanyName = reader["company_name"].ToString(),
                            Email = reader["email"].ToString(),
                            FirstName = reader["first_name"].ToString(),
                            LastLoginDate = Convert.ToDateTime(reader["last_login_date"]),
                            LastName = reader["last_name"].ToString(),
                            Password = reader["password"].ToString(),
                            SproutUserId = Convert.ToInt32(reader["sprout_user_id"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return user;
        }
    }
}