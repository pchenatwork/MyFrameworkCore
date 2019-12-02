using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class PlanWorkflowStatus : EnumBase<PlanWorkflowStatus>
    {

        #region Enumeration Elements
        /* ==========================================================================
         * Name: 
         * Description:  TypeName for reflection
         * ==========================================================================*/
        public static readonly PlanWorkflowStatus NewPlan = new PlanWorkflowStatus(1, "NewPlan", "New Plan");
        public static readonly PlanWorkflowStatus PlanSubmitted = new PlanWorkflowStatus(2, "PlanSubmitted", "Plan Submitted");
        public static readonly PlanWorkflowStatus ManagerApproved = new PlanWorkflowStatus(3, "ManagerApproved", "Manager Approved");
        public static readonly PlanWorkflowStatus WaitingHR = new PlanWorkflowStatus(4, "WaitingHR", "Waiting for HR Approval");
        public static readonly PlanWorkflowStatus HRApproved = new PlanWorkflowStatus(5, "HRApproved", "HRApproved");
        public static readonly PlanWorkflowStatus HRMoreInfoPending = new PlanWorkflowStatus(6, "HRMoreInfoPending", "HR Requested More Info");
        public static readonly PlanWorkflowStatus Approved = new PlanWorkflowStatus(7, "Approved", "All Approved");
        #endregion

        #region Constructors
        private PlanWorkflowStatus(int id, string name, string description)
            : base(id, name, description)
        {
        }
        #endregion
    }
}
