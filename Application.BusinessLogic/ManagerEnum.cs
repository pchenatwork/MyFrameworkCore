using AppBase.Core.ValueObjects;
using Application.BusinessLogic.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class ManagerEnum : EnumBase<ManagerEnum>
    {

        #region Enumeration Elements
        /* ==========================================================================
         * Name: 
         * Description:  TypeName for reflection
         * ==========================================================================*/
        public static readonly ManagerEnum Job1 = new ManagerEnum(1, nameof(WorkflowNodeManager), typeof(WorkflowNodeManager).FullName);
        public static readonly ManagerEnum Mgr2 = new ManagerEnum(2, nameof(WorkflowListManager), typeof(WorkflowListManager).FullName);

        #endregion

        #region Constructors
        private ManagerEnum(int id, string name, string extra) : base(id, name, extra)
        {
        }
        #endregion
    }
}
