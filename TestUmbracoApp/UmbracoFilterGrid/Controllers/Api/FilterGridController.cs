using System.Collections.Generic;
using Umbraco.Web.WebApi;
using System.Linq;
using System.Web.Http;
using UmbracoFilterGrid.Model;
using UmbracoFilterGrid.Services;

namespace UmbracoFilterGrid.Controllers.Api
{

    public class FilterGridController : UmbracoApiController
    {
        private IFilterGridService _filterGridService;

        private IEnumerable<int> GetSelectedFilterIds(IEnumerable<FilterOption> options)
        {
            var ids = new List<int>();
            if (options != null)
            {
                ids.AddRange(options.Where(f => f.SelectedProperty != 0).Select(f => f.SelectedProperty));
            }
            return ids;
        }

        [HttpPost]
        public FilterOptionsModel GetFilterValues(FilterOptionsModel filterModel)
        {
            var filters = GetSelectedFilterIds(filterModel.Options);

            _filterGridService = new FilterGridService(filterModel.DocTypeId, filters, filterModel.FilterRoot, Umbraco);
            filterModel.Options = _filterGridService.GetFilterSet();
            return filterModel;
        }

        [HttpPost]
        public FilterGridModel GetData(
            FilterGridModel requestModel
            )
        {
            var filters = GetSelectedFilterIds(requestModel.FilterOptions.Options);

            _filterGridService = new FilterGridService(requestModel.FilterOptions.DocTypeId, filters, requestModel.FilterOptions.FilterRoot, Umbraco);
            requestModel.FilterOptions.Options = _filterGridService.GetFilterSet();
            requestModel.DataModel = _filterGridService.GetData(requestModel.DataModel.PageNumber, requestModel.DataModel.PageSize);
            return requestModel;

        }


    }
}
