using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Data.Domain
{
    public class SproutUser
    {
        public int SproutUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool Active { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
