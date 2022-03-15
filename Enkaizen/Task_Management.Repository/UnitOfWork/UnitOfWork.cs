using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Data.Data;
using Task_Management.Repository.Interfaces;
using Task_Management.Repository.Repository;

namespace Task_Management.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;
        private TaskRepository taskRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ITaskRepository TaskRepository
        {
            get
            {
                return taskRepository ?? (taskRepository = new TaskRepository(context));
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
