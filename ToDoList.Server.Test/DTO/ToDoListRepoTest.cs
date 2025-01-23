using System.ComponentModel.DataAnnotations;
using ToDoList.Server.DTO;
using Xunit;

namespace ToDoList.Tests.DTO
{
    public class TaskDTOTest
    {
        [Fact]
        public void TaskDTO_NameIsRequired_ShouldFailValidationIfMissing()
        {

            // Arrange
            var task = new TaskDTO
            {
                Id = 1,
                Completed = false ,
                Name = null                
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(task, null, null);
            var isValid = Validator.TryValidateObject(task, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Name"));
        }

        [Fact]
        public void TaskDTO_ValidData_ShouldPassValidation()
        {
            // Arrange
            var task = new TaskDTO
            {
                Id = 1,
                Name = "Test Task",
                Completed = false
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(task, null, null);
            var isValid = Validator.TryValidateObject(task, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid);
        }
    }
}

