using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Core.Specs
{
    public class BookSpec : BaseSpec<Book>
    {
        public BookSpec(string? sort, string? search) : base(b => string.IsNullOrEmpty(search) || b.Title.Contains(search))
        {
            if (!string.IsNullOrEmpty(sort))
                if (sort.ToLower().Contains("Title"))
                    AddOrderByAsc(b => b.Title);

                else AddOrderByAsc(b => b.Id);

        }

        public BookSpec(string name) : base(b => b.Title == name)
        {

        }
    }
}
