using HP.Application.Commands;
using HP.Application.DTOs;
using HP.Application.Handlers;
using HP.Application.Queries.Todos;
using HP.Domain;
using HP.GeneralUI.DropdownControl;
using HP.Shared;
using HP.Shared.Contacts;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorUI.Pages
{
    public class TodoBase : ComponentBase
    {
        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public NavigationManager NavigationManager {get; set; }
        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }
        [Parameter]
        public string TodoId {get; set;}
        public TodoDetailsDto TodoDetails { get; private set; } = new();
        public TodoDetailsDto TodoDetailsFromTodoSearch { get; private set; }
        public IEnumerable<TodoDetailsDto> Todos { get; set; } = new List<TodoDetailsDto>();
        protected EditContext EditContext { get; set; }
        protected CreateTodoModel CreateTodoModel { get; set; } = new();
        protected IList<DropdownItem<TodoType>> TodoTypeEnums { get; } = new List<DropdownItem<TodoType>>();
        protected DropdownItem<TodoType> SelectedTodoTypeDropDownItem { get; set; }
        protected IList<DropdownItem<TodoStatus>> TodoStatusEnums { get; } = new List<DropdownItem<TodoStatus>>();

        public string TodoIdInput { get; set; }
        public TodoBase()
        {

            var todoInfo = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Work,
                DisplayText = "Work"
            };
            var todoInfo2 = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Research,
                DisplayText = "Research"
            };
            var todoInfo3 = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Others,
                DisplayText = "Others"
            };
            var todoInfo4 = new DropdownItem<TodoType>
            {
                ItemObject = TodoType.Chores,
                DisplayText = "Chores"
            };
            TodoTypeEnums.Add(todoInfo);
            TodoTypeEnums.Add(todoInfo2);
            TodoTypeEnums.Add(todoInfo3);
            TodoTypeEnums.Add(todoInfo4);

            var todoStatusValue = new DropdownItem<TodoStatus>
            {
                ItemObject = TodoStatus.Started,
                DisplayText = "Start"
            };



            SelectedTodoTypeDropDownItem = todoInfo3;

        }
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            EditContext = new EditContext(CreateTodoModel);
            var temp_username = CurrentUserService.CurrentUser.UserName;
            Todos = await Mediator.Send(new GetTodosByUserId(temp_username));
        }
        public async void SearchChanged(string value)
        {
            var getTodo= await Mediator.Send(new GetTodoById(value));
            if (getTodo == null)
                TodoDetailsFromTodoSearch = null;
            TodoDetailsFromTodoSearch = getTodo;
        }

        protected async void OnSubmit()
        {
            TodoType todoType =SelectedTodoTypeDropDownItem.ItemObject;
            TodoDetailsDto newTodo = await Mediator.Send(new CreateTodoCommand(CreateTodoModel.UserId, CreateTodoModel.Title, todoType.Name,CreateTodoModel.Description));
            NavigationManager.NavigateTo("todos");
        }
        protected void OnClickViewDetails(string todoId)
        {
            NavigationManager.NavigateTo($"todos/details/{todoId}");
        }
        protected void OnClickGoToCreateTodo()
        {
            NavigationManager.NavigateTo("todos/create");
        }
        protected IOrderedEnumerable<IGrouping<string, TodoDetailsDto>> GetTodosByUserName()
        {
            return null;
        }
        protected async void OnClickRemoveTodo(string todoId)
        {
            await Mediator.Send(new DeleteTodoCommand(todoId));
        }
    }
}
