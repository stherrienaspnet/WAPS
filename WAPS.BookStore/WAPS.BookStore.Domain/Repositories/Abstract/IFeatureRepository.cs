using System.Collections.Generic;
using MTTWebAPI.Domain.Entities;

namespace WAPS.BookStore.Domain.Repositories.Abstract
{
    public interface IFeatureRepository
    {
	    IEnumerable<Feature> GetAll();
	    bool CanUserAccessFeature(string username, string featureUrl);
    }
}
