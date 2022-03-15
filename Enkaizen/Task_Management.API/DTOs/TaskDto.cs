using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Management.Data.Models;

namespace Task_Management.API.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }


        public ApplicationUser DoneByUser { get; set; }
        public string DoneById { get; set; }
        public DateTime DoneDate { get; set; }


        public ApplicationUser CreatedByUser { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }


        public ApplicationUser DeletedByUser { get; set; }
        public string DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
