using Framework.Core.Common;
using Framework.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application.JobServices
{
    public class JobStatusEnum : EnumBase<JobStatusEnum>
    {
        #region Enumeration Elements
        public static readonly JobStatusEnum Failed = new JobStatusEnum(-1, nameof(Failed), "Failed");
        public static readonly JobStatusEnum Started = new JobStatusEnum(0, nameof(Started), "Started");
        public static readonly JobStatusEnum Completed = new JobStatusEnum(1, nameof(Completed), "Completed");
        public static readonly JobStatusEnum CompletedWithError = new JobStatusEnum(2, nameof(CompletedWithError), "Completed with errors");

        /// Conversion Operator     
        /// It has to be here if we need (int)-1 => JobStatus.Failed conversion.
        /// This can not put into the generic EnumBase
        public static implicit operator JobStatusEnum(int id)
        {
            return GetById(id);
        }
        #endregion

        #region Constructors
        private JobStatusEnum(int id, string name, string description)
            : base(id, name, description)
        {
        }
        #endregion

    }
    public interface IJob
    {
        JobStatusEnum Execute();
    }
    public abstract class JobBase : IJob
    {
        /* ==========================================================================
         * PCHEN  201903: Created
         * Abstract Job that ALL Job needs to be based on
         * ==========================================================================*/
        #region Proteccted variables
        protected int _cntErr = 0;
        protected int _cntFatal = 0;
        protected string _jobName = string.Empty;
        protected string _extra = string.Empty;
        #endregion

        #region constructor
        protected JobBase(string job)
        {
            _jobName = job;
        }
        static JobBase()
        {
            //_logger = LoggingUtility.GetLogger(typeof(JobBase).FullName);
        }
        #endregion

        #region Property
        /// <summary>
        /// Parameters in string format for the job(optional).
        /// Each job has to define its own demiliter for multiple parameters(if any)
        /// </summary>
        public string Params
        {
            set { _extra = value; }
        }
        #endregion

        #region Methods
        public JobStatusEnum Execute()
        {
            PreRun();
            Run();
            PostRun();
            return JobStatusEnum.Completed;
        }
        protected abstract void Run();
        /// <summary>
        /// Create the ScheduleHistory record
        /// </summary>
        protected virtual void PreRun()
        {
        }
        /// <summary>
        /// Update ScheduleHistory record with Finishs tatus
        /// </summary>
        protected virtual void PostRun()
        {
        }
        #endregion
    }    
    public sealed class JobFactory : FactoryBase<JobFactory>
    {
        /// Job (JobEnum) need to be defined in the context
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public IJob GetJob(JobEnum job)
        {
            IJob _job = null;
            object[] args = { "ConstructorParameter1" };  // For parametered Constructor 

            try
            {
                // create IJob incident using reflection (parameters)
                _job = Activator.CreateInstance(Type.GetType(job.Extra),
                    BindingFlags.NonPublic | BindingFlags.Instance, null,
                    args, null) as IJob;

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

