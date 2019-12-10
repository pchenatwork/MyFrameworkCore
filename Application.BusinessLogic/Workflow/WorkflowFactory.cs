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
        public IWorkflowable GetJob(WorkflowListEnum job)
        {
            IWorkflowable _job = null;
            object[] args = { "ConstructorParameter1" };  // For parametered Constructor 

            try
            {
                // create IJob incident using reflection (parameters)
                _job = Activator.CreateInstance(Type.GetType(job.Description),
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null,
                    args, null) as IWorkflowable;

                /* parameterless
                  _job = (IJob)Activator.CreateInstance(Type.GetType(job.Description));
                    */
            }
            catch (TargetInvocationException e)
            {
                throw new SystemException(e.InnerException.Message, e.InnerException);
            }

            return _job;
        }
    }
}
