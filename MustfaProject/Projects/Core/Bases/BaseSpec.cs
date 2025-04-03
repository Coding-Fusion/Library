using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Talabat.Core.Specification
{
    public class BaseSpec<T> : ISpec<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Critirea { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }



        public BaseSpec()
        {
        }
        public BaseSpec(Expression<Func<T, bool>> criteriaExpression)
        {
            Critirea = criteriaExpression;
        }
        public void AddOrderByAsc(Expression<Func<T, object>> expression)
        {
            OrderBy = expression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> expression)
        {
            OrderByDesc = expression;
        }






    }
}