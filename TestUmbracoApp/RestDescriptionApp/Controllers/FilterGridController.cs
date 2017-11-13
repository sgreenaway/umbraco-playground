using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UmbracoFilterGrid.Model;

namespace RestDescriptionApp.Controllers
{

    public class FilterGridController : ApiController
    {

        private IEnumerable<int> GetSelectedFilterIds(IEnumerable<FilterOption> options)
        {
            var ids = new List<int>();
            foreach (var f in options)
            {
                var s = f.Properties.Where(p => p.Selected).Select(x => x.NodeId);
                ids.AddRange(s);
            }
            return ids;
        }

        [HttpPost]
        public FilterGridModel GetData(
            FilterGridModel requestModel
        )
        {
            return requestModel;

        }

        //[HttpPost]
        //public PagedDataModel<ContentModel> GetData(
        //    FilterOptionsModel requestModel
        //    )
        //{
        //    var filters = GetSelectedFilterIds(requestModel.Options);

        //    return new PagedDataModel<ContentModel>();
        //}


    }
}
