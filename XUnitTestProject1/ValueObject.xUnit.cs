using Application.ValueObjects.Workflow;
using Framework.Core.ValueObjects;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class ValueObjectShould
    {
        [Fact]
        public void EqualTest()
        {
            WorkflowList wf1 = ValueObjectFactory<WorkflowList>.Instance.Create();
            wf1.Id = 1;
            wf1.Description = "xxx";
            wf1.Name = "paul";

            WorkflowList wf2 = new WorkflowList()
            {
                Id = 1, Description = "xxx2", Name = "paul2"
            };

            Assert.True(wf1 != wf2);
            wf2.CopyFrom(wf1);
            Assert.True(wf1 == wf2);
            Assert.True(wf1.Equals(wf2));
        }
    }
}
