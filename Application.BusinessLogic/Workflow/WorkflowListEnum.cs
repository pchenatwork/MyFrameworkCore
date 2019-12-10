using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowListEnum : EnumBase<WorkflowListEnum>
    {
        #region Enumeration Elements
        /* ==========================================================================
         * PCHEN 201911
         * ==========================================================================*/
        public static readonly WorkflowListEnum TimeoffWorkflow = new WorkflowListEnum(1, nameof(TimeoffWorkflow), typeof(TimeoffWorkflow).FullName);
        #endregion

        protected WorkflowListEnum(int id, string name, string description) : base(id, name, description)
        {
        }
    }
}
