using System.Collections.Generic;
using Umbraco.Core.Models;

namespace UmbracoFilterGrid.Model
{
    public class FilterOption
    {
        public string Name { get; set; }
        public IEnumerable<FilterProperty> Properties { get; set; }
        public int SelectedProperty { get; set; }
    }

    public class FilterProperty
    {
        public string Name { get; set; }
        public int NodeId { get; set; }
        //public bool Selected { get; set; }
        public bool Active { get; set; }
    }

    public class FilterOptionsModel
    {
        public int DocTypeId { get; set; }
        public IEnumerable<FilterOption> Options { get; set; }
        public int FilterRoot { get; set; }
        //public int Page { get; set; }
        //public int PageSize { get; set; }
    }

    public class FilterGridModel
    {
        public PagedDataModel<ContentModel> DataModel { get; set; }
        public FilterOptionsModel FilterOptions { get; set; }
    }

}
