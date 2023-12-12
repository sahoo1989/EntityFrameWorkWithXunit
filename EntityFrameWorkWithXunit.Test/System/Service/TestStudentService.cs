using EntityFrameWorkWithXunit.Application;
using EntityFrameWorkWithXunit.Domain;
using EntityFrameWorkWithXunit.Infrastructure;
using EntityFrameWorkWithXunit.Test.MockData;
using FluentAssertions;
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
    public class TestStudentService : IDisposable
    {
        private readonly StudentDbContext _studentDbContext;
        public TestStudentService()
        {
            var options = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _studentDbContext = new StudentDbContext(options);
            _studentDbContext.Database.EnsureCreated();
        }
        [Fact]
        public async Task GetStudentsAsync_ReturnStudentCollections()
        {
            //Arrange
            _studentDbContext.students.AddRange(StudentMockData.GetStudents());
            await _studentDbContext.SaveChangesAsync();

            var sut = new StudentService(_studentDbContext);
            //Act
            var result = await sut.GetStudentsAsync();
            //Assert
            result.Should().HaveCount(StudentMockData.GetStudents().Count);
        }
        [Fact]
        public async Task SaveStudentAsync_AddStudentCollection()
        {
            //Arrange
            _studentDbContext.students.AddRange(StudentMockData.GetStudents());
            await _studentDbContext.SaveChangesAsync();

            var sut = new StudentService(_studentDbContext);

            //Act
            await sut.SaveStudentAsync(StudentMockData.AddStudent);
            //Assert
            _studentDbContext.students.Count().Should().Be(StudentMockData.GetStudents().Count + 1);
        }
        [Fact]
        public async Task GetStudentByIdAsync_ShouldReturnCorrectStudent()
        {
            //Arrange
            _studentDbContext.students.AddRange(StudentMockData.GetStudents());
           await _studentDbContext.SaveChangesAsync();

            var sut=new StudentService(_studentDbContext);
            //Act
            var result = await sut.GetStudentByIdAsync(1);
            //Assert
            Assert.Equal(1, result.StudentId);
        }
        [Fact]
        public async Task GetStudentByIdAsync_ShouldReturnNoStudentForIDNotmatched()
        {
            //Arrange
            _studentDbContext.students.AddRange(StudentMockData.GetStudents());
            await _studentDbContext.SaveChangesAsync();

            var sut = new StudentService(_studentDbContext);
            //Act
            var result = await sut.GetStudentByIdAsync(10);
            //Assert
            Assert.Null(result);
        }
        public void Dispose()
        {
            _studentDbContext.Database.EnsureDeleted();
            _studentDbContext.Dispose();
        }
    }
}
