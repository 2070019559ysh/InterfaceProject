namespace InterfaceProject.NetModel.CoreDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysConfigInfo")]
    public partial class SysConfigInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        [Required]
        [StringLength(200)]
        public string Value { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
