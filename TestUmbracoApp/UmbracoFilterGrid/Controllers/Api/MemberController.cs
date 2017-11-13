using Umbraco.Web.WebApi;
using System.Web.Http;
using Umbraco.Core.Models;

namespace UmbracoFilterGrid.Controllers.Api
{
    public class MemberController : UmbracoApiController
    {
        [HttpGet]
        public string SetRelation()
        {
            var member = Services.MemberService.GetByUsername("sam");
            var content = Umbraco.TypedContent(1122);
            var reltype = Services.RelationService.GetRelationTypeByAlias("FavoriteProducts");
            var r = new Relation(member.Id, content.Id, reltype);
            r.Comment = content.Name;
            Services.RelationService.Save(r);
            return "Done";
        }
    }
}
