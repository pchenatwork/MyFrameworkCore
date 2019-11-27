using Framework.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Framework.Core.ValueObjects
{
    /// <summary>
    /// Inspired by
    /// http://grabbagoft.blogspot.com/2007/06/generic-value-object-equality.html
    /// https://enterprisecraftsmanship.com/2015/01/03/value-objects-explained/
    /// </summary>
    [Serializable]
    public abstract class AbstractValueObject<T> : IValueObject where T: AbstractValueObject<T>
    {

        #region	Private Members
        protected int _id;
        protected string _createUser = string.Empty;
        protected DateTime _createDate = new DateTime(1900, 1, 1);
        protected string _changeUser = string.Empty;
        protected DateTime _changeDate = new DateTime(1900, 1, 1);
        protected string _extra = string.Empty;

        protected int _totalRecordNumber;
        protected string _result = string.Empty;
        #endregion

        #region constructor
        ///	<summary>
        ///	Private constructor to force using ValueObjectFactory<T>
        ///	</summary>
        protected AbstractValueObject() // Private constructor to force using ValueObjectFactory<T> 
        {
        }
        #endregion constructor

        #region Properties
        [XmlAttribute()]
        public int Id { get => this._id; set => _id = value; }
        [XmlAttribute()]
        public string CreateBy { get => _createUser; set => _createUser = value; }
        [XmlAttribute()]
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        [XmlAttribute()]
        public string LastUpdateBy { get => _changeUser; set => _changeUser = value; }
        [XmlAttribute()]
        public DateTime LastUpdateDate { get => _changeDate; set => _changeDate = value; }
        [XmlAttribute()]
        public string Extra { get => _extra; set => _extra = value; }


        /// <summary>
        /// Property TotalRecordNumber (int)
        /// </summary>
        [XmlAttribute()]
        public virtual int TotalRecordNumber
        {
            get
            {
                return this._totalRecordNumber;
            }
            set
            {
                this._totalRecordNumber = value;
            }
        }

        /// <summary>
        /// Property Result (string)
        /// </summary>
        [XmlAttribute()]
        public virtual string Result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
            }
        }

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
        /// 
        public void CopyFrom(IValueObject source)
        {
            _id = source.Id;
            _createUser = source.CreateBy;
            _createDate = source.CreateDate;
            _changeUser = source.LastUpdateBy;
            _changeDate = source.LastUpdateDate;
            _extra = source.Extra;

            _CopyFrom((T)source);

            _totalRecordNumber = source.TotalRecordNumber;
            _result = source.Result;

        }
        protected abstract void _CopyFrom(T source);
        public override String ToString()
        {
            return XMLUtility.ToXml(this);
        }
        
        #endregion methods

    }

}
