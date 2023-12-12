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
    }
}
