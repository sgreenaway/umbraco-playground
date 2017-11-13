using System.Collections.Generic;
using UmbracoFilterGrid.Model;

namespace UmbracoFilterGrid.Services
{
    public interface IFilterGridService
    {
        int DocTypeId { get; set; }
        IEnumerable<int> SelectedCategories { get; set; }
        int FilterRoot { get; set; }
        IEnumerable<FilterOption> GetFilterSet();
        PagedDataModel<ContentModel> GetData(int pageNumber, int pageSize);
    }

}
