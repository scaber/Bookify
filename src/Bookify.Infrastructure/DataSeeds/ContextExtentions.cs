using System.Linq.Expressions;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Data.EntityFramework.DataSeeds
{
    public static partial class ContextExtensions
  {
    public static void Upsert<T>(this ApplicationDbContext ctx, IEnumerable<T> entities, Expression<Func<T, T, bool>> predicate = null) where T : Entity 
    {
      var dbset = ctx.Set<T>();
      var list = entities.ToList();
      foreach (var ent in list)
      {
        T h = null;
        if (predicate != null)
        {
          var exp = new ReplaceExpressionVisitor(predicate.Parameters.FirstOrDefault(), Expression.Constant(ent)).Visit(predicate.Body);
          var lambda = Expression.Lambda<Func<T, bool>>(exp, predicate.Parameters.Skip(1));
          h = dbset.IgnoreQueryFilters().FirstOrDefault(lambda);

          //var exp = predicate.Replace(predicate.Parameters.FirstOrDefault(), Expression.Constant(ent));
         }
        else if (ent.Id != Guid.Empty )
        {
          h = dbset.IgnoreQueryFilters().FirstOrDefault(x => x.Id == ent.Id);
        }
        if (h == null)
           
          
          dbset.Add(ent);
        else
        {
          //ent.Id = h.Id;
          //ctx.Entry(h).State = EntityState.Detached;
          //ctx.Entry(ent).State = EntityState.Modified;
        }
      }
    }

    public static void Delete<T>(this ApplicationDbContext ctx, Expression<Func<T, bool>> predicate = null) where T : Entity
    {
      var dbset = ctx.Set<T>();
      IEnumerable<T> h = null;
      if (predicate != null)
      {
        h = dbset.Where(predicate).AsEnumerable();
      }
      if (h != null && h.Count() > 0)
      {
        dbset.RemoveRange(h.ToList());
      }

    }
  }

  public class ReplaceExpressionVisitor : ExpressionVisitor
  {
    private readonly Expression _oldValue;
    private readonly Expression _newValue;

    public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
    {
      _oldValue = oldValue;
      _newValue = newValue;
    }

    public override Expression Visit(Expression node)
    {
      if (node == _oldValue)
        return _newValue;
      return base.Visit(node);
    }
  }
}
