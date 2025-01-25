using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Server.Business;
using ToDoList.Server.DTO;
using ToDoList.Server.Repository;

namespace ToDoList.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;

        private readonly IToDoListRepo _dolistRepo;

        public TasksController(ILogger<TasksController> logger, IToDoListRepo dolistrepo)
        {
            _logger = logger;
            _dolistRepo = dolistrepo;
            
        }

        [HttpGet()]
        public async Task<List<TaskDTO>> GetTask()
        {
              var tasks = await _dolistRepo.GetAllTaskList();
            return tasks.Select(item => new TaskDTO { Id = item.Id, Name = item.Name, Completed = item.Completed }).ToList();
        }

        [HttpPost()]
        public async Task<bool> AddTask(TaskDTO task)
        {
            var newtask = new ToDoTask() { Name = task.Name, Completed = task.Completed };
            var tasks = await _dolistRepo.AddTask(newtask);
            return tasks;
        }

        

        [HttpDelete("{id}")]
        public async Task<bool> DeleteTask(int id)
        {
            
            return await _dolistRepo.DeleteTask(id);
        }


        [HttpPost("UpdateTask")]
        public async Task<bool> UpdateTask(TaskDTO task)
        {
            var updatetask = new ToDoTask() { Id = task.Id , Name = task.Name, Completed = task.Completed };
            return await _dolistRepo.UpdateTask(updatetask);
        }
    }
}
