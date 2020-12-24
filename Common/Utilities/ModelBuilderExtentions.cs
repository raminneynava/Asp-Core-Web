using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Utilities
{
    public static class ModelBuilderExtentions
    {
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> Types = assemblies.SelectMany(x => x.GetExportedTypes())
                .Where(x => x.IsClass && !x.IsAbstract && x.IsPublic && typeof(BaseType).IsAssignableFrom(x));
            foreach (Type type in Types)
                modelBuilder.Entity(type);
        }


        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {

            MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(x => x.Name == nameof(ModelBuilder.ApplyConfiguration));
            IEnumerable<Type> Types = assemblies.SelectMany(x => x.GetExportedTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsPublic);
            foreach (Type type in Types)
            {
                foreach (Type iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        MethodInfo applyConcreateMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        applyConcreateMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                    }
                }
            }

        }

    }
}
