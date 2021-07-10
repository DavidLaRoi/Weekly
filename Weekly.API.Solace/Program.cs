﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;


namespace Weekly.API.Solace
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine();
            GenerateFile(url);
        }

        private static void GenerateOpenApi()
        {
            var g = new Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenerator(new Swashbuckle.AspNetCore.SwaggerGen.SwaggerGeneratorOptions
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

            var c = new NSwag.CodeGeneration.CSharp.CSharpClientGenerator(document, settings);
            string s = c.GenerateFile();
            var cc = new TextCopy.Clipboard();
            cc.SetText(s);
        }

      
    }

}