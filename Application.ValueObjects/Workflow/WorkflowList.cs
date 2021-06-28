using AppBase.Core.Interfaces;
using AppBase.Core.ValueObjects;
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
    public class WorkflowList : ValueObjectBase
    {

        #region	Constants
        #endregion

        #region	Private Members
        // *************************************************************************
        //				 Private Members
        // *************************************************************************
        private string _name = string.Empty;
        private string _description = string.Empty;
        private bool _isActive;
        #endregion Private Members

        #region	Constructors
        static WorkflowList()
        {
        }

        ///	<summary>
        ///	default	constructor	
        ///	</summary>
        private WorkflowList()
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

        [XmlAttribute()]
        public Boolean IsActive { get => _isActive; set => _isActive = value; }
        #endregion Properties

        #region	Overide Methods

        protected override void _CopyFrom(IValueObject src)
        {
            WorkflowList source = (WorkflowList)src;
            _name = source.Name;
            _description = source.Description;
        }

        protected override bool _Equals(IValueObject that)
        {
            throw new NotImplementedException();
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}


