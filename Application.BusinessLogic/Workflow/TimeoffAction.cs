using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class TimeoffAction : EnumBase<TimeoffAction>
    {

        #region Enumeration Elements
        /* ==========================================================================
         * Name: 
         * Description:  TypeName for reflection
         * ==========================================================================*/
        public static readonly TimeoffAction SubmitPlan = new TimeoffAction(8, "SubmitPlan", "Submit Plan");
        public static readonly TimeoffAction ManagerApproval = new TimeoffAction(10, "ManagerApproval", "Manager Approval");
        public static readonly TimeoffAction HRRequestMoreInfo = new TimeoffAction(13, "HRRequestMoreInfo", "HR Request More Info");
        public static readonly TimeoffAction HRApproval = new TimeoffAction(12, "HRApproval", "HR Approval");
        public static readonly TimeoffAction SubmitMoreInfo = new TimeoffAction(15, "SubmitMoreInfo", "Submit More Info");
        #endregion

        #region Constructors
        private TimeoffAction(int id, string name, string description)
            : base(id, name, description)
        {
        }
        #endregion
    }
}
