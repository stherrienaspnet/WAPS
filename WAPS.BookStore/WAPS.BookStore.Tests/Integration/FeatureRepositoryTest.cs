using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAPS.BookStore.Domain.Repositories.Concrete;

namespace MTTWebAPISeed.Tests.Integration
{
	[TestClass]
	public class FeatureRepositoryTest
	{
		[TestMethod]
		public void TestGetAllReturnsEveryRowsOfTheFeatureTable()
		{
			var repository = new FeatureRepository();
			var features = repository.GetAll();
		}

		[TestMethod]
		public void TestCanUserAccessFeature()
		{
			var repository = new FeatureRepository();
			var canAccess = repository.CanUserAccessFeature("user5@gmail.com", "/api/board/remove");
			var canAccess2 = repository.CanUserAccessFeature("user5@gmail.com", "/api/board/retrieve");
		}
	}
}
