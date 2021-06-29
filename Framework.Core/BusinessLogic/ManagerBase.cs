﻿using System;
using System.Collections.Generic;
using System.Text;
using Framework.Core.DataAccess;
using Framework.Core.ValueObjects;

namespace Framework.Core.BusinessLogic
{
    public class ManagerBase<T> : IManager<T> where T: IValueObject
    {
        #region Private Variables
        private IDbSession _dbSession;
        private IDataAccessObject<T> _dao;
        #endregion

        #region Constructors
        protected ManagerBase(IDbSession dbSession, IDataAccessObject<T> dao)
        {
            _dbSession = dbSession;
            _dao = dao;
        }
        //protected ManagerBase() { }
        #endregion

        public IDbSession dbSession => this._dbSession;

        public IDataAccessObject<T> dao => this._dao;

        public int Create(T newObject)
        {
            return _dao.Create(_dbSession, newObject);
        }

        public T CreateObject()
        {
            return ValueObjectFactory<T>.Instance.Create();
        }

        public virtual bool Delete(int id)
        {
            return _dao.Delete(_dbSession, id);
        }

        public IEnumerable<T> FindByCriteria(string finderType, object[] criteria)
        {
            return _dao.FindByCriteria(_dbSession, finderType, criteria);
        }

        public T Get(int id)
        {
            return _dao.Get(_dbSession, id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dao.GetAll(_dbSession);
        }

        public bool Update(T existingObject)
        {
            return _dao.Update(_dbSession, existingObject);
        }
    }
}