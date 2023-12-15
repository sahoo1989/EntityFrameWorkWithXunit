using EntityFrameWorkWithXunit.Application;
using EntityFrameWorkWithXunit.Domain;
using EntityFrameWorkWithXunit.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkWithXunit.Test.System.Service
{
    public class TestStudentServiceWithMockDBContext
    {
        [Fact]
        public async Task GetStudentByIdAsync_ShouldReturnCorrectStudent()
        {
            // Arrange
            int studentIdToRetrieve = 1;
            var expectedStudent = new Student { StudentId = studentIdToRetrieve, Name = "Subodh", DateOfBirth = Convert.ToDateTime("04-10-1986", CultureInfo.InvariantCulture), Mobile = "9967975258" };

            // Mocking the DbContext and DbSet using Moq
            var mockDbContext = new Mock<StudentDbContext>();
            var mockDbSet = new Mock<DbSet<Student>>();

            // Set up the mock DbSet to return the expected student when queried
            mockDbSet.Setup(x => x.FindAsync(studentIdToRetrieve)).ReturnsAsync(expectedStudent);

            // Set up the DbContext to return the mock DbSet
            mockDbContext.Setup(x => x.students).Returns(mockDbSet.Object);

            // Create an instance of the class containing the GetStudentByIdAsync method
            var studentService = new StudentService(mockDbContext.Object);

            // Act
            var result = await studentService.GetStudentByIdAsync(studentIdToRetrieve);

            // Assert
            Assert.Equal(expectedStudent, result);
        }
    }
}
