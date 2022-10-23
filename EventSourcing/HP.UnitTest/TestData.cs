using HP.Application.DTOs;
using HP.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.UnitTest
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
            user.TodoItems = new ObservableCollection<TodoBasicInfoDto>();

            var TodoBasicInfoDto = new TodoBasicInfoDto();
            TodoBasicInfoDto.UserId = user.UserName;
            user.TodoItems.Add(TodoBasicInfoDto);

            TestUser = user;
        }
    }
}
