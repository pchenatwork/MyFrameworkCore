using AppBase.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AppBase.Core.ValueObjects
{
    /// <summary>
    /// Inspired by
    /// http://grabbagoft.blogspot.com/2007/06/generic-value-object-equality.html
    /// https://enterprisecraftsmanship.com/2015/01/03/value-objects-explained/
    /// </summary>
    [Serializable]
    public abstract class ValueObjectBase : IValueObject
    {
        #region	Private Members
        protected int _id;
        protected string _createdBy = string.Empty;
        protected DateTime _createdDate = new DateTime(1900, 1, 1);
        protected string _updatedBy = string.Empty;
        protected DateTime _updatedDate = new DateTime(1900, 1, 1);
        protected string _extra = string.Empty;
        #endregion

        #region constructor
        ///	<summary>
        ///	Private constructor to force using ValueObjectFactory<T>
        ///	</summary>
        protected ValueObjectBase() 
        {
        }
        #endregion constructor
        #region Properties
        [XmlAttribute()]
        public int Id { get => this._id; set => _id = value; }
        [XmlAttribute()]
        public string CreatedBy { get => _createdBy; set => _createdBy = value; }
        [XmlAttribute()]
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        [XmlAttribute()]
        public string UpdatedBy { get => _updatedBy; set => _updatedBy = value; }
        [XmlAttribute()]
        public DateTime UpdatedDate { get => _updatedDate; set => _updatedDate = value; }
        [XmlAttribute()]
        public string Extra
        {
            get => _extra; set => _extra = value;
        }
        #endregion Properties


        #region Operations
        public void CopyFrom(IValueObject source)
        {
            _id = source.Id;
            _createdBy = source.CreatedBy;
            _createdDate = source.CreatedDate;
            _updatedBy = source.UpdatedBy;
            _updatedDate = source.UpdatedDate;
            _extra = source.Extra;
            _CopyFrom_(source);
        }
        protected abstract void _CopyFrom_(IValueObject source);

        public override bool Equals(object obj)
        {
            var o = obj as IValueObject;
            if (ReferenceEquals(o, null))
                return false;
            return _Equals_(o);
        }
        protected abstract bool _Equals_(IValueObject that);

        public static bool operator ==(ValueObjectBase a, ValueObjectBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }
        public static bool operator !=(ValueObjectBase a, ValueObjectBase b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            return _GetHashCode();
        }
        protected abstract int _GetHashCode();
        #endregion Operations
    }
}
