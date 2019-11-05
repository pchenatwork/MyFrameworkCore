using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Application.ValueObjects.Workflow
{
    [Serializable]
    public class WorkflowNode : AbstractValueObject<WorkflowNode>
    {
        #region	Constants
        #endregion

        #region	Private valurables
        // *************************************************************************
        //				 Private Members
        // *************************************************************************
        private int _workflowId;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private int _nodeTypeEnum;
        private string _nodeType;
        private int _nodeFromId;
        private int _nodeToId;
        private int _stepId;
        private double _timeToSkip;
        private int _isPermissioned;
        private string _nodeValue;
        private int _nodeConditionEnum;
        private string _nodeCondition;
        private string _action = string.Empty;

        private ValueObjectCollection<WorkflowAction> _workflowActions = null;
        ///private WorkflowConditionCollection _workflowConditions = null;
        ///private WorkflowNodeUserCollection _workflowNodeUsers = null;
        #endregion Private Members

        #region	Constructors
        static WorkflowNode()
        {
        }
        ///	<summary>
        ///	default	constructor	
        ///	</summary>
        public WorkflowNode()
        {
        }
        #endregion Constructors

        #region	Public Properties

        [XmlAttribute()]
        public int WorkflowId
        {
            get
            {
                return this._workflowId;
            }
            set
            {
                this._workflowId = value;
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
        public string NodeType
        {
            get
            {
                return this._nodeType;
            }
            set
            {
                this._nodeType = value;
            }
        }
        [XmlAttribute()]
        public int NodeTypeEnum
        {
            get
            {
                return this._nodeTypeEnum;
            }
            set
            {
                this._nodeTypeEnum = value;
            }
        }
        [XmlAttribute()]
        public int NodeFromId
        {
            get
            {
                return this._nodeFromId;
            }
            set
            {
                this._nodeFromId = value;
            }
        }
        [XmlAttribute()]
        public int NodeToId
        {
            get
            {
                return this._nodeToId;
            }
            set
            {
                this._nodeToId = value;
            }
        }
        [XmlAttribute()]
        public int StepId
        {
            get
            {
                return this._stepId;
            }
            set
            {
                this._stepId = value;
            }
        }
        [XmlAttribute()]
        public string Action
        {
            get
            {
                return this._action;
            }
            set
            {
                this._action = value;
            }
        }
        [XmlAttribute()]
        public double TimeToSkip
        {
            get
            {
                return this._timeToSkip;
            }
            set
            {
                this._timeToSkip = value;
            }
        }
        [XmlAttribute()]
        public string NodeValue
        {
            get
            {
                return this._nodeValue;
            }
            set
            {
                this._nodeValue = value;
            }
        }
        [XmlAttribute()]
        public int NodeConditionEnum
        {
            get
            {
                return this._nodeTypeEnum;
            }
            set
            {
                this._nodeTypeEnum = value;
            }
        }

        /// <summary>
        /// Property WorkflowActions (WorkflowActionCollection)
        /// </summary>
        [XmlArray("ArrayOfWorkflowAction")]
        public ValueObjectCollection<WorkflowAction> WorkflowActions
        {
            get
            {
                return this._workflowActions;
            }
            set
            {
                this._workflowActions = value;
            }
        }
        #endregion Properties

        #region	override Methods
        /// <summary>
        /// Copy all the Member variables from the source object.
        /// Call base.CopyFrom first in the implementation.
        /// </summary>
        /// <param name="source">The source object.</param>
        protected override void _CopyFrom(WorkflowNode source)
        {
            _workflowId = source._workflowId;
            _name = source._name;
            _description = source._description;
            _nodeTypeEnum = source._nodeTypeEnum;
            _nodeType = source._nodeType;
            _nodeFromId = source._nodeFromId;
            _nodeToId = source._nodeToId;
            _stepId = source._stepId;
            _action = source._action;
            _isPermissioned = source._isPermissioned;
            _timeToSkip = source._timeToSkip;
            _nodeConditionEnum = source._nodeConditionEnum;
            _nodeCondition = source._nodeCondition;
            _nodeValue = source._nodeValue;
        }

        protected override bool _Equals(WorkflowNode that)
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
