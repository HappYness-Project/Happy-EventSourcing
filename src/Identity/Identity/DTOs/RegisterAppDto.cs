using Microsoft.AspNetCore.Mvc;
namespace Identity.DTOs
{
    public class RegisterAppDto
    {
        public string ApplicationName { get; set; }
        public string Email { get; set; }
        public string CallbakUri { get; set; }
        [BindProperty]
        public HashSet<string> SelectedApis { get; set; }

    }
}