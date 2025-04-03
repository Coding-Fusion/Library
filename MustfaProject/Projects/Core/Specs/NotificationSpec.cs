using LibraryBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Core.Specs
{
    public class NotificationSpec : BaseSpec<Notification>
    {
        public NotificationSpec(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
                if (sort.ToLower().Contains("Message"))
                    AddOrderByAsc(b => b.Message);

                else AddOrderByDesc(b => b.Message);


        }
    }
}
