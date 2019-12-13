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
        public static readonly WorkflowListEnum TimeoffHRWorkflow = new WorkflowListEnum(2, nameof(TimeoffHRWorkflow), typeof(TimeoffWorkflow).FullName);
        #endregion

        private WorkflowListEnum(int id, string name, string extra) : base(id, name, extra)
        {
        }
    }
}
