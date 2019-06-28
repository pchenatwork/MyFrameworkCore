using Framework.Core.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Application.ValueObjects.Workflow
{
    #region	Header
    ///	<summary>
    ///	Summary	description	for	Workflow
    ///	</summary>
    #endregion

    [Serializable]
    public class Workflow : AbstractValueObject<Workflow>
    {

        #region	Constants
        #endregion

        #region	Private Members
        // *************************************************************************
        //				 Private Members
        // *************************************************************************
        private string _name = string.Empty;
        private string _description = string.Empty;
        #endregion Private Members

        #region	Constructors
        static Workflow()
        {
        }

        ///	<summary>
        ///	default	constructor	
        ///	</summary>

        public Workflow()
        {
        }
        #endregion Constructors

        #region	Properties

        [XmlAttribute()]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        [XmlAttribute()]
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
        #endregion Properties

        #region	Overide Methods

        protected override void _CopyFrom(Workflow source)
        {
            _name = source.Name;
            _description = source.Description;
        }

        protected override bool _Equals(Workflow that)
        {
            return _id == that.Id && _name.Equals(that.Name) && _description.Equals(that.Description);
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}


