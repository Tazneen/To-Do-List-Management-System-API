using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Task_Management.Data.Data;
using Task_Management.Data.Models;
using Task_Management.Repository.Interfaces;

namespace Task_Management.Repository.Repository
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<Task> GetAllTask()
        {
            List<Expression<Func<Task, object>>> includeParameters = new List<Expression<Func<Task, object>>>
            {
                m => m.CreatedByUser, m => m.DoneByUser
            };

            Expression<Func<Task, bool>> filterParameter = m => m.IsDeleted == false;

            Func<IQueryable<Task>, IOrderedQueryable<Task>> orderByParameters = m => m.OrderByDescending(m => m.CreatedDate);

            return GetAll(filter: filterParameter, includeProperties: includeParameters, orderBy: orderByParameters).ToList();
        }

        public Task GetTaskById(int id)
        {
            List<Expression<Func<Task, object>>> includeParameters = new List<Expression<Func<Task, object>>>
            {
                m => m.CreatedByUser, m => m.DoneByUser
            };

            Expression<Func<Task, bool>> filterParameter = m => m.IsDeleted == false && m.Id == id;

            Func<IQueryable<Task>, IOrderedQueryable<Task>> orderByParameters = m => m.OrderByDescending(m => m.CreatedDate);

            return GetAll(filter: filterParameter, includeProperties: includeParameters, orderBy: orderByParameters).FirstOrDefault();
        }
    }
}
