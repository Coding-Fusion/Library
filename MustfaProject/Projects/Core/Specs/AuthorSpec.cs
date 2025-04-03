using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Core.Specs
{
    public class AuthorSpec : BaseSpec<Author>
    {
        public AuthorSpec(string? search, string? sort) : base(a => string.IsNullOrEmpty(search) || a.Name.Contains(search))
        {

            if (!string.IsNullOrEmpty(sort))
                if (sort.ToLower().Contains("Name"))
                    AddOrderByAsc(a => a.Name);
                else AddOrderByDesc(a => a.Id);

        }

        public AuthorSpec(string name) : base(a => a.Name == name)
        {

        }
    }
}
