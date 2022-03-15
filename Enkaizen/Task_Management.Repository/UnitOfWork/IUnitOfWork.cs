using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Repository.Interfaces;

namespace Task_Management.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository TaskRepository { get; }

        int Save();
    }
}
