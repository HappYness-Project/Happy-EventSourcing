namespace HP.Domain.Categories
{
    public class Category 
    {
        public int Id { get; set; }  
        public bool IsDone { get; set; }
        public IList<CategoryItem> Items { get; set; }

    }
}
