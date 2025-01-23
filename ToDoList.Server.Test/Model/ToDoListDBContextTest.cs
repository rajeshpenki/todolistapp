using System.Collections.Generic;
using Moq;
using ToDoList.Server.Business;
using ToDoList.Server.Model;
using Xunit;

namespace ToDoList.Server.Test.Model
{
    public class ToDoListDBContextTest
    {
        private readonly Mock<IDataStorage<ToDoTask>> _mockDataStorage;
        private readonly ToDoListDBContext _context;

        public ToDoListDBContextTest()
        {
            _mockDataStorage = new Mock<IDataStorage<ToDoTask>>();
            _context = new ToDoListDBContext(_mockDataStorage.Object);
        }

        [Fact]
        public void GetTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<ToDoTask>
            {
                new ToDoTask { Id = 1, Name = "Task 1", Completed = false },
                new ToDoTask { Id = 2, Name = "Task 2", Completed = true }
            };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(tasks);

            // Act
            var result = _context.GetTasks();

            // Assert
            Assert.Equal(tasks, result);
        }

        [Fact]
        public void AddTask_ShouldAddTask()
        {
            // Arrange
            var task = new ToDoTask { Name = "Task 1", Completed = false };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(new List<ToDoTask>());
            _mockDataStorage.Setup(ds => ds.Add(task));

            // Act
            var result = _context.AddTask(task);

            // Assert
            Assert.True(result);
            _mockDataStorage.Verify(ds => ds.Add(task), Times.Once);
        }

        [Fact]
        public void AddTask_ShouldNotAddDuplicateTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            var tasks = new List<ToDoTask> { task };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(tasks);

            // Act
            var result = _context.AddTask(task);

            // Assert
            Assert.False(result);
            _mockDataStorage.Verify(ds => ds.Add(It.IsAny<ToDoTask>()), Times.Never);
        }

        [Fact]
        public void DeleteTask_ShouldDeleteTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            var tasks = new List<ToDoTask> { task };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(tasks);
            _mockDataStorage.Setup(ds => ds.Remove(task));

            // Act
            var result = _context.DeleteTask(task.Id);

            // Assert
            Assert.True(result);
            _mockDataStorage.Verify(ds => ds.Remove(task), Times.Once);
        }

        [Fact]
        public void UpdateTask_ShouldUpdateTask()
        {
            // Arrange
            var task = new ToDoTask { Id = 1, Name = "Task 1", Completed = false };
            var updatedTask = new ToDoTask { Id = 1, Name = "Updated Task", Completed = true };
            var tasks = new List<ToDoTask> { task };
            _mockDataStorage.Setup(ds => ds.GetAll()).Returns(tasks);
            _mockDataStorage.Setup(ds => ds.Update(updatedTask));

            // Act
            var result = _context.UpdateTask(updatedTask);

            // Assert
            Assert.True(result);
            _mockDataStorage.Verify(ds => ds.Update(updatedTask), Times.Once);
        }
    }
}