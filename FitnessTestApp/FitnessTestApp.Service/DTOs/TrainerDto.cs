using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Service.DTOs
{
    public class TrainerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Specialty { get; set; } = null!;
    }
}
