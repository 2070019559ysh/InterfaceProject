namespace InterfaceProject.NetModel.CoreDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WeatherCity")]
    public partial class WeatherCity
    {
        [StringLength(20)]
        public string Id { get; set; }

        [Required]
        [StringLength(20)]
        public string ParentId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public int WCityLevel { get; set; }

        [Required]
        [StringLength(20)]
        public string WCityLevelName { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
