using System.ComponentModel.DataAnnotations;
using ToDoList.Server.Business;

namespace ToDoList.Server.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        
        public bool Completed { get; set; }
    }
}
