using Framework.Core.BusinessLogic;
using Framework.Core.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application.BusinessLogic.Workflow
{
    public class WorkflowFactory : FactoryBase<WorkflowFactory>
    {
        public IWorkflowable GetWorkflow(WorkflowEnum wf)
        {
            IWorkflowable _wf = null;
            ///object[] args = { "ConstructorParameter1" };  // For parametered Constructor 

            try
            {
                // create IWorkflowable incident using reflection (parameters)
                //_wf = Activator.CreateInstance(Type.GetType(wf.Extra),
                //    BindingFlags.NonPublic | BindingFlags.Instance, null,
                //    null, null) as IWorkflowable;

                /* parameterless*/
                _wf = (IWorkflowable)Activator.CreateInstance(Type.GetType(wf.Extra));
                    
            }
            catch (TargetInvocationException e)
            {
                throw new SystemException(e.InnerException.Message, e.InnerException);
            }

            return _wf;
        }
    }
}
