using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Weekly.Utils
{
    public class Assert
    {
        public static T NotNull<T>(T value, [CallerArgumentExpression("value")] string argumentName = null)
        {
            if (value is null) throw new ArgumentNullException(argumentName);
            return value;
        }

        public static T SmallerThan<T>(T value, T max, [CallerArgumentExpression("value")] string argumentName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(max) < 0)
            {
                return value;
            }
            throw new ArgumentOutOfRangeException($"{argumentName} must be smaller than {max}");
        }

        public static T SmallerThanOrEquals<T>(T value, T max, [CallerArgumentExpression("value")] string argumentName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(max) <= 0)
            {
                return value;
            }
            throw new ArgumentOutOfRangeException($"{argumentName} cannot be bigger than {max}");
        }

        public static T BiggerThan<T>(T value, T max, [CallerArgumentExpression("value")] string argumentName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(max) > 0)
            {
                return value;
            }
            throw new ArgumentOutOfRangeException($"{argumentName} must be bigger than {max}");
        }

        public static T BiggerThanOrEquals<T>(T value, T lim, [CallerArgumentExpression("value")] string argumentName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(lim) >= 0)
            {
                return value;
            }
            throw new ArgumentOutOfRangeException($"{argumentName} cannot be smaller than {lim}");
        }

        public static T NotZero<T>(T value, [CallerArgumentExpression("value")] string argumentName = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(default) == 0) throw new ArgumentException($"{argumentName} cannot be {default(T)}");
            return value;
        }
    }

}
