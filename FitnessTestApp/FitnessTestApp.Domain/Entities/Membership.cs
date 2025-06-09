using FitnessTestApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTestApp.Domain.Entities
{
    public class Membership : BaseEntity
    {
        public Guid Id { get; set; }
        public MembershipType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
