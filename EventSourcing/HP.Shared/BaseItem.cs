using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HP.Shared
{
    public class BaseEntity : NotifyingObject
    {
        public int Id { get
            {
                return _id;
            }
            set
            {
                if (_id == value) 
                {
                    return;
                }
                _id = value;
                NotifyPropertyChanged();
            }
        }
        private int _id;
    }

    public class BaseItem  : BaseEntity
    {
        public int ParentId { get => _parentId;
            set => SetProperty(ref _parentId, value); 
        }
        private int _parentId;
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