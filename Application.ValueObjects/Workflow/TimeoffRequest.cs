using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ValueObjects.Workflow
{
    public class TimeoffRequest : AbstractValueObject<TimeoffRequest>
    {
        public int TransactionId { get; set; }

        public int TimeoffTypeEnum { get; set; }

        public string TimeoffType { get; set; }

        public string User { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Note { get; set; }

        public int StatusId { get; set; }
        public int HRStatusId { get; set; }

        public bool IsActive { get; set; }


        protected override void _CopyFrom(TimeoffRequest source)
        {
            throw new NotImplementedException();
        }

        protected override bool _Equals(TimeoffRequest that)
        {
            throw new NotImplementedException();
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
