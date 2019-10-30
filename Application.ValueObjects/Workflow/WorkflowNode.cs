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

        #region	Private Members
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
        private double _timeToSkip;
        private int _isPermissioned;
        private string _nodeValue;
        private int _nodeConditionEnum;
        private string _nodeCondition;
        private string _action1 = string.Empty;
        private string _action2 = string.Empty;
        private string _emailAction = string.Empty;

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

        #region	Properties

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
        public int UserStepId
        {
            get
            {
                return this._userStepId;
            }
            set
            {
                this._userStepId = value;
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
        public int ConditionId
        {
            get
            {
                return this._conditionId;
            }
            set
            {
                this._conditionId = value;
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
        public int Counter
        {
            get
            {
                return this._counter;
            }
            set
            {
                this._counter = value;
            }
        }
        [XmlAttribute()]
        public int IsSkip
        {
            get
            {
                return this._isSkip;
            }
            set
            {
                this._isSkip = value;
            }
        }
        [XmlAttribute()]
        public int IsPermCtrl
        {
            get
            {
                return this._isPermCtrl;
            }
            set
            {
                this._isPermCtrl = value;
            }
        }
        [XmlAttribute()]
        public string Action1
        {
            get
            {
                return this._action1;
            }
            set
            {
                this._action1 = value;
            }
        }
        [XmlAttribute()]
        public string Action2
        {
            get
            {
                return this._action2;
            }
            set
            {
                this._action2 = value;
            }
        }
        [XmlAttribute()]
        public string Action3
        {
            get
            {
                return this._action3;
            }
            set
            {
                this._action3 = value;
            }
        }
        [XmlAttribute()]
        public string Action4
        {
            get
            {
                return this._action4;
            }
            set
            {
                this._action4 = value;
            }
        }
        [XmlAttribute()]
        public string Action5
        {
            get
            {
                return this._action5;
            }
            set
            {
                this._action5 = value;
            }
        }
        [XmlAttribute()]
        public string Action6
        {
            get
            {
                return this._action6;
            }
            set
            {
                this._action6 = value;
            }
        }
        [XmlAttribute()]
        public string EmailAction
        {
            get
            {
                return this._emailAction;
            }
            set
            {
                this._emailAction = value;
            }
        }

        /// <summary>
        /// Property WorkflowActions (WorkflowActionCollection)
        /// </summary>
        [XmlArray("ArrayOfWorkflowAction")]
        public WorkflowActionCollection WorkflowActions
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

        /// <summary>
        /// Property WorkflowConditions (WorkflowConditionCollection)
        /// </summary>
        [XmlArray("ArrayOfWorkflowCondition")]
        public WorkflowConditionCollection WorkflowConditions
        {
            get
            {
                return this._workflowConditions;
            }
            set
            {
                this._workflowConditions = value;
            }
        }

        /// <summary>
        /// Property WorkflowNodeUsers (WorkflowNodeUserCollection)
        /// </summary>
        [XmlArray("ArrayOfWorkflowNodeUser")]
        public WorkflowNodeUserCollection WorkflowNodeUsers
        {
            get
            {
                return this._workflowNodeUsers;
            }
            set
            {
                this._workflowNodeUsers = value;
            }
        }
        #endregion Properties

        #region	Public Methods
        //	*************************************************************************
        //				   Public methods
        //	*************************************************************************
        /// <summary>
        /// Copy all the Member variables from the source object.
        /// Call base.CopyFrom first in the implementation.
        /// </summary>
        /// <param name="source">The source object.</param>
        public override void CopyFrom(IValueObject source)
        {
            WorkflowNode sourceWorkflowNode = (WorkflowNode)source;
            base.CopyFrom(source);
            _workflowId = sourceWorkflowNode._workflowId;
            _userStepId = sourceWorkflowNode._userStepId;
            _name = sourceWorkflowNode._name;
            _type = sourceWorkflowNode._type;
            _conditionId = sourceWorkflowNode._conditionId;
            _nodeFromId = sourceWorkflowNode._nodeFromId;
            _nodeToId = sourceWorkflowNode._nodeToId;
            _description = sourceWorkflowNode._description;
            _timeToSkip = sourceWorkflowNode._timeToSkip;
            _counter = sourceWorkflowNode._counter;
            _isSkip = sourceWorkflowNode._isSkip;
            _isPermCtrl = sourceWorkflowNode._isPermCtrl;
            _action1 = sourceWorkflowNode._action1;
            _action2 = sourceWorkflowNode._action2;
            _action3 = sourceWorkflowNode._action3;
            _action4 = sourceWorkflowNode._action4;
            _action5 = sourceWorkflowNode._action5;
            _action6 = sourceWorkflowNode._action6;
            _emailAction = sourceWorkflowNode._emailAction;
        }

        protected override void _CopyFrom(WorkflowNode source)
        {
            throw new NotImplementedException();
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
