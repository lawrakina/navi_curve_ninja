using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

namespace Tests
{
    public class RunValidationTestsBeforeBuild : IPreprocessBuildWithReport 
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            var results = new ResultCollector();
            var api = ScriptableObject.CreateInstance<TestRunnerApi>();
            
            api.RegisterCallbacks(results);
            api.Execute(new ExecutionSettings()
            {
                runSynchronously = true,
                filters = new Filter[]
                {
                    new Filter()
                    {
                        categoryNames = new string[] { "PreBuildValidationTests" },
                        testMode = TestMode.EditMode
                    }
                }
            });

            if (results.Result.FailCount > 0)
                throw new BuildFailedException($"any test failed\n{results.Result.Message}");
        }
    }
}