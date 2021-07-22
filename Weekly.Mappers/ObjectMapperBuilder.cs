using System;
using System.Collections.Generic;

namespace Weekly.Mappers
{
    internal class PropertyTransfer
    {
        public System.Reflection.PropertyInfo SourceProp { get; set; }

        public System.Reflection.PropertyInfo TargetProp { get; set; }

        public System.Linq.Expressions.Expression ConvertExpression { get; set; }
    }

    public abstract class Builder
    {
        private bool isSealed;

        protected void AssertNotSealed()
        {
            if (isSealed) throw new InvalidOperationException("Cannot perform this action when the builder has been sealed.");
        }

        public void Seal()
        {
            Validate();
            isSealed = true;
        }

        protected abstract void Validate();

    }

    public class ObjectMapperBuilder : Builder
    {
        internal readonly Type source;

        internal readonly Type target;

        internal readonly Type interfaceType;

        private List<PropertyTransfer> PropertyTransfers = new List<PropertyTransfer>();

        private List<PropertyTransferBuilder> PropertyBuilders = new List<PropertyTransferBuilder>();

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
            Seal();
            built = built ?? Build();
            return built;
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }

}
