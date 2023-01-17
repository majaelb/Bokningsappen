using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningsappen.Models
{
    public class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string BirthDate { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public int? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public double? SalaryPerHour { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
