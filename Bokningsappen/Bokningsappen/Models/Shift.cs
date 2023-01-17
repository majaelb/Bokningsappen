using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class Shift
    {
        public Shift()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Time { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
