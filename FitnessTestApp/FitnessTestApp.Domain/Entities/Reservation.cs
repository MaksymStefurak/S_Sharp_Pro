using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Activity { get; set; } = null!;

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public Guid? TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
    }
}
