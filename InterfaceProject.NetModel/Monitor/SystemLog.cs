using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace InterfaceProject.NetModel.Monitor
{

    [Table("SystemLog")]
    public partial class SystemLog
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Module { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        public int ThreadId { get; set; }

        [Required]
        [StringLength(10)]
        public string Level { get; set; }

        [Required]
        [StringLength(20)]
        public string Version { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
