namespace ProjectProgress.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mark
    {
        [Key]
        [StringLength(50)]
        public string StudentUSN { get; set; }

        public int? Phase1Marks { get; set; }

        public int? Phase2Marks { get; set; }

        public int? Phase3Marks { get; set; }

        public int? TotalMarks { get; set; }

        public virtual Student Student { get; set; }
    }
}
