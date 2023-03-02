using HP.Application.DTOs;

namespace BlazorUI.Services.Todo
{
    public class TodoEventArgs
    {//http://www.binaryintellect.net/articles/c5feae9d-0dcc-419a-a4c8-1cd93379ab8a.aspx
     // TODO : Need to create Data Button.
     //public DataButton Button { get; set; }

        public TodoDetailsDto Item { get; set; }
    }
}
