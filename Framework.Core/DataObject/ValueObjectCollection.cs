using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

namespace Framework.Core.DataObject
{
    [Serializable]
    public class ValueObjectCollection<T> : Collection<T> where T : IValueObject
    {
        #region Constructors 
        // *************************************************************************
        // Constructors 
        // *************************************************************************
        /// <summary>
        /// class constructor 
        /// </summary>
        static ValueObjectCollection()
        {
        }

        /// <summary>
        /// default constructor	
        /// </summary>
        public ValueObjectCollection()
        {
        }
        #endregion Constructors

        /// <summary>
        /// Property IsPartial (bool)
        /// </summary>
        [XmlAttribute()]
        public bool IsPartial { get; set; }
    }
}
