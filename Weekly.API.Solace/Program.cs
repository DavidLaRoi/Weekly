using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console = System.Console;

namespace Weekly.API.Solace
{
    internal class Program : ConsoleAdapter.Commands
    {
        private static void Main(string[] args)
        {
            Run<Program>();
        }

        private static void GenerateOpenApi()
        {

        }


        private class NameSpaceBuilder
        {
            private readonly List<Type> list = new List<Type>();


            private static string TypeName(Type type)
            {
                return type.Name.Split('`').First();
            }

            public NameSpaceBuilder()
            {

            }

            public NameSpaceBuilder(IEnumerable<Type> types)
            {
                list.AddRange(types);
            }

            public NameSpaceBuilder AddType(Type type, bool addList = false)
            {
                list.Add(type);
                if (addList)
                {
                    list.Add(typeof(List<>).MakeGenericType(type));
                }

                return this;
            }

            public NameSpaceBuilder AddType<T>(bool addList = false)
            {
                return this.AddType(typeof(T), addList); 
            }

            private static string PredictSwaggerName(Type type)
            {
                if (type.IsGenericType)
                {
                    Type t2 = type.GetGenericTypeDefinition();
                    Type[] t3 = type.GetGenericArguments();
                    return PredictSwaggerName(t3[0]) + t2.Name;
                }
                return type.Name;
            }

            private static string QualifyForUsing(Type type)
            {
                string ns = type.Namespace;
                string name = type.Name;
                if (type.IsGenericType)
                {
                    name = name.Split("'").First();
                    return $"{ns}.{name}<{type.GetGenericArguments()[0]}>";
                }
                else
                {
                    return $"{ns}.{name}";
                }
            }

            public string[] ToArray()
            {
                return list.Select((x) => $"{PredictSwaggerName(x)} = {QualifyForUsing(x)}").ToArray();
            }


        }

        private class CSharpClientGeneratorSettings : NSwag.CodeGeneration.CSharp.CSharpClientGeneratorSettings
        {
            public CSharpClientGeneratorSettings Clone() => (CSharpClientGeneratorSettings)MemberwiseClone();
        }

        [ConsoleAdapter.ConsoleVisible]
        public async Task GenerateFile(string fileName)
        {
            NSwag.OpenApiDocument document = await NSwag.OpenApiDocument.FromFileAsync(fileName);

            NameSpaceBuilder builder = new NameSpaceBuilder()
                .AddType<Data.Dtos.Task>(true);

            CSharpClientGeneratorSettings settings = new CSharpClientGeneratorSettings
            {
                GenerateClientInterfaces = false,
                GenerateClientClasses = false,
                GenerateDtoTypes = false,
                GenerateResponseClasses = false,
                AdditionalNamespaceUsages = builder.ToArray()
            };

            var clientSettings = settings.Clone();
            clientSettings.GenerateClientClasses = true;

            var interfaceSettings = settings.Clone();
            interfaceSettings.GenerateClientInterfaces = true;

            NSwag.CodeGeneration.CSharp.CSharpClientGenerator c = new NSwag.CodeGeneration.CSharp.CSharpClientGenerator(document, settings);
            string s = c.GenerateFile();
            TextCopy.Clipboard cc = new TextCopy.Clipboard();
            cc.SetText(s);
        }


    }

}
