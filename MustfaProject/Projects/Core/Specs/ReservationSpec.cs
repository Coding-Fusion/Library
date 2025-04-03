using Core.Entites;
using LibraryBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Core.Specs
{
    public class ReservationSpec : BaseSpec<Reservation>
    {
        public ReservationSpec(string? search, string? sort) : base(br => string.IsNullOrEmpty(search) || br.UserReservationName.Contains(search))
        {


            if (!string.IsNullOrEmpty(sort))
                if (sort.ToLower().Contains("Date"))
                    AddOrderByAsc(b => b.ReservationDate);

                else AddOrderByAsc(b => b.Id);

        }

        public ReservationSpec(string username) : base(r => r.UserReservationName == username)
        {

        }

    }
}
