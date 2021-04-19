﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StarChart.Models
{
    public class CelestialObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? OrbitedOjbectId { get; set; }
        [NotMapped]
        public List<CelestialObject> Satgellites { get; set; }
        public TimeSpan OrbitalPeriod { get; set; }

    }
}
