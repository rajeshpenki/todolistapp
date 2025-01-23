using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Server.Business;
using ToDoList.Server.Controllers;
using ToDoList.Server.DTO;
using ToDoList.Server.Repository;
using Xunit;

namespace ToDoList.Tests.Controllers
{
    public class TasksControllerTest
    {
        private readonly Mock<ILogger<TasksController>> _mockLogger;
        private readonly Mock<IToDoListRepo> _mockRepo;
        private readonly TasksController _controller;

        public TasksControllerTest()
        {
            _mockLogger = new Mock<ILogger<TasksController>>();
            _mockRepo = new Mock<IToDoListRepo>();
            _controller = new TasksController(_mockLogger.Object, _mockRepo.Object);
        }

        [Fact]
        public void GetTask_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<ToDoTask> { new ToDoTask { Id = 1, Name = "Test Task", Completed = false } };
            _mockRepo.Setup(repo => repo.GetAllTaskList()).Returns(tasks);

            // Act
            var result = _controller.GetTask();

            // Assert
            Assert.Equal(tasks.Count, result.Count);
            Assert.Equal(tasks.First().Id, result.First().Id);
            Assert.Equal(tasks.First().Name, result.First().Name);
            Assert.Equal(tasks.First().Completed, result.First().Completed);
        }

        [Fact]
        public void AddTask_ShouldReturnTrue_WhenTaskIsAdded()
        {
            // Arrange
            var taskDto = new TaskDTO { Name = "Test Task", Completed = false };
            _mockRepo.Setup(repo => repo.AddTask(It.IsAny<ToDoTask>())).Returns(true);

            // Act
            var result = _controller.AddTask(taskDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteTask_ShouldReturnTrue_WhenTaskIsDeleted()
        {
            // Arrange
            var taskId = 1;
            _mockRepo.Setup(repo => repo.DeleteTask(taskId)).Returns(true);

            // Act
            var result = _controller.DeleteTask(taskId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateTask_ShouldReturnTrue_WhenTaskIsUpdated()
        {
            // Arrange
            var taskDto = new TaskDTO { Id = 1, Name = "Updated Task", Completed = true };
            _mockRepo.Setup(repo => repo.UpdateTask(It.IsAny<ToDoTask>())).Returns(true);

            // Act
            var result = _controller.UpdateTask(taskDto);

            // Assert
            Assert.True(result);
        }
    }
}