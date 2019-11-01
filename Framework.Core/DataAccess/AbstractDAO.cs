using Framework.Core.ValueObjects;
using Framework.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Framework.Core.DataAccess
{
    public abstract class AbstractDAO<T> : IRepository<T> where T : IValueObject<T>
    {
        #region constructor
        public AbstractDAO()
        {
        }
        static AbstractDAO()
        {

        }
        #endregion constructor

        public virtual int Create(IDbSession dbSession, T entity)
        {
            throw new NotImplementedException();
        }
        public virtual bool Delete(IDbSession dbSession, dynamic Id)
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> FindByCriteria(IDbSession dbSession, string finderType, params object[] criteria)
        {
            throw new NotImplementedException();
        }
        public virtual T Get(IDbSession dbSession, dynamic id)
        {
            throw new NotImplementedException();
        }
        public virtual IEnumerable<T> GetAll(IDbSession dbSession)
        {
            throw new NotImplementedException();
        }
        public virtual bool Update(IDbSession dbSession, T entity)
        {
            throw new NotImplementedException();
        }
        public virtual object InvokeByMethodName(IDbSession dbSession, string methodName, params object[] parameters)
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
