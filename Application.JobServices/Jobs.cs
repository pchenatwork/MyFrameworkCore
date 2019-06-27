using System;
using System.Collections.Generic;
using System.Text;

namespace Application.JobServices
{
    public sealed class Job1 : JobBase
    {
        #region Constructor
        internal Job1(string s) : base(s) { }
        #endregion
        protected override void Run()
        {
            Console.WriteLine("Job 1 '{0}' is running. Message '{1}", base._jobName, _extra);
        }
    }

    public sealed class Job2 : JobBase
    {
        #region Constructor
        internal Job2(string s) : base(s) { }
        #endregion
        protected override void Run()
        {
            Console.WriteLine("Job 2 '{0}' is running", base._jobName);
            Console.WriteLine("Message is '{0}'", _extra);
        }
    }
}
