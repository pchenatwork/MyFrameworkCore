using Framework.Core.DataObject;
using Framework.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Framework.Core.DataAccess
{
    public abstract class AbstractDAO<T> : IRepository<T> where T : IValueObject<T>
    {
        protected readonly DbContext _dbContext;

        public AbstractDAO(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public abstract int Create(T entity);
        public abstract bool Delete(dynamic Id);
        public abstract IEnumerable<T> FindByCriteria(string finderType, params object[] criteria);
        public abstract T Get(dynamic id);
        public abstract IEnumerable<T> GetAll();
        public abstract bool Update(T entity);

        public virtual object InvokeByMethodName(string methodName, params object[] parameters)
        {
            Type type = this.GetType();
            return type.InvokeMember(methodName, BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                null, this, parameters);
        }

        protected T Deserialize(XmlReader reader)
        {
            object result = XMLUtility.Deserialize<T>(reader);
            return (T)result;
        }
        protected IEnumerable<T> DeserializeCollection(XmlReader reader)
        {
            object result = XMLUtility.Deserialize<ValueObjectCollection<T>>(reader);
            return (IEnumerable<T>)result;
        }
    }
}
