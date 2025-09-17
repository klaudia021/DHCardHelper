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
                return false;

            return true;
        }

        public static void AddErrorToModel<T>(this PageModel model, Expression<Func<T>> expression) where T: struct
        {
            var memberName = GetFullExpressionName(expression);
            model.ModelState.AddModelError(memberName, "Invalid data provided!");
        }

        private static string GetFullExpressionName<T>(Expression<Func<T>> expression) where T : struct
        {
            var memberNames = new List<string>();
            var memberExpression = expression.Body as MemberExpression;

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
