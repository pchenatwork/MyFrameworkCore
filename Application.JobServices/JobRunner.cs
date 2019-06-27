using System;
using System.Collections.Generic;
using System.Text;

namespace Application.JobServices
{
    public class JobRunner
    {
        /* ==========================================================================
         * PCHEN  201903: Created
         * JobRunner: trigger a job
         * ==========================================================================*/
        #region Private variables
        private const string CLASS_NAME = nameof(JobRunner);
        //private static ILog _logger = null;
        #endregion

        #region	Constructors
        /// <summary>
        /// class constructor initializes logging
        /// </summary>
        static JobRunner()
        {
            // _logger = LoggingUtility.GetLogger(typeof(JobRunner).FullName);
        }
        ///	<summary>
        ///	No constructor	
        ///	</summary>
        private JobRunner()
        {
        }
        #endregion

        #region Handle to Run a Job
        /// <summary>
        /// Run the job
        /// </summary>
        /// <param name="jobName">JobName has to matched to [JobSchedule].Jobname</param>
        public static JobStatusEnum RunJob(string jobName)
        {
            return RunJob(jobName, string.Empty);
        }
        /// <summary>
        /// Run the job with params
        /// </summary>
        /// <param name="jobName">JobName has to matched to [JobSchedule].Jobname</param>
        /// <param name="extra">Extra values for execute the job, eg a SendTo to Email</param>
        public static JobStatusEnum RunJob(string jobName, string extra)
        {
            JobEnum job = JobEnum.GetByName(jobName);
            return RunJob(job, extra);
        }
        public static JobStatusEnum RunJob(JobEnum job)
        {
            return RunJob(job, string.Empty);
        }
        private static JobStatusEnum RunJob(JobEnum job, string extra)
        {
            JobStatusEnum jobStatus = JobStatusEnum.Failed; // assume failed
            try
            {
                IJob _job = JobFactory.Instance.GetJob(job);
                ((JobBase)_job).Params = extra;
                jobStatus = _job.Execute();
            }
            catch (Exception ex)
            {
                //   _logger.Error(CLASS_NAME + ".RunJob() Excepted: Job Name = '" + (job == null ? "{null}" : job.Name) + "'", ex);
            }
            return jobStatus;
        }
        #endregion
    }
}
