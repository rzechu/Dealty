using Swashbuckle.AspNetCore.Annotations;

namespace Dealty.WebApi.Data
{
    public class Category : Shared.Data.Category
    {
        [SwaggerSchema(ReadOnly = true)]
        public override int CategoryID { get => base.CategoryID; set => base.CategoryID = value; }
    }
}