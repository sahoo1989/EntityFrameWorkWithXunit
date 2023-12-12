using EntityFrameWorkWithXunit.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWorkWithXunit.Test.MockData
{
    public class StudentMockData
    {
        public static List<Student> GetStudents()
        {
            return new List<Student>() {
            new Student{ StudentId=1,Name="Subodh",DateOfBirth=Convert.ToDateTime("12-06-1989",CultureInfo.InvariantCulture),Mobile="9819613634" },
            new Student{ StudentId=2,Name="Sandhya",DateOfBirth=Convert.ToDateTime("12-06-1989",CultureInfo.InvariantCulture),Mobile="9967975258" },
            new Student{ StudentId=3,Name="Sanvi",DateOfBirth=Convert.ToDateTime("04-10-2019",CultureInfo.InvariantCulture),Mobile="9967975258" }
            };
        }

        public static List<Student> GetEmptyStudents() => new List<Student>() { };

        public static Student AddStudent => new Student { StudentId = 4, Name = "Mukesh", DateOfBirth = Convert.ToDateTime("04-10-1986", CultureInfo.InvariantCulture), Mobile = "9967975258" };
    }
}
