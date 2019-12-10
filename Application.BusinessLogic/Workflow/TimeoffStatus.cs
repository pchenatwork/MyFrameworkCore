using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class TimeoffStatus : EnumBase<TimeoffStatus>
    {

        #region Enumeration Elements
        /* ==========================================================================
         * Name: 
         * Description:  TypeName for reflection
         * ==========================================================================*/
        public static readonly TimeoffStatus NewPlan = new TimeoffStatus(1, "NewPlan", "New Plan");
        public static readonly TimeoffStatus PlanSubmitted = new TimeoffStatus(2, "PlanSubmitted", "Plan Submitted");
        public static readonly TimeoffStatus ManagerApproved = new TimeoffStatus(3, "ManagerApproved", "Manager Approved");
        public static readonly TimeoffStatus WaitingHR = new TimeoffStatus(4, "WaitingHR", "Waiting for HR Approval");
        public static readonly TimeoffStatus HRApproved = new TimeoffStatus(5, "HRApproved", "HRApproved");
        public static readonly TimeoffStatus HRMoreInfoPending = new TimeoffStatus(6, "HRMoreInfoPending", "HR Requested More Info");
        public static readonly TimeoffStatus Approved = new TimeoffStatus(7, "Approved", "All Approved");
        #endregion

        #region Constructors
        private TimeoffStatus(int id, string name, string description)
            : base(id, name, description)
        {
        }
        #endregion
    }
}
