using System;
using System.Collections.Generic;
using System.Text;

namespace AppBase.Core.Common
{
    /// <summary>
    /// Generic Singlton Factory base class to any Factory Method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FactoryBase<T> where T : class
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);
        public static T Instance => instance.Value;
    }
}
