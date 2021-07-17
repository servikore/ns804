using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? DetetedOn { get; set; }
    }
}
