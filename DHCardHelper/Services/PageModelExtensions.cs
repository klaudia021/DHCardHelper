using DHCardHelper.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace DHCardHelper.Services
{
    public static class PageModelExtensions
    {
        public async static Task<bool> IsForeignKeyValid<T>(this PageModel model, IRepository<T> repository, Expression<Func<T, bool>> filter) where T : class
        {
            var isForeignKeyValid = await repository.AnyAsync(filter);

            if (!isForeignKeyValid)
            {
                string memberName = GetFullExpressionName<T>(filter);
                model.ModelState.AddModelError(memberName, "Invalid data selected.");

                return false;
            }

            return true;
        }

        private static string GetFullExpressionName<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var memberNames = new List<string>();
            var memberExpression = ((BinaryExpression)expression.Body).Right as MemberExpression;

            while (memberExpression != null)
            {
                memberNames.Add(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            memberNames.Reverse();
            return string.Join(".", memberNames);
        }
    }
}
