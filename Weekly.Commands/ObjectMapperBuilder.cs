using System;
using System.Collections.Generic;

namespace Weekly.Commands
{
    internal class PropertyTransfer
    {
        public System.Reflection.PropertyInfo SourceProp { get; init; }

        public System.Reflection.PropertyInfo TargetProp { get; init; }

        public System.Linq.Expressions.Expression ConvertExpression { get; init; }
    }

    public class ObjectMapperBuilder
    {
        internal readonly Type source;

        internal readonly Type target;

        internal readonly Type interfaceType;

        private List<PropertyTransfer> PropertyTransfers = new List<PropertyTransfer>();

        public ObjectMapperBuilder(Type source, Type target)
        {
            this.source = source;
            this.target = target;
            interfaceType = AutoMappersBuilder.GenericMapperType(source, target);
        }

        public ObjectMapperBuilder AddAutos()
        {
            foreach(var sourceProp in source.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if(sourceProp.CanRead && sourceProp.CanWrite)
                {
                    var targetProp = target.GetProperty(sourceProp.Name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    if(targetProp.CanRead && targetProp.CanWrite)
                    {
                        PropertyTransfers.Add(new PropertyTransfer
                        {
                            SourceProp = sourceProp,
                            TargetProp = targetProp
                        });
                    }
                }
            }
            return this;
        }


        private Type built;

        private Type Build()
        {
            throw new NotImplementedException();
        }

        internal Type GetBuilt()
        {
            built ??= Build();
            return built;
        }

        private bool isSealed;

        private void AssertNotSealed()
        {

        }

        public void Seal()
        {

        }
    }

}
