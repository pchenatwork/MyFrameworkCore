using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ValueObjects.Workflow
{
    public class WorkflowHistory : AbstractValueObject<WorkflowHistory>
    {
        protected override void _CopyFrom(WorkflowHistory source)
        {
            throw new NotImplementedException();
        }

        protected override bool _Equals(WorkflowHistory that)
        {
            throw new NotImplementedException();
        }

        protected override int _GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
