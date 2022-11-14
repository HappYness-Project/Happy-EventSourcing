using HP.Domain.Common;
namespace HP.Domain.Categories
{
    public class Category : BaseItem,  IAggregateRoot
    {
        public string PersonId { get; set; }
        public IList<CategoryItem> Items { get; set; }

    }
}
