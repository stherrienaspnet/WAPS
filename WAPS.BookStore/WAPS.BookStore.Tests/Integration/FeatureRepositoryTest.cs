using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAPS.BookStore.Domain.Repositories.Concrete;

namespace WAPS.BookStore.Tests.Integration
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
            var canAccessWithRole = repository.CanUserAccessFeature("MTTWebAPIAdmin@gmail.com", "/api/board/remove");
			var canAccessWithProfile= repository.CanUserAccessFeature("user5@gmail.com", "/api/board/retrieve");
            Assert.IsTrue(canAccessWithRole);
            Assert.IsTrue(canAccessWithProfile);
		}
	}
}
