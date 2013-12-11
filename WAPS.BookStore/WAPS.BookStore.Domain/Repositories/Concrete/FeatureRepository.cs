using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using MTTWebAPI.Domain.Entities;
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
                var sql = new StringBuilder();
                sql.Append("SELECT COUNT(*) FROM ");
                sql.Append("(	SELECT UserId FROM ");
                sql.Append("	dbo.UserFeature UF JOIN Feature F ON UF.FeatureId = F.FeatureId ");
                sql.Append("	WHERE F.Url = '" + featureUrl + "' AND F.IsActive = 1 ");
                sql.Append("UNION ");
                sql.Append("	SELECT UserId FROM ");
                sql.Append("	dbo.webpages_UsersInRoles UIR JOIN ( ");
                sql.Append("	SELECT RoleId FROM ");
                sql.Append("	dbo.RoleFeature RF JOIN Feature F ON RF.FeatureId = F.FeatureId ");
                sql.Append("	WHERE F.Url = '" + featureUrl + "' AND F.IsActive = 1) AGR ON UIR.RoleId = AGR.RoleId ");
                sql.Append(")TB JOIN UserProfile UP ON TB.UserId = UP.UserId ");
                sql.Append("WHERE UP.Email = '" + username + "'");

                return connection.Query<int>(sql.ToString()).Single() > 0;
			}
		}
	}
}
