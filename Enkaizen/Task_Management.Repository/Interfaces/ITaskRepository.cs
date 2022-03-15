using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Data.Models;

namespace Task_Management.Repository.Interfaces
{
    public interface ITaskRepository : IRepository<Task>
    {
        List<Task> GetAllTask();

        Task GetTaskById(int id);
    }
}
