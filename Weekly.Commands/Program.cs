using ConsoleAdapter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Weekly.Utils;

namespace Weekly.Commands
{
    public partial class Program : ConsoleAdapter.Commands
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Call<Program>(args);
            }
            else
            {
                Run<Program>();
            }
        }

        [ConsoleVisible]
        public void T()
        {
            decimal D = 12345678.12345678M;
            var cultures = System.Globalization.CultureInfo.GetCultures((System.Globalization.CultureTypes)127);
            foreach(var culture in cultures)
            {
                string s = D.ToString(culture);
                if (s.Any(char.IsWhiteSpace))
                {
                    Console.WriteLine(culture.ToString());
                }
            }

        }

        public string A { get; set; } = "not poo";
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }

        [ConsoleVisible]
        public void Visit()
        {
            string argument = "poo";
            Expression<Func<Program, string, bool>> parammedPredicate = null;


            Expression<Func<Program, bool>> exp = x => x.A.ToLower().Contains(argument)
            || x.B.ToLower().Contains(argument)
            || x.C.ToLower().Contains(argument)
            || x.D.ToLower().Contains(argument);

            var tolower = typeof(string).GetMethod(nameof(string.ToLower), new Type[] { });
            var contains = typeof(string).GetMethod(nameof(string.Contains), new Type[] { typeof(string) });


           
            MethodCallExpression containsExpression(ParameterExpression parameter, ConstantExpression mustContain, PropertyInfo propertyInfo)
            {
                var propget = Expression.Property(parameter, propertyInfo);
                var lwr = Expression.Call(propget, tolower);
                var cnts = Expression.Call(lwr, contains, mustContain);
                return cnts;
            }

            Expression ors(params Expression[] expressions)
            {
                Expression binary = expressions.First();
                foreach(var expression in expressions.Skip(1))
                {
                    binary = Expression.MakeBinary(ExpressionType.OrElse, expression, binary);
                }
                return binary;
            }
            DbSet<Program> bset;
            var c = Expression.Constant(argument);
            var p = Expression.Parameter(typeof(Program));

            Type type = GetType();
            var propNames = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.PropertyType == typeof(string) && x.CanRead);
            var containsExpressions = propNames.Select(x => containsExpression(p, c, x)).ToArray();
            var exp123 = exp.Update(ors(containsExpressions), new ParameterExpression[] { p });

            Console.WriteLine(exp.ToString());
            Console.WriteLine(exp123.ToString());
        }

        private readonly IAsyncLock lck = new AsyncLock();

        [ConsoleVisible]
        public async Task TestAsyncAwaitable()
        {
            async Task Stuff(string value)
            {
                using (await lck.LockAsync())
                {
                    Console.WriteLine($"Enter:{value}:{DateTime.Now}");
                    await Task.Delay(2000);
                    Console.WriteLine($"Exit:{value}:{DateTime.Now}");
                }
            }
            await Task.WhenAll(Stuff("uno"), Stuff("dos"), Stuff("tre"));
        }

        public void CreateOpenApi()
        {

        }

        [ConsoleVisible]
        public void RunNSwag(string swaggerFile, string targetDir)
        {

        }

        [ConsoleVisible]
        public async System.Threading.Tasks.Task Test(string message, int timeout)
        {
            Console.WriteLine(message);
            await System.Threading.Tasks.Task.Delay(timeout);
        }

        private static void BuildMapper<Tx, Ty>(Tx tx, Ty ty)
        {
            AppDomain myDomain = AppDomain.CurrentDomain;
            AssemblyName asmName = new AssemblyName()
            {
                Name = "DynamicMappers"
            };
            AssemblyBuilder builder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder moduleBuilder = builder.DefineDynamicModule("DynamicMapers");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("Mappers", TypeAttributes.Public);

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "MapXToY_123",
                MethodAttributes.Static | MethodAttributes.Public,
                typeof(void),
                new Type[] { typeof(Tx), typeof(Ty) });

            ParameterBuilder paramX = methodBuilder.DefineParameter(1, ParameterAttributes.None, "x");
            ParameterBuilder paramY = methodBuilder.DefineParameter(2, ParameterAttributes.None, "y");

            ILGenerator ILGenerator = methodBuilder.GetILGenerator();

            ILGenerator.Emit(OpCodes.Nop);

            ILGenerator.Emit(OpCodes.Ldarg_0);
            ILGenerator.Emit(OpCodes.Ldarg_1);
            ILGenerator.Emit(OpCodes.Callvirt, typeof(Tx).GetProperty("Name").GetMethod);
            ILGenerator.Emit(OpCodes.Callvirt, typeof(Ty).GetProperty("Name").SetMethod);

            ILGenerator.Emit(OpCodes.Nop);

            ILGenerator.Emit(OpCodes.Ldarg_0);
            ILGenerator.Emit(OpCodes.Ldarg_1);
            ILGenerator.Emit(OpCodes.Callvirt, typeof(Tx).GetProperty("Value").GetMethod);
            ILGenerator.Emit(OpCodes.Callvirt, typeof(Ty).GetProperty("Value").SetMethod);

            ILGenerator.Emit(OpCodes.Nop);
            ILGenerator.Emit(OpCodes.Ret);

            Type myType = typeBuilder.CreateType();
            myType.InvokeMember("MapXToY_123", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null
                , null, new object[] { tx, ty });


        }

        private Type ConstructAutoMapper<T1, T2>(bool includeNoop = true)
        {
            return ConstructAutoMapper(typeof(T1), typeof(T2), includeNoop);
        }

        private static IEnumerable<(PropertyInfo sourceProp, PropertyInfo targetProp)> GetMatching(Type source, Type target)
        {
            foreach (PropertyInfo sourceProp in source.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                PropertyInfo targetProp = target.GetProperty(sourceProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (targetProp is PropertyInfo &&
                    targetProp.CanRead &&
                    targetProp.CanWrite &&
                    targetProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                {
                    yield return (sourceProp, targetProp);
                }
            }
        }


        private Type ConstructAutoMapper(Type source, Type target, bool emitNoop = true)
        {
            AppDomain myDomain = AppDomain.CurrentDomain;
            AssemblyName asmName = new AssemblyName()
            {
                Name = "DynamicMappers"
            };
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicMapers");

            Type mapperInterface = typeof(IMapper<,>);
            mapperInterface = mapperInterface.MakeGenericType(source, target);

            TypeBuilder typeBuilder = moduleBuilder.DefineType("Mappers", TypeAttributes.Public, typeof(object), new Type[] { mapperInterface });

            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                nameof(IMapper<int, int>.Map),
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.NewSlot,
                typeof(void),
                new Type[] { source, target });



            ILGenerator ilbuilder = methodBuilder.GetILGenerator();

            foreach ((PropertyInfo sourceProp, PropertyInfo targetProp) in GetMatching(source, target))
            {
                if (emitNoop)
                {
                    ilbuilder.Emit(OpCodes.Nop);
                }

                ilbuilder.Emit(OpCodes.Ldarg_2);
                ilbuilder.Emit(OpCodes.Ldarg_1);
                ilbuilder.Emit(OpCodes.Callvirt, sourceProp.GetMethod);
                ilbuilder.Emit(OpCodes.Callvirt, sourceProp.SetMethod);
            }

            if (emitNoop)
            {
                ilbuilder.Emit(OpCodes.Nop);
            }

            ilbuilder.Emit(OpCodes.Ret);

            return typeBuilder.CreateType();
        }

        [ConsoleVisible]
        public void Test2()
        {
            ObjA a = new ObjA
            {
                Name = "poop",
                Value = 12

            };
            ObjB b = new ObjB();
            //BuildMapper(b, a);

            //XToY(new ObjA(), new ObjB());

            Type autoMapper = ConstructAutoMapper<ObjA, ObjB>(false);
            IMapper<ObjA, ObjB> mapper = (IMapper<ObjA, ObjB>)Activator.CreateInstance(autoMapper);
            mapper.Map(a, b);
        }

        public class SymMapper
            : IMapper<ObjA, ObjB>
        {
            public void Map(ObjA source, ObjB target)
            {
                target.Name = source.Name;
                target.Value = source.Value;
            }

            public void Map2(ObjA source, ObjB target)
            {
                target.Name = source.Name;
                target.Value = source.Value;
            }
        }

        public class SymMapper2
            : IMapper<ObjA, ObjB>
        {
            void IMapper<ObjA, ObjB>.Map(ObjA source, ObjB target)
            {
                target.Name = source.Name;
                target.Value = source.Value;
            }
        }

        public static void XToY(ObjA objA, ObjB objB)
        {
            objA.Name = objB.Name;
            objA.Value = objB.Value;
        }

        public class ObjA
        {
            public string Name { get; set; }

            public int Value { get; set; }
        }

        public class ObjB
        {
            public string Name { get; set; }

            public int Value { get; set; }
        }
    }
}
