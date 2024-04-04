using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace NextUnit.TestExplorer
{
    public class RunContext : IRunContext
    {
        public bool KeepAlive => throw new NotImplementedException();

        public bool InIsolation => throw new NotImplementedException();

        public bool IsDataCollectionEnabled => throw new NotImplementedException();

        public bool IsBeingDebugged => throw new NotImplementedException();

        public string? TestRunDirectory => throw new NotImplementedException();

        public string? SolutionDirectory => throw new NotImplementedException();

        public IRunSettings? RunSettings => throw new NotImplementedException();

        public ITestCaseFilterExpression? GetTestCaseFilter(IEnumerable<string>? supportedProperties, Func<string, TestProperty?> propertyProvider)
        {
            throw new NotImplementedException();
        }
    }
}
