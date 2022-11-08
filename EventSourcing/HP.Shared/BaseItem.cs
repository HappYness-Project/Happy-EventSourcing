using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HP.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }
    public class BaseItem : BaseEntity
    {
        public int ParentId { get; set; }
        public ItemTypeEnum ItemTypeEnum { get; set; }
        public int Position { get; set; }
        public bool IsDone { get; set; }
        public string Title { get; set; }
    }
    public class TextItem : BaseItem
    {
        public string SubTitle { get; set; }
        public string Desc { get; set; }
    }
    public class ChildItem : BaseItem { }
    public class ParentItem : BaseItem
    {
        public ObservableCollection<ChildItem> ChildItems { get; set; }
    }
    public enum ItemTypeEnum
    {
        Text = 1,
        Url = 2,
        Parent = 3,
        Child = 4
    }
}