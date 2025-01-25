namespace ToDoList.Server.Repository
{
    using Moq;
    using System.Collections.Generic;
    using ToDoList.Server.Model;
    using ToDoList.Server.Business;
    using Xunit;
    using ToDoList.Server.Repository;

    public class ToDoListRepoTest
    {
        private readonly Mock<IToDoListDBContext> _mockContext;
        private readonly ToDoListRepo _repo;

        public ToDoListRepoTest()
        {
            _mockContext = new Mock<IToDoListDBContext>();
            _repo = new ToDoListRepo(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllTaskList_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<ToDoTask>
            {
                new ToDoTask { Id = 1, Name = "Task 1", Completed = false },
                new ToDoTask { Id = 2, Name = "Task 2", Completed = true }
            };
            _mockContext.Setup(c => c.GetTasks()).Returns(Task.FromResult(tasks));

            // Act
            var result = await _repo.GetAllTaskList();

            // Assert
            Assert.Equal(tasks, result);
        }

        [Fact]
        public async Task AddTask_ShouldAddTask()
        {
            // Arrange
            var task = new ToDoTask { Name = "Task 1", Completed = false };
            _mockContext.Setup(c => c.AddTask(task)).Returns(Task.FromResult(true));

            // Act
            var result = await _repo.AddTask(task);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddTask_ShouldNotAddDuplicateTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            _mockContext.Setup(c => c.AddTask(task)).Returns(Task.FromResult(false));

            // Act
            var result = await _repo.AddTask(task);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteTask_ShouldRemoveTask()
        {
            // Arrange
            var taskId = 1;
            _mockContext.Setup(c => c.DeleteTask(taskId)).Returns(Task.FromResult(true));

            // Act
            var result = await _repo.DeleteTask(taskId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTask_ShouldReturnFalseIfTaskNotFound()
        {
            // Arrange
            var taskId = 1;
            _mockContext.Setup(c => c.DeleteTask(taskId)).Returns(Task.FromResult(false));

            // Act
            var result = await _repo.DeleteTask(taskId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateTask_ShouldUpdateTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Updated Task 1", Completed = true };
            _mockContext.Setup(c => c.UpdateTask(task)).Returns(Task.FromResult(true));

            // Act
            var result = await _repo.UpdateTask(task);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateTask_ShouldReturnFalseIfTaskNotFound()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            _mockContext.Setup(c => c.UpdateTask(task)).Returns(Task.FromResult(false));

            // Act
            var result = await _repo.UpdateTask(task);

            // Assert
            Assert.False(result);
        }
    }
}
