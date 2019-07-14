namespace InterfaceProject.NetModel.CoreDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JokeInfo")]
    public partial class JokeInfo
    {
        public Guid Id { get; set; }

        [StringLength(400)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(2048)]
        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        [Required]
        [StringLength(36)]
        public string CreateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [StringLength(36)]
        public string UpdateBy { get; set; }

        [Required]
        [StringLength(20)]
        public string Version { get; set; }

        public bool IsDel { get; set; }
    }
}
