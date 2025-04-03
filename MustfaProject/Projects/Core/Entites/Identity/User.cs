using LibraryBackend.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.Identity
{
    public class User : IdentityUser
    {
        public string? DisplayName { get; set; }
        public ICollection<Notification>? notifications { get; set; } = new List<Notification>();

        public ICollection<Borrowing>? borrowing { get; set; } = new List<Borrowing>();

        public ICollection<Reservation>? reservations { get; set; } = new List<Reservation>();

    }
}
