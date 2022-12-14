using HP.Application.DTOs;
using HP.Domain;
using HP.GeneralUI.DropdownControl;
using HP.Shared.Contacts;
using HP.Shared.Requests.Todos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorUI.Pages
{
    public class TodoBase : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ITodoService _todoService { get; set; }
        [Inject] public ICurrentUserService CurrentUserService { get; set; }
        [Parameter] public string TodoId { get; set; }
        public TodoDetailsDto TodoDetailsFromTodoSearch { get; private set; }
        public IEnumerable<TodoDetailsDto> Todos { get; set; } = new List<TodoDetailsDto>();
        private string _deleteTodoId { get; set; } = string.Empty;
        protected EditContext EditContext { get; set; }
        protected CreateTodoDto CreateTodoRequest { get; set; } = new();
        protected IList<DropdownItem<TodoType>> TodoTypeEnums { get; } = new List<DropdownItem<TodoType>>();
        protected DropdownItem<TodoType> SelectedTodoTypeDropDownItem { get; set; }
        protected IList<DropdownItem<TodoStatus>> TodoStatusEnums { get; } = new List<DropdownItem<TodoStatus>>();
        protected DropdownItem<TodoStatus> SelectedTodoStatusDropDownItem { get; set; }
        public bool DeleteDialogOpen { get; set; }
        public string TodoIdInput { get; set; }
        public string CurrentUserName { get; set; }
        public TodoBase()
        {
            foreach (TodoType type in TodoType.List())
            {
                var ddItem = new DropdownItem<TodoType>
                {
                    ItemObject = type,
                    DisplayText = type.Name
                };
                TodoTypeEnums.Add(ddItem);
            }

            foreach (TodoStatus type in TodoStatus.List())
            {
                var ddStatus = new DropdownItem<TodoStatus>
                {
                    ItemObject = type,
                    DisplayText = type.Name
                };
                TodoStatusEnums.Add(ddStatus);
            }
            SelectedTodoTypeDropDownItem = TodoTypeEnums[0];
            SelectedTodoStatusDropDownItem = TodoStatusEnums[0];
        }
        protected override async Task OnInitializedAsync()
        {
            CurrentUserName = CurrentUserService.CurrentUser.UserName;
            EditContext = new EditContext(CreateTodoRequest);
            await LoadData();
        }
        public async void SearchChanged(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            var getTodo = await _todoService.GetTodoById(value);
            if (getTodo.IsSuccess)
            {
                TodoDetailsFromTodoSearch = getTodo.Data;
                StateHasChanged();
                return;
            }
            TodoDetailsFromTodoSearch = null;

        }
        private async Task LoadData()
        {
            var temp_username = CurrentUserService.CurrentUser.UserName;
            var result = await _todoService.GetTodosByPersonId(temp_username);
            if(result.IsSuccess)
            {
                Todos = result.Data;
                StateHasChanged();
            }
        }
        protected async void OnSubmit()
        {
            TodoType todoType = SelectedTodoTypeDropDownItem.ItemObject;
            CreateTodoRequest.TodoType = todoType.ToString();
            var result = await _todoService.CreateAsync(CreateTodoRequest);
            if(result.IsSuccess)
            {
                NavigationManager.NavigateTo("todos");
                return;
            }
            // TODO : Need to display error msg for failure
        }
        protected void OnClickGoToCreateTodo()
        {
            NavigationManager.NavigateTo("todos/create");
        }
        protected async Task OnDeleteDialogClose(bool accepted)
        {
            if (accepted)
            {
                await _todoService.DeleteAsync(_deleteTodoId);
                _deleteTodoId = string.Empty;
            }
            DeleteDialogOpen = false;
            await LoadData();
        }
    }
}
