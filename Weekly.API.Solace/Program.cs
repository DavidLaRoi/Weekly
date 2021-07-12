using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            public NameSpaceBuilder()
            {

            }

            public NameSpaceBuilder(IEnumerable<Type> types)
            {
                list.AddRange(types);
            }

            public NameSpaceBuilder AddType<T>(bool addList = false)
            {
                list.Add((typeof(T)));
                if (addList)
                {
                    list.Add(typeof(List<T>));
                }

                return this;
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

        [ConsoleAdapter.ConsoleVisible]
        public async Task GenerateFile(string url)
        {
            NSwag.OpenApiDocument document = await NSwag.OpenApiDocument.FromFileAsync(url);

            NameSpaceBuilder builder = new NameSpaceBuilder()
                .AddType<Data.Task>(true);

            NSwag.CodeGeneration.CSharp.CSharpClientGeneratorSettings settings = new NSwag.CodeGeneration.CSharp.CSharpClientGeneratorSettings
            {
                GenerateClientInterfaces = true,
                GenerateDtoTypes = false,
                GenerateResponseClasses = false,
                AdditionalNamespaceUsages = builder.ToArray()
            };

            NSwag.CodeGeneration.CSharp.CSharpClientGenerator c = new NSwag.CodeGeneration.CSharp.CSharpClientGenerator(document, settings);
            string s = c.GenerateFile();
            TextCopy.Clipboard cc = new TextCopy.Clipboard();
            cc.SetText(s);
        }


    }

}
