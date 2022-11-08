﻿using System.ComponentModel.DataAnnotations;
namespace HP.Shared
{
    public class CreateTodoItemModel
    {
        [Required]
        public string TodoId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string TodoType { get; set; }
        
    }
}
