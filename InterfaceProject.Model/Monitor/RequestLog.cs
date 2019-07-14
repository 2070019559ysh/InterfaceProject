using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterfaceProject.Model.Monitor
{
    [Table("RequestLog")]
    public partial class RequestLog
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string RequestUrl { get; set; }

        [Required]
        [StringLength(10)]
        public string RequestMethod { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string RequestMsg { get; set; }

        [Column(TypeName = "ntext")]
        public string ResponseMsg { get; set; }

        [Column(TypeName = "ntext")]
        public string ExceptionMsg { get; set; }

        public Guid? ReferenceId { get; set; }

        [StringLength(200)]
        public string ReferenceTable { get; set; }

        [Required]
        [StringLength(10)]
        public string Level { get; set; }

        [Required]
        [StringLength(20)]
        public string Version { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
