using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Framework.Core.DataObject
{
    /// <summary>
    /// Generic abstract base class for enumeration and lookup data. 
    /// PCHEN 201903 Introduced to replace EnumerationBase class, SOMEWHAT TESTED AND WORKED!
    /// </summary>
    [Serializable]
    public class EnumBase<T> where T : EnumBase<T>
    {
        /* ============================================================================
         * PCHEN 201903 Created as a generic replacement to the none-generic "EnumerationBase"
         * Inspired by https://github.com/ardalis/SmartEnum
         * Adapted to VAS standard EnumerationBase interface such that EnumerationBase ( none-generic ) can be phased out
         * Dev notes: IXmlSerializable interface methods not tested yet
         * ============================================================================ */
        #region Private Variables
        private int _id;
        private string _name;
        private string _description;

        private static readonly Lazy<IEnumerable<T>> _list = new Lazy<IEnumerable<T>>(() => _GetList());
        private static IEnumerable<T> _GetList()
        {
            Type type = typeof(T);
            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                object obj = fieldInfo.GetValue(null);
                if (obj is T)
                {
                    yield return (T)obj;
                }
            }
        }
        #endregion

        #region Constructors
        private EnumBase() { }
        protected EnumBase(int id, string name, string description)
        {
            this._id = id;
            this._name = (name??string.Empty).Trim();
            this._description = (description??string.Empty).Trim();
        }
        #endregion

        #region Method Override 

        /// <summary>
        /// This standard override of the GetHashCode function
        /// hashes the Id member.
        /// </summary>
        /// <returns>hash value of the enumeration instance</returns>
        public override int GetHashCode()
        {
            return this._id.GetHashCode();
        }

        /// <summary>
        /// ToString() returns the Description of the enum
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            /* --------------------------------------------------------------------
             * AbstractEnum can not be object, so can't use IValueObject.ToString(), has to be overrided
             * -------------------------------------------------------------------- */
            return this.Name.ToString();
        }

        #endregion

        #region Public GetBy Methods
        /// <summary>
        /// Get Enum by Name.
        /// </summary>
        public static T GetByName(string name)
        {
            for (int i = 0; i < _list.Value.Count(); i++)
            {
                try
                {
                    if (((T)_list.Value.ElementAt(i)).Name.ToLower().Equals(name.ToLower()))
                        return (T)_list.Value.ElementAt(i);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Get Enum by Id.
        /// </summary>
        public static T GetById(int Id)
        {
            for (int i = 0; i < _list.Value.Count(); i++)
            {
                if (((T)_list.Value.ElementAt(i)).Id == Id)
                    return (T)_list.Value.ElementAt(i);
            }
            return null;
        }


        #endregion

        #region Properties
        /// <summary>
        /// Singleton list conaining all enumeration values.
        /// </summary>
        [XmlIgnore]
        public static IReadOnlyCollection<T> List
        {
            get { return _list.Value.ToList().AsReadOnly(); }
        }

        /// <summary>
        /// Name value of the Enum.
        /// </summary>
        [XmlIgnore]
        public string Name => _name;

        /// <summary>
        /// Description value of the Enum
        /// </summary>
        [XmlIgnore]
        public string Description => _description;

        /// <summary>
        /// Override the base Id and make it read only.
        /// </summary>
        [XmlIgnore]
        public int Id => _id;
        #endregion

        #region Conversion Operators

        /// <summary>
        /// determine whether the given enumeration object is equivalent (value-equivalence)
        /// to this enumeration instance.
        /// id==id OR name==name
        /// </summary>
        /// <param name="enumObj">the enumeration object to compare</param>
        /// <returns>true if they are equal</returns>
        public override bool Equals(object obj)
        {
            var o = obj as T;
            if (ReferenceEquals(o, null))
                return false;
            //return o.Id==_id&&o.Name.ToLower().Equals(_name.ToLower())&_description.ToLower().Equals(_description.ToLower());
            return o.Id == _id || o.Name.ToLower().Equals(_name.ToLower());
        }
        /// <summary>
        /// Standard override of the EQUALS operator. The function
        /// will compare (by value) the given enumeration arguments
        /// for equivalence.
        /// </summary>
        /// <param name="a">first enum to compare</param>
        /// <param name="b">second enum to compare</param>
        /// <returns>true if the two are equal in value</returns>
        public static bool operator ==(EnumBase<T> a, EnumBase<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        /// <summary>
        /// Standard override of the NOT EQUALS operator. The function
        /// will compare (by value) the given enumeration arguments
        /// for the inverse of equivalence.
        /// </summary>
        /// <param name="enum1">first enum to compare</param>
        /// <param name="enum2">second enum to compare</param>
        /// <returns>true if the two are NOT EQUAL in value</returns>
        public static bool operator !=(EnumBase<T> enum1, EnumBase<T> enum2)
        {
            return !(enum1 == enum2);
        }

        /// <summary>
        /// Implicit operator to convert Enum to Int
        /// </summary>
        public static implicit operator int(EnumBase<T> value)
        {
            return value.Id;
        }
        #endregion

    }
}
