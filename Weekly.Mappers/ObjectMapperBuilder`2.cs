using System;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Weekly.Mappers
{

    public class ObjectMapperBuilder<TSource, TTarget> : ObjectMapperBuilder
    {
        internal ObjectMapperBuilder() : base(typeof(TSource), typeof(TTarget)) { }

        public new ObjectMapperBuilder<TSource, TTarget> AddAutos()
        {
            base.AddAutos();
            return this;
        }

        public ObjectMapperBuilder<TSource, TTarget> AddProp<TValue>(
            Expression<Func<TSource, TValue>> sourceProp, 
            Expression<Action<TTarget, TValue>> targetProp)
        {

            return this;
        }

        public ObjectMapperBuilder<TSource,TTarget> AddProp<TSourceValue,TTargetValue>(
                        Expression<Func<TSource, TSourceValue>> sourceProp,
            Expression<Action<TTarget, TTargetValue>> targetProp,
            Expression<Func<TSourceValue,TTargetValue>> convertExpression)
        {
            //TODO implement
            return this;
        }
    }

}
