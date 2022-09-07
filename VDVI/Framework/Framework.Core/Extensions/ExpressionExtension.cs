using System;
using System.Linq.Expressions;

namespace Framework.Core.Extensions
{
    public static class ExpressionExtension
    {
        public static Expression<Func<TEntity, U>> ExpressionConversion<TEntity, SEntity, U>(Expression<Func<SEntity, bool>> expression)
        {

            var resultBody = expression.Body; ;
            //var resultParam = expression.Parameters;

            var param = Expression.Parameter(typeof(TEntity), nameof(TEntity));

            var filter = Expression.Lambda<Func<TEntity, U>>(resultBody, param);

            return filter;
        }

    }
}
