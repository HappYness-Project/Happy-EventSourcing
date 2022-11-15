using HP.Domain.Common;
namespace HP.Domain.Categories
{
    public class Category : BaseItem,  IAggregateRoot
    {
        public IList<CategoryItem> Items { get; set; }

    }
}
