using System;
using System.Linq.Expressions;

namespace Weekly.Commands
{

    public class ObjectMapperBuilder<TSource, TTarget> : ObjectMapperBuilder
    {
        internal ObjectMapperBuilder() : base(typeof(TSource), typeof(TTarget)) { }

        public new ObjectMapperBuilder<TSource, TTarget> AddAutos()
        {
            base.AddAutos();
            return this;
        }

        public ObjectMapperBuilder<TSource, TTarget> AddExpression<TValue>(
            Expression<Func<TSource, TValue>> sourceProp, 
            Expression<Action<TTarget, TValue>> targetProp)
        {
            //TODO implement
            return this;
        }

        public ObjectMapperBuilder<TSource,TTarget> AddExpression<TSourceValue,TTargetValue>(
                        Expression<Func<TSource, TSourceValue>> sourceProp,
            Expression<Action<TTarget, TTargetValue>> targetProp,
            Expression<Func<TSourceValue,TTargetValue>> convertExpression)
        {
            //TODO implement
            return this;
        }
    }

}
