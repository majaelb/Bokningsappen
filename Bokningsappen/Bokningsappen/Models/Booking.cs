using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public int UnitId { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public int Day { get; set; }


        public virtual Shift Shift { get; set; } = null!;
        public virtual Unit Unit { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
