using AppBase.Core.Interfaces;
using AppBase.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Application.ValueObjects.Workflow
{
    public class WorkflowHistory : ValueObjectBase
    {
        #region	Constructors
        ///	<summary>
        ///	default	private constructor	
        ///	</summary>
        private WorkflowHistory()
        {
        }
        static WorkflowHistory()
        {
        }

        #endregion Constructors

        #region	Properties

        [XmlAttribute()]
        public int TransactionId { get; set; }
        [XmlAttribute()]
        public int WorkflowId { get; set; }
        [XmlAttribute()]
        public int NodeId { get; set; }
        [XmlAttribute()]
        public string ApprovalUser { get; set; }
        [XmlAttribute()]
        public DateTime? ApprovalDate { get; set; }
        [XmlAttribute()]
        public int PrevHistoryId { get; set; }
        [XmlAttribute()]
        public bool IsCurrent { get; set; }
        [XmlAttribute()]
        public string Comment { get; set; } = string.Empty;
        #endregion

        #region Override Methods
        protected override void _CopyFrom(IValueObject src)
        {
            WorkflowHistory source = src as WorkflowHistory;
            this.TransactionId = source.TransactionId;
        }

        protected override bool _Equals(IValueObject that)
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
