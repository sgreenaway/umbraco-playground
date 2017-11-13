using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbracoFilterGrid.Model;

namespace UmbracoFilterGrid.Services
{
    internal sealed class FilterGridService : IFilterGridService
    {
        public int DocTypeId { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }
        public int FilterRoot { get; set; }

        private readonly UmbracoHelper _umbraco;

        public FilterGridService(int docTypeId, IEnumerable<int> selectedCategories, int filterRoot, UmbracoHelper umbraco)
        {
            DocTypeId = docTypeId;
            SelectedCategories = selectedCategories;
            FilterRoot = filterRoot;
            _umbraco = umbraco;
        }

        private bool CheckFlterExistsInData(IEnumerable<int> categories)
        {
            return _umbraco.TypedContentAtRoot().First().Descendants().Where(p => p.DocumentTypeId == DocTypeId).ToList()
                .Any(p => categories.ContainsAny(p.GetPropertyValue<IEnumerable<IPublishedContent>>("categories")
                    .Select(i => i.Id)));
        }

        private IEnumerable<FilterProperty> GetProperties()
        {
            var ids = new List<FilterProperty>();

            foreach (var content in FilteredDataSet())
            {
                ids.AddRange(content.GetPropertyValue<IEnumerable<IPublishedContent>>("categories")
                    .Select(p =>
                        new FilterProperty
                        {
                            Name = p.Name,
                            NodeId = p.Id,
                        }));
            }
            ids = ids.DistinctBy(p => p.NodeId).ToList();
            return ids;
        }

        private IEnumerable<IPublishedContent> FilteredDataSet()
        {
            var data = _umbraco.TypedContentAtRoot().First().Descendants()
                .Where(p => p.DocumentTypeId == DocTypeId);
            if (SelectedCategories.Any())
            {
                foreach (var c in SelectedCategories)
                {
                    data = data.Where(p => p.GetPropertyValue<IEnumerable<IPublishedContent>>("categories")
                        .Select(i => i.Id).Contains(c));
                }
            }
            return data.ToList();
        }
        public IEnumerable<FilterOption> GetFilterSet()
        {
            var returnData = new List<FilterOption>();
            var filters = GetProperties().Select(p => p.NodeId);
            var rootItem = _umbraco.TypedContent(FilterRoot);
            foreach (var group in rootItem.Children.Where(p => p.DocumentTypeAlias == "categoryGroup"))
            {
                if (group.Children.Count() > 1 && CheckFlterExistsInData(group.Children.Select(p => p.Id)))
                {
                    var props = group.Children.Select(p => new FilterProperty { Name = p.Name, NodeId = p.Id }).ToList();

                    foreach (var prop in props.Where(p => filters.Contains(p.NodeId)))
                    {
                        prop.Active = true;
                    }
                    var selected = 0;

                    if (SelectedCategories.Any() && props.SingleOrDefault(p => SelectedCategories.Contains(p.NodeId)) != null)
                    {
                        selected = props.SingleOrDefault(p => SelectedCategories.Contains(p.NodeId)).NodeId;
                    }

                    var objs = new FilterOption
                    {
                        Name = group.Name,
                        Properties = props,
                        SelectedProperty = selected
                    };
                    returnData.Add(objs);
                }
            }
            return returnData;
        }

        public PagedDataModel<ContentModel> GetData(int pageNumber, int pageSize)
        {
            var qryData = FilteredDataSet().Select(c => new ContentModel
            {
                Title = c.GetPropertyValue<string>("ProductName"),
                Descrption = c.GetPropertyValue<string>("Description"),
                Categories = c.GetPropertyValue<IEnumerable<IPublishedContent>>("categories").Select(p => p.Name)
            }).AsQueryable();
            return CreatePagedResults(qryData, pageNumber, pageSize);
        }

        private PagedDataModel<ContentModel> CreatePagedResults(
            IQueryable<ContentModel> queryable,
            int page,
            int pageSize)
        {
            var skipAmount = pageSize * (page - 1);

            var projection = queryable
                .Skip(skipAmount)
                .Take(pageSize);

            var totalNumberOfRecords = queryable.Count();
            var results = projection.ToList();

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            return new PagedDataModel<ContentModel>
            {
                Results = results,
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords,
            };
        }

    }
}
