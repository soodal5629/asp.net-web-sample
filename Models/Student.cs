using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloAspMVC.Models
{
    [Table("Students")]
    public class Student
    {
        // public int ID { get; set; }
        // public string LastName { get; set; }
        // public string FirstMidName { get; set; }
        // public DateTime EnrollmentDate { get; set; }
        [Column("student_id")]
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }

    }
}