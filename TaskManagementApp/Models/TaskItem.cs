using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TaskManagementApp.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Status { get; set; }   

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string? CreatedById { get; set; }

        public string? CreatedByName { get; set; }
        
        public string? UpdatedById { get; set; }
      
        public string? UpdatedByName { get; set; }
    }
}
