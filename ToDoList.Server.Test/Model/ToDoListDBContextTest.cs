using System.Collections.Generic;
using Moq;
using ToDoList.Server.Business;
using ToDoList.Server.Model;
using Xunit;
using Microsoft.Extensions.Logging;

namespace ToDoList.Server.Test.Model
{
    public class ToDoListDBContextTest
    {
        private readonly Mock<IDataStorage<ToDoTask>> _mockDataStorage;
        private readonly ToDoListDBContext _context;
        private readonly Mock<ILogger<ToDoListDBContext>> _mockLogger;

        public ToDoListDBContextTest()
        {
            _mockLogger = new Mock<ILogger<ToDoListDBContext>>();
            _mockDataStorage = new Mock<IDataStorage<ToDoTask>>();
            _context = new ToDoListDBContext(_mockDataStorage.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<ToDoTask>
            {
                new ToDoTask { Id = 1, Name = "Task 1", Completed = false },
                new ToDoTask { Id = 2, Name = "Task 2", Completed = true }
            };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(Task.FromResult(tasks));

            // Act
            var result = await _context.GetTasks();

            // Assert
            Assert.Equal(tasks, result);
        }

        [Fact]
        public async Task AddTask_ShouldAddTask()
        {
            // Arrange
            var task = new ToDoTask { Name = "Task 1", Completed = false };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(Task.FromResult(new List<ToDoTask>()));
            _mockDataStorage.Setup(ds => ds.Add(task));

            // Act
            var result = await _context.AddTask(task);

            // Assert
            Assert.True(result);
            _mockDataStorage.Verify(ds => ds.Add(task), Times.Once);
        }

        [Fact]
        public async Task AddTask_ShouldNotAddDuplicateTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            var tasks = new List<ToDoTask> { task };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(Task.FromResult(tasks));

            // Act
            var result = await _context.AddTask(task);

            // Assert
            Assert.False(result);
            _mockDataStorage.Verify(ds => ds.Add(It.IsAny<ToDoTask>()), Times.Never);
        }

        [Fact]
        public async Task DeleteTask_ShouldDeleteTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            var tasks = new List<ToDoTask> { task };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(Task.FromResult(tasks));
            _mockDataStorage.Setup(ds => ds.Remove(task));

            // Act
            var result = await _context.DeleteTask(task.Id);

            // Assert
            Assert.True(result);
            _mockDataStorage.Verify(ds => ds.Remove(task), Times.Once);
        }

        [Fact]
        public async Task UpdateTask_ShouldUpdateTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            var updatedTask = new ToDoTask { Id = 1, Name = "Updated Task", Completed = true };
            var tasks = new List<ToDoTask> { task };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(Task.FromResult(tasks));
            _mockDataStorage.Setup(ds => ds.Update(updatedTask));

            // Act
            var result = await _context.UpdateTask(updatedTask);

            // Assert
            Assert.True(result);
            _mockDataStorage.Verify(ds => ds.Update(updatedTask), Times.Once);
        }
    }
}