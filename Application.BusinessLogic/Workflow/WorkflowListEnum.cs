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
        public static readonly WorkflowListEnum AEContactReminder = new WorkflowListEnum(1, "AE Contact 90-day Reminder", typeof(WorkflowControl).FullName);
        #endregion

        protected WorkflowListEnum(int id, string name, string description) : base(id, name, description)
        {
        }
    }
}
