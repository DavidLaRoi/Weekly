using System;


namespace Weekly.API.Solace
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string url = Console.ReadLine();
            GenerateFile(url);
        }

        private static void GenerateOpenApi()
        {
            Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator g = new Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator(new Swashbuckle.AspNetCore.SwaggerGen.SwaggerGeneratorOptions
            {

            }, new poop(), new Swashbuckle.AspNetCore.SwaggerGen.SchemaGenerator());
        }

        private static void GenerateFile(string url)
        {


            NSwag.OpenApiDocument document = NSwag.OpenApiDocument.FromUrlAsync(url).Result;
            NSwag.CodeGeneration.CSharp.CSharpClientGeneratorSettings settings = new NSwag.CodeGeneration.CSharp.CSharpClientGeneratorSettings
            {
                GenerateClientInterfaces = true,
                GenerateDtoTypes = false,
                GenerateResponseClasses = false
            };

            NSwag.CodeGeneration.CSharp.CSharpClientGenerator c = new NSwag.CodeGeneration.CSharp.CSharpClientGenerator(document, settings);
            string s = c.GenerateFile();
            TextCopy.Clipboard cc = new TextCopy.Clipboard();
            cc.SetText(s);
        }


    }

}
