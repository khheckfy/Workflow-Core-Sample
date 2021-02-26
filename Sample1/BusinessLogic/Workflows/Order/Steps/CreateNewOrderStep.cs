using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Sample1.BusinessLogic.Workflows.Order.Steps
{
    public class CreateNewOrderStep : StepBodyAsync
    {
        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
