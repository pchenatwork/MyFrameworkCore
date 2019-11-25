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
        #region	Constructors
        ///	<summary>
        ///	default prvate constructor	
        ///	</summary>
        private WorkflowNode()
        {
        }
        #endregion Constructors

        #region	Public Properties

        [XmlAttribute()]
        public int WorkflowId { get; set; }
        [XmlAttribute()]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [XmlAttribute()]
        public string NodeType { get; set; } = string.Empty;
        [XmlAttribute()]
        public int NodeTypeEnum { get; set; }
        [XmlAttribute()]
        public int NodeFromId { get; set; }
        [XmlAttribute()]
        public int NodeToId { get; set; }
        [XmlAttribute()]
        public int StepId { get; set; }
        [XmlAttribute()]
        public string Action { get; set; } = string.Empty;
        [XmlAttribute()]
        public bool IsPermissioned { get; set; }
        [XmlAttribute()]
        public double TimeToSkip { get; set; }
        [XmlAttribute()]
        public string NodeValue { get; set; }
        [XmlAttribute()]
        public int NodeConditionEnum { get; set; }

        [XmlAttribute()]
        public bool IsAuto { get; set; }

        /// <summary>
        /// Property WorkflowActions (WorkflowActionCollection)
        /// </summary>
        [XmlArray("ArrayOfWorkflowAction")]
        public ValueObjectCollection<WorkflowAction> WorkflowActions { get; set; } = null;
        #endregion Properties

        #region	override Methods
        /// <summary>
        /// Copy all the Member variables from the source object.
        /// Call base.CopyFrom first in the implementation.
        /// </summary>
        /// <param name="source">The source object.</param>
        protected override void _CopyFrom(WorkflowNode source)
        {
            WorkflowId = source.WorkflowId;
            Name = source.Name;
            Description = source.Description;
            NodeTypeEnum = source.NodeTypeEnum;
            NodeType = source.NodeType;
            NodeFromId = source.NodeFromId;
            NodeToId = source.NodeToId;
            StepId = source.StepId;
            Action = source.Action;
            IsPermissioned = source.IsPermissioned;
            TimeToSkip = source.TimeToSkip;
            NodeValue = source.NodeValue;
            NodeConditionEnum = source.NodeConditionEnum;
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
