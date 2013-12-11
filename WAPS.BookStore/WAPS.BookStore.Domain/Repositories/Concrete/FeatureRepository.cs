using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MTTWebAPI.Domain.Entities;
using WAPS.BookStore.Domain.Extenstions;
using WAPS.BookStore.Domain.Factories;
using WAPS.BookStore.Domain.Repositories.Abstract;

namespace WAPS.BookStore.Domain.Repositories.Concrete
{
	public class FeatureRepository : IFeatureRepository
	{
		public IEnumerable<Feature> GetAll()
		{
			using (SqlConnection connection = SqlConnectionFactory.Create())
			{
				return connection.Query<Feature>("select * from Feature");
			}
		}
		
		public bool CanUserAccessFeature(string username, string featureUrl)
		{
            using (SqlConnection connection = SqlConnectionFactory.Create())
            {
                string sqlCmd = @"SELECT COUNT(*) FROM 
                            (	SELECT UserId FROM 
	                            dbo.UserFeature UF JOIN Feature F ON UF.FeatureId = F.FeatureId 
	                            WHERE F.Url = '@featureUrl' AND F.IsActive = 1
                            UNION	
	                            SELECT UserId FROM
	                            dbo.webpages_UsersInRoles UIR JOIN ( 
	                            SELECT RoleId FROM
	                            dbo.RoleFeature RF JOIN Feature F ON RF.FeatureId = F.FeatureId 
	                            WHERE F.Url = '@featureUrl' AND F.IsActive = 1) AGR ON UIR.RoleId = AGR.RoleId
                            )TB JOIN UserProfile UP ON TB.UserId = UP.UserId
                            WHERE UP.Email = '@username'";

                sqlCmd = sqlCmd.Replace("@username", username).Replace("@featureUrl", featureUrl);
                return connection.Query<int>(sqlCmd.ToSqlFormat()).Single() > 0;
			}
		}
	}
}
