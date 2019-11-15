using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Application.ValueObjects.Workflow
{
    public class WorkflowHistory : AbstractValueObject<WorkflowHistory>
    {
        #region	Constructors
        ///	<summary>
        ///	default	private constructor	
        ///	</summary>
        private WorkflowHistory()
        {
        }
        #endregion Constructors

        #region	Properties

        [XmlAttribute()]
        public int TransactionId { get; set; }
        [XmlAttribute()]
        public int WorkflowId { get; set; }
        [XmlAttribute()]
        public int CurrentNodeId { get; set; }
        [XmlAttribute()]
        public int ApprovalUserId { get; set; }
        [XmlAttribute()]
        public DateTime ApprovalDate { get; set; }
        [XmlAttribute()]
        public int PrevHistoryId { get; set; }
        [XmlAttribute()]
        public bool IsActive { get; set; }
        [XmlAttribute()]
        public string Comment { get; set; } = string.Empty;
        #endregion

        #region Override Methods
        protected override void _CopyFrom(WorkflowHistory source)
        {
            this.TransactionId = source.TransactionId;
        }

        protected override bool _Equals(WorkflowHistory that)
        {
            throw new NotImplementedException();
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion Override Methods
    
    }
}
