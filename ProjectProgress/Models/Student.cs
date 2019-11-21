namespace ProjectProgress.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [Key]
        [StringLength(50)]
        public string StudentUSN { get; set; }

        [Required]
        [StringLength(255)]
        public string StudentName { get; set; }

        public string StudentPhoneNumber { get; set; }

        public int? Semester { get; set; }

        public string StudentEmailId { get; set; }

        public int? DepartmentID { get; set; }

        public int? ProjectID { get; set; }

        public int? TeacherID { get; set; }

        public virtual Department Department { get; set; }

        public virtual Mark Mark { get; set; }

        public virtual Project Project { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
