using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Management.API.DTOs;
using Task_Management.API.Models;
using Task_Management.Data.Models;
using Task_Management.Repository.UnitOfWork;

namespace Task_Management.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public TaskController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskDto taskDto)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                var task = _mapper.Map<Data.Models.Task>(taskDto);

                task.Status = Data.StaticData.TaskStatus.Pending;
                task.CreatedById = user.Id;
                task.CreatedDate = DateTime.Now;

                await _unitOfWork.TaskRepository.Add(task);
                _unitOfWork.Save();

                return Ok(new Response<Data.Models.Task> { 
                    Result = task,
                    Success = true,
                    Message = "Task created successfully"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Data.Models.Task>
                {
                    Result = null,
                    Success = false,
                    Message = "Failed to create task"
                });
            }

        }


        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public IActionResult GetTasks()
        {
            try
            {
                var tasks = _unitOfWork.TaskRepository.GetAllTask();

                var tasksDtos = _mapper.Map<List<TaskDto>>(tasks);

                return Ok(tasksDtos);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                var task = _unitOfWork.TaskRepository.GetTaskById(taskId);

                if (task == null)
                {
                    return NotFound(new Response<Data.Models.Task>
                    {
                        Result = null,
                        Success = false,
                        Message = "There is no task on this id"
                    });
                }

                task.IsDeleted = true;
                task.DeletedById = user.Id;
                task.DeletedDate = DateTime.Now;
                _unitOfWork.Save();

                return Ok(new Response<Data.Models.Task>
                { 
                    Result = task,
                    Success = true,
                    Message = "Task deleted successfully!"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Data.Models.Task>
                {
                    Result = null,
                    Success = false,
                    Message = "Error occured on deleting task"
                });
            }
        }


        [Authorize(Roles = "Admin,User")]
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTaskStatusAsDone(int taskId)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                var task = _unitOfWork.TaskRepository.GetTaskById(taskId);

                if (task == null)
                {
                    return NotFound(new Response<Data.Models.Task>
                    {
                        Result = null,
                        Success = false,
                        Message = "There is no task on this id"
                    });
                }

                task.Status = Data.StaticData.TaskStatus.Done;
                task.DoneById = user.Id;
                task.DoneDate = DateTime.Now;
                _unitOfWork.Save();

                return Ok(new Response<Data.Models.Task>
                {
                    Result = task,
                    Success = true,
                    Message = "Task done successfully!"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Data.Models.Task>
                {
                    Result = null,
                    Success = false,
                    Message = "Error occured on updating task"
                });
            }
        }
    }
}
