namespace HP.Application.Commands.Category
{
    public class CreateCategoryCommand
    {
        public string Name { get; set; }
        public string Desc { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
