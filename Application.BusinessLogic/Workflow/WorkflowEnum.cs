using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowEnum : EnumBase<WorkflowEnum>
    {
        #region Enumeration Elements
        /* ==========================================================================
         * PCHEN 201911
         * ==========================================================================*/
        public static readonly WorkflowEnum TimeoffMainFlow = new WorkflowEnum(1, nameof(TimeoffMainFlow), typeof(TimeoffWorkflow).FullName);
        public static readonly WorkflowEnum TimeoffHRFlow = new WorkflowEnum(2, nameof(TimeoffHRFlow), typeof(TimeoffWorkflow).FullName);
        #endregion

        private WorkflowEnum(int id, string name, string extra) : base(id, name, extra)
        {
        }
    }
}
