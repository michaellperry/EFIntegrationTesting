using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Globalmantics.DAL
{
    public static class DbSetExtensions
    {
        public static void AddOrUpdate<TEntity, TIdentifier>(
            this DbSet<TEntity> set,
            Expression<Func<TEntity, TIdentifier>> identifierExpression,
            IEqualityComparer<TEntity> entityComparer,
            params TEntity[] entities)
            where TEntity : class
        {
            var identifierFunction = identifierExpression.Compile();
            var currentValues = set.OrderBy(identifierExpression).ToArray();
            var desiredValues = entities.OrderBy(identifierFunction).ToArray();

            if (!currentValues.Any())
            {
                set.AddRange(desiredValues);
            }
            else if (!currentValues.SequenceEqual(desiredValues, entityComparer))
            {
                var pairs = desiredValues.GroupJoin(currentValues,
                    identifierFunction, identifierFunction,
                    (desired, group) => new { desired, current = group.SingleOrDefault() });
                foreach (var pair in pairs)
                {
                    if (pair.current != null)
                    {
                        foreach (var property in typeof(TEntity).GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            property.SetValue(pair.current, property.GetValue(pair.desired));
                        }
                    }
                    else
                    {
                        set.Add(pair.desired);
                    }
                }
            }
        }
    }
}
