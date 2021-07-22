using System;
using System.Linq.Expressions;

namespace Weekly.Mappers
{
    public class P<s,p> : PropertyTransferBuilder
    {


        private P() : base(typeof(s), typeof(p))
        {

        }

        public P<s, p> AddConversionExpression(Expression<Func<s,p>> expression)
        {

            return this;
        }

        public P<s, p> AddInjectedConversion()
        {
            return this;
        }
    }

}
