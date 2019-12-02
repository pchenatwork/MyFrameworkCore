using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class PlanWorkflowAction : EnumBase<PlanWorkflowAction>
    {

        #region Enumeration Elements
        /* ==========================================================================
         * Name: 
         * Description:  TypeName for reflection
         * ==========================================================================*/
        public static readonly PlanWorkflowAction SubmitPlan = new PlanWorkflowAction(8, "SubmitPlan", "Submit Plan");
        public static readonly PlanWorkflowAction ManagerApproval = new PlanWorkflowAction(10, "ManagerApproval", "Manager Approval");
        public static readonly PlanWorkflowAction HRRequestMoreInfo = new PlanWorkflowAction(13, "HRRequestMoreInfo", "HR Request More Info");
        public static readonly PlanWorkflowAction HRApproval = new PlanWorkflowAction(12, "HRApproval", "HR Approval");
        public static readonly PlanWorkflowAction SubmitMoreInfo = new PlanWorkflowAction(15, "SubmitMoreInfo", "Submit More Info");
        #endregion

        #region Constructors
        private PlanWorkflowAction(int id, string name, string description)
            : base(id, name, description)
        {
        }
        #endregion
    }
}
