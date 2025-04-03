using LibraryBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Core.Specs
{
    public class BorrowingSpec : BaseSpec<Borrowing>
    {
        public BorrowingSpec(string? search , string? sort):base  (b => string.IsNullOrEmpty(search) || b.UserName.Contains(search))
        {

            if (!string.IsNullOrEmpty(sort))
                if (sort.ToLower().Contains("Date"))
                    AddOrderByAsc(b => b.BorrowDate);
                
            else AddOrderByAsc(b => b.ReturnDate);
        }
    }
}
