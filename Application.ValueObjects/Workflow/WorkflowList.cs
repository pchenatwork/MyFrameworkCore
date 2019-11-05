using Framework.Core.ValueObjects;
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
    public class WorkflowList : AbstractValueObject<WorkflowList>
    {

        #region	Constants
        #endregion

        #region	Private Members
        // *************************************************************************
        //				 Private Members
        // *************************************************************************
        private string _name = string.Empty;
        private string _description = string.Empty;
        private bool _status;
        #endregion Private Members

        #region	Constructors
        static WorkflowList()
        {
        }

        ///	<summary>
        ///	default	constructor	
        ///	</summary>

        public WorkflowList()
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
        public Boolean Status { get => _status; set => _status = value; }
        #endregion Properties

        #region	Overide Methods

        protected override void _CopyFrom(WorkflowList source)
        {
            _name = source.Name;
            _description = source.Description;
        }

        protected override bool _Equals(WorkflowList that)
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


