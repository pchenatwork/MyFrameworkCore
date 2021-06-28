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
        private Lazy<IDataAccessObject<T>> _dao; // = new Lazy<IRepository<T>>(()=> _GetDAO());

        //rivate Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

        #endregion

        protected abstract IDataAccessObject<T> _GetDAO_();

        #region Constructors
        protected ManagerBase(IDbSession dbSession)
        {
            _dbSession = dbSession;
            _dao = new Lazy<IDataAccessObject<T>>(() => _GetDAO_());
            //_dao = _GetDAO();
        }
        //protected Manager() { }
        #endregion

        // protected readonly string DAO_CLASS_NAME = typeof(T).Name.ToLower() + "manager";

        public IDbSession dbSession => this._dbSession;

        //public IRepository<T> dao => this._dao;

        public int Create(T newObject)
        {
            return _dao.Value.Create(_dbSession, newObject);
        }

        public bool Update(T existingObject)
        {
            return _dao.Value.Update(_dbSession, existingObject);
        }
        public bool Delete(int id)
        {
            return _dao.Value.Delete(_dbSession, id);
        }

        public T CreateObject()
        {
            return ValueObjectFactory<T>.Instance.Create();
        }
        public T Get(int id)
        {
            return _dao.Value.Get(_dbSession, id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dao.Value.GetAll(_dbSession);
        }

        public IEnumerable<T> FindByCriteria(string finderType, object[] criteria)
        {
            return _dao.Value.FindByCriteria(_dbSession, finderType, criteria);
        }
    }
}
