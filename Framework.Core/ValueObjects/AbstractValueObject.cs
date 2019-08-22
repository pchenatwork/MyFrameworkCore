using Framework.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Framework.Core.ValueObjects
{
    /// <summary>
    /// Inspired by
    /// http://grabbagoft.blogspot.com/2007/06/generic-value-object-equality.html
    /// https://enterprisecraftsmanship.com/2015/01/03/value-objects-explained/
    /// </summary>
    [Serializable]
    public abstract class AbstractValueObject<T> : IValueObject<T> where T: AbstractValueObject<T>
    {

        #region	Private Members
        protected int _id;
        protected string _createUser = string.Empty;
        protected DateTime _createDate = new DateTime(1900, 1, 1);
        protected string _changeUser = string.Empty;
        protected DateTime _changeDate = new DateTime(1900, 1, 1);
        protected string _extra = string.Empty;
        #endregion

        #region constructor
        ///	<summary>
        ///	default	constructor	
        ///	</summary>
        public AbstractValueObject()
        {
        }
        #endregion constructor

        #region Properties
        [XmlAttribute()]
        public int Id { get => this._id; set => _id = value; }
        [XmlAttribute()]
        public string CreateUser { get => _createUser; set => _createUser = value; }
        [XmlAttribute()]
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        [XmlAttribute()]
        public string ChangeUser { get => _changeUser; set => _changeUser = value; }
        [XmlAttribute()]
        public DateTime ChangeDate { get => _changeDate; set => _changeDate = value; }
        [XmlAttribute()]
        public string Extra { get => _extra; set => _extra = value; }
        #endregion properties

        #region Methods
        public static bool operator ==(AbstractValueObject<T> a, AbstractValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }
        public static bool operator !=(AbstractValueObject<T> a, AbstractValueObject<T> b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            var _o = obj as T;
            if (ReferenceEquals(_o, null))
                return false;
            return _Equals(_o);
        }
        protected abstract bool _Equals(T that);

        public override int GetHashCode()
        {
            return _GetHashCode();
        }
        protected abstract int _GetHashCode();

        /// <summary>
        /// Copy all the Member variables from the source object.
        /// Call base.CopyFrom first in the implementation.
        /// </summary>
        /// <param name="source">The source object.</param>
        public virtual void CopyFrom(T source)
        {
            _id = source.Id;
            _createUser = source.CreateUser;
            _createDate = source.CreateDate;
            _changeUser = source.ChangeUser;
            _changeDate = source.ChangeDate;
            _extra = source.Extra;
            _CopyFrom(source);
        }
        protected abstract void _CopyFrom(T source);
        public override String ToString()
        {
            return XMLUtility.ToXml(this);
        }
        public string ToJSON()
        {
            return " Newtonsoft.Json.JsonConvert.SerializeObject(this) ";
        }
        #endregion methods

    }

}
