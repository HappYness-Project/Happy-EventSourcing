using System.Collections.ObjectModel;
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
        public int ParentId { 
            get => _parentId;
            set => SetProperty(ref _parentId, value); 
        }
        private int _parentId;
        public ItemTypeEnum ItemTypeEnum { 
            get => _itemTypeEnum;
            set => SetProperty(ref _itemTypeEnum, value);
        }
        private ItemTypeEnum _itemTypeEnum;
        public int Position { 
            get => _position;
            set => SetProperty(ref _position, value);
        }
        private int _position;
        public bool IsDone { 
            get => _isDone;
            set => SetProperty(ref _isDone, value);
        }
        private bool _isDone;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title;
    }
    public class TextItem : BaseItem
    {
        public string SubTitle
        {
            get => _subTitle;
            set => SetProperty(ref _subTitle, value);
        }
        private string _subTitle;
        public string Desc
        {
            get => _desc;
            set=> SetProperty(ref _desc, value);
        }
        private string _desc;
    }
    public class UrlItem : BaseItem
    {
        public string Url
        {
            get => _url; set => SetProperty(ref _url, value);   
        }
        private string _url;
    }
    public class ChildItem : BaseItem { }
    public class ParentItem : BaseItem
    {
        public ObservableCollection<ChildItem> ChildItems
        {
            get => _childItems;
            set
            {
                if(value == _childItems)
                {
                    return;
                }
                _childItems = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<ChildItem> _childItems;
    }
    public enum ItemTypeEnum
    {
        Text = 1,
        Url = 2,
        Parent = 3,
        Child = 4
    }
}