using Framework.Core.DataObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Application.ValueObjects.Workflow
{
    [Serializable]
    public class WorkflowAction : AbstractValueObject<WorkflowAction>
    {
        #region	Private Members
        // *************************************************************************
        //				 Private Members
        // *************************************************************************
        private int _workflowNodeId;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private int _actionTypeEnum;
        private string _actionType = string.Empty;
        private string _functionName = string.Empty;
        private string _functionOption = string.Empty;
        private int _displayOrder;
        private int _status;
        #endregion Private Members

        #region	Constructors
        static WorkflowAction()
        {
        }

        ///	<summary>
        ///	default	constructor	
        ///	</summary>

        public WorkflowAction()
        {
        }
        #endregion Constructors

        #region	Properties

        [XmlAttribute()]
        public int WorkflowNodeId
        {
            get
            {
                return this._workflowNodeId;
            }
            set
            {
                this._workflowNodeId = value;
            }
        }

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
        public int ActionTypeEnum
        {
            get
            {
                return this._actionTypeEnum;
            }
            set
            {
                this._actionTypeEnum = value;
            }
        }

        [XmlAttribute()]
        public string ActionType
        {
            get
            {
                return this._actionType;
            }
            set
            {
                this._actionType = value;
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
        public int DisplayOrder
        {
            get
            {
                return this._displayOrder;
            }
            set
            {
                this._displayOrder = value;
            }
        }

        [XmlAttribute()]
        public string FunctionName
        {
            get
            {
                return this._functionName;
            }
            set
            {
                this._functionName = value;
            }
        }

        [XmlAttribute()]
        public string FunctionOption
        {
            get
            {
                return this._functionOption;
            }
            set
            {
                this._functionOption = value;
            }
        }

        [XmlAttribute()]
        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        #endregion Properties

        #region	Overide Methods

        protected override void _CopyFrom(WorkflowAction sourceWorkflowAction)
        {
            _workflowNodeId = sourceWorkflowAction._workflowNodeId;
            _name = sourceWorkflowAction._name;
            _actionTypeEnum = sourceWorkflowAction._actionTypeEnum;
            _actionType = sourceWorkflowAction._actionType;
            _description = sourceWorkflowAction._description;
            _displayOrder = sourceWorkflowAction._displayOrder;
            _functionName = sourceWorkflowAction._functionName;
            _functionOption = sourceWorkflowAction._functionOption;
            _status = sourceWorkflowAction._status;
        }

        protected override bool _Equals(WorkflowAction that)
        {
            throw new NotImplementedException();
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }

        #endregion	Overide Methods

    }
}
