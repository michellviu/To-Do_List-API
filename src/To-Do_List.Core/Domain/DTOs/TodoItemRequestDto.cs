using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Core.Domain.Entities;

namespace To_Do_List.Core.Domain.DTOs
{
    public class TodoItemRequestDto
    {
        public int Id { get; init; }

        [Required]
        public string Title { get; init; }

        [MaxLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; init; }

        [Range(1, 5, ErrorMessage = "The difficulty should be between 1 and 5.")]
        public int? Difficulty { get; init; }

        [Range(0, 2, ErrorMessage = "The status must be a value between 0 and 2.")]
        public Status Status { get; init; }
    }
}
