using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.DataObject
{
    public class ValueObjectFactory<T> : FactoryBase<ValueObjectFactory<T>> where T : IValueObject<T>
    {
        public T Create()
        {
            object o = Activator.CreateInstance(typeof(T));
            return (T)o;
        }
    }
}
