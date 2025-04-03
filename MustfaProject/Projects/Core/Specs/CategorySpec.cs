using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specification;

namespace Core.Specs
{
    public class CategorySpec : BaseSpec<Category>
    {

        public CategorySpec(string? search, string? sort) : base(c => string.IsNullOrEmpty(search) || c.Name.Contains(search))
        {

            if (!string.IsNullOrEmpty(sort))
                if (sort.ToLower().Contains("Name"))
                    AddOrderByAsc(b => b.Name);

                else AddOrderByAsc(b => b.Id);
        }

            public CategorySpec(string name) : base(c => c.Name == name)
        {
            
        }
        
    }
}
