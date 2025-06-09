using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Service.DTOs
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Activity { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public Guid? TrainerId { get; set; }
    }
}
