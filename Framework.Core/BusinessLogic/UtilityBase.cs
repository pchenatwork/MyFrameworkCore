using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.BusinessLogic
{
    public class UtilityBase<T> where T: IValueObject
	{
		public static T CreateObject()
		{
			return (T)Activator.CreateInstance(typeof(T));
		}
		public static T Get(string dataSourceName, int id)
		{
			IManager<T> mgr = ManagerFactory2<T>.Instance.GetManager(dataSourceName);
			return (T)mgr.Get(id);
		}
		public static ValueObjectCollection<T> FindByCriteria(string dataSourceName, string finderType, object[] criteria)
		{
			IManager<T> mgr = ManagerFactory2<T>.Instance.GetManager(dataSourceName);
			return (ValueObjectCollection<T>)mgr.FindByCriteria(finderType, criteria);
		}
		public static int Create(string dataSourceName, T v)
		{
			IManager<T> mgr = ManagerFactory2<T>.Instance.GetManager(dataSourceName);
			return mgr.Create(v);
		}
		public static bool Update(string dataSourceName, T v)
		{
			IManager<T> mgr = ManagerFactory2<T>.Instance.GetManager(dataSourceName);
			return mgr.Update(v);
		}
		public static bool Delete(string dataSourceName, int id)
		{
			IManager<T> mgr = ManagerFactory2<T>.Instance.GetManager(dataSourceName);
			return mgr.Delete(id);
		}

	}
}
