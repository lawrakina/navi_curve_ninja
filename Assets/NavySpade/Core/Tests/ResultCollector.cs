using UnityEditor.TestTools.TestRunner.Api;

namespace Tests
{
    public class ResultCollector : ICallbacks
    {
        public ITestResultAdaptor Result { get; private set; }
        
        public void RunStarted(ITestAdaptor testsToRun)
        {
            
        }

        public void RunFinished(ITestResultAdaptor result)
        {
            
        }

        public void TestStarted(ITestAdaptor test)
        {
            
        }

        public void TestFinished(ITestResultAdaptor result)
        {
            Result = result;
        }
    }
}