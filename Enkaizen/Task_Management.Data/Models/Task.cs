using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Data.Models
{
    public class Task
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
