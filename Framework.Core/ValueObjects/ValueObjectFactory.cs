using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Core.ValueObjects
{
    public class ValueObjectFactory<T> : FactoryBase<ValueObjectFactory<T>> where T : IValueObject
    {
        public T Create()
        {
            object o = Activator.CreateInstance(typeof(T),
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null,
                    null, null);
            return (T)o;
        }
    }
}
