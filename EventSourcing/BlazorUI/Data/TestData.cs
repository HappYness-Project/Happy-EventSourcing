using HP.Shared;
using System.Collections.ObjectModel;

namespace BlazorUI.Data
{
    public class TestData
    {
        public static User TestUser { get; private set; }
        public static void CreateTestUser()
        {
            var user = new User();
            user.UserName = "hyunbin7303";
            user.FirstName = "Kevin";
            user.LastName = "Park";
            user.Password = "test";
            user.UserType = "Normal";
            user.TodoItems = new ObservableCollection<BaseItem>();

            var textItem = new TextItem();
            textItem.ParentId = user.Id;
            user.TodoItems.Add(textItem);
            textItem.Id = 1;
            textItem.Title = "Buy Apples";
            textItem.SubTitle = "Red | 5";
            textItem.Desc = "From New Zealand preferred HEY YO";
            textItem.ItemTypeEnum = ItemTypeEnum.Text;
            textItem.Position = 1;

            var parentItem = new ParentItem();
            parentItem.ParentId = user.Id;
            user.TodoItems.Add(parentItem);
            parentItem.Id = 3;
            parentItem.Title = "Make Birthday Present";
            parentItem.ItemTypeEnum = ItemTypeEnum.Parent;
            parentItem.Position = 3;
            parentItem.ChildItems = new ObservableCollection<ChildItem>();

            var childItem = new ChildItem();
            childItem.ParentId = parentItem.Id;
            parentItem.ChildItems.Add(childItem);
            childItem.Id = 4;
            childItem.Position = 1;
            childItem.Title = "Cut";

            TestUser = user;
        }
    }
}
