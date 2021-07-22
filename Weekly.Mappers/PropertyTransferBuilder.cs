using System;

namespace Weekly.Mappers
{
    public class PropertyTransferBuilder : Builder
    {
        private readonly Type sourcePropType;
        private readonly Type targetPropType;

        public PropertyTransferBuilder(Type sourcePropType, Type targetPropType)
        {
            this.sourcePropType = sourcePropType;
            this.targetPropType = targetPropType;
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }

}
