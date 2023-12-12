using EntityFrameWorkWithXunit.Controllers;
using EntityFrameWorkWithXunit.Domain;
using EntityFrameWorkWithXunit.Infrastructure;
using EntityFrameWorkWithXunit.Test.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkWithXunit.Test.System.Controler
{
    public class TestStudentController
    {
        [Fact]
        public async Task GetStudentsAsync_ShouldReturn200Status()
        {
            //Arrange
            var studentService = new Mock<IStudentService>();
            studentService.Setup(_=>_.GetStudentsAsync()).ReturnsAsync(StudentMockData.GetStudents());
            var sut=new StudentController(studentService.Object);

            //Act
            var result= await sut.GetStudentsAsync();

            //Assert
            result.Result.GetType().Should().Be(typeof(OkObjectResult));
            (result.Result as OkObjectResult).StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task GetStudentsAsync_ShouldReturn204Status()
        {
            //Arrange
            var studentService= new Mock<IStudentService>();
            studentService.Setup(_ => _.GetStudentsAsync()).ReturnsAsync(StudentMockData.GetEmptyStudents());
            var sut = new StudentController(studentService.Object);

            //Act
            var result= await sut.GetStudentsAsync();

            //Assert
            result.Result.GetType().Should().Be(typeof(NoContentResult));
            (result.Result as NoContentResult).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task SaveStudentAsync_ShouldCalledSaveStudentAsyncOnce()
        {
            //Arrange
            var studentService = new Mock<IStudentService>();
            var newstudent = StudentMockData.AddStudent;
            var sut = new StudentController(studentService.Object);
            //Act
            var result = await sut.SaveStudentAsync(newstudent);

            //Assert
            studentService.Verify(_ => _.SaveStudentAsync(newstudent), Times.Exactly(1));
        }

        [Fact]
        public async Task GetStudentsByIdAsync_ShouldReturn200Status()
        {
            //Arrange
            int studentIdToRetrieve = 4;
            var studentService = new Mock<IStudentService>();
            studentService.Setup(_ => _.GetStudentByIdAsync(studentIdToRetrieve)).ReturnsAsync(StudentMockData.AddStudent);
            var sut = new StudentController(studentService.Object);

            //Act
            var result = await sut.GetStudentsByIdAsync(4);

            
            // Assert
            // Check that the result is an OkObjectResult
            Assert.IsType<OkObjectResult>(result.Result);

            // If needed, you can further check the content of the OkObjectResult
            var okObjectResult = result.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            // Assuming your service returns a Student object
            var returnedStudent = okObjectResult?.Value as Student;
            Assert.NotNull(returnedStudent);
            Assert.Equal(StudentMockData.AddStudent.StudentId, returnedStudent.StudentId);
        }

        [Fact]
        public async Task UpdateStudent_ShouldReturnNoContent()
        {
            // Arrange
            var mockStudentService = new Mock<IStudentService>();

            // Assuming the service returns true, indicating a successful update
            mockStudentService.Setup(x => x.UpdateStudentAsync(It.IsAny<Student>())).ReturnsAsync(true);

            // Create an instance of the controller with the mocked service
            var studentsController = new StudentController(mockStudentService.Object);

            // Act
            var result = await studentsController.UpdateStudentAsync(StudentMockData.AddStudent);

            // Assert
            // Check that the result is a NoContentResult
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task UpdateStudent_ShouldReturnBadRequest_WhenStudentIsNull()
        {
            // Arrange
            var mockStudentService = new Mock<IStudentService>();

            // Create an instance of the controller with the mocked service
            var studentsController = new StudentController(mockStudentService.Object);

            // Act
            var result = await studentsController.UpdateStudentAsync(null);

            // Assert
            // Check that the result is a BadRequestResult
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task UpdateStudent_ShouldReturnBadRequest_WhenUpdateFails()
        {
            // Arrange
            var mockStudentService=new Mock<IStudentService>();
            mockStudentService.Setup(x => x.UpdateStudentAsync(It.IsAny<Student>())).ReturnsAsync(false);
            var studentController = new StudentController(mockStudentService.Object);
            //Act
            var result = await studentController.UpdateStudentAsync(StudentMockData.AddStudent);
            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
