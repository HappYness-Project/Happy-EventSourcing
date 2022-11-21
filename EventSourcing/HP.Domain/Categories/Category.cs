using HP.Domain.Common;
namespace HP.Domain.Categories
{
    public class Category : BaseItem
    {
        public IList<CategoryItem> Items { get; set; }

    }
}
