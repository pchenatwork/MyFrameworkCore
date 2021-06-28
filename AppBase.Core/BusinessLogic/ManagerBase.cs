using AppBase.Core.Interfaces;
using AppBase.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppBase.Core.BusinessLogic
{
    public abstract class ManagerBase<T> : IManager<T> where T: IValueObject
    {
        #region Private Variables
        private IDbSession _dbSession;
        private IDataAccessObject<T> _dao; // = new Lazy<IDataAccessObject<T>>(()=> _GetDAO_()).Value;

        //rivate Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

        #endregion

        protected abstract IDataAccessObject<T> GetDefaultDAO();

        private IDataAccessObject<T> GetDAO()
        {
            return _dao?? GetDefaultDAO();
        }

        #region Constructors
        protected ManagerBase(IDbSession dbSession)
        {
            _dbSession = dbSession;
            _dao = new Lazy<IDataAccessObject<T>>(() => GetDAO()).Value;
        }
        #endregion

        public IDbSession DbSession => this._dbSession;

        public IDataAccessObject<T> DAO { set => _dao = value; }

        //public IRepository<T> dao => this._dao;

        public int Create(T newObject)
        {
            return _dao.Create(_dbSession, newObject);
        }

        public bool Update(T existingObject)
        {
            return _dao.Update(_dbSession, existingObject);
        }
        public bool Delete(int id)
        {
            return _dao.Delete(_dbSession, id);
        }
        public T Get(int id)
        {
            return _dao.Get(_dbSession, id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dao.GetAll(_dbSession);
        }

        public IEnumerable<T> FindByCriteria(string finderType, object[] criteria)
        {
            return _dao.FindByCriteria(_dbSession, finderType, criteria);
        }
    }
}
