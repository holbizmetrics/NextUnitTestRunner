using NextUnit.Autofixture.AutoMoq.Core;
using NextUnit.Core.AttributeLogic;
using NextUnit.Core.Combinators;

namespace NextUnit.TestRunner.TestRunners
{
    public interface ITestRunnerSettings
    {
        ICombinator Combinator { get; set; }
        ITestDiscoverer TestDiscoverer { get; set; }
        bool UseThreading { get; set; }
        IAttributeLogicMapper AttributeLogicMapper { get; set; }
        IInstanceCreationBehavior InstanceCreationBehavior { get; set; }
        bool PreferDelegates { get; set; }
    }

    /// <summary>
    /// Contains settings for the TestRunner
    /// For now: 
    /// 
    /// 1. Either can the TestRunner be called with an empty constructor
    /// AND then setup via property.
    /// 2. We can also setup the TestRunner using a fluent syntax since TestRunner3.
    /// 3. And also, now, we're able to summarize all settings in TestRunnerSettings that can be get/set.
    /// </summary>
    public abstract class TestRunnerSettings : ITestRunnerSettings
    {
        public abstract ICombinator Combinator { get; set; }
        public abstract ITestDiscoverer TestDiscoverer { get; set; }
        public abstract bool UseThreading { get; set; }
        public abstract IAttributeLogicMapper AttributeLogicMapper { get; set; }
        public abstract IInstanceCreationBehavior InstanceCreationBehavior { get; set; }
        public bool PreferDelegates { get; set; }

        public TestRunnerSettings()
        {
        }
        public static TestRunnerSettings CreateTestRunnerSettings(ITestRunner4 testRunner4)
        {
            TestRunnerSettings testRunnerSettings = new CurrentTestRunnerSettings();
            return testRunnerSettings;
        }
    }

    public class CurrentTestRunnerSettings : TestRunnerSettings
    {
        public CurrentTestRunnerSettings()
        {
            this.Combinator = new DefaultCombinator();
            this.TestDiscoverer = new TestDiscoverer();
            this.UseThreading = false;
            this.AttributeLogicMapper = new AutofixtureAutomoqAttributeAttributeLogicMapper();
            this.InstanceCreationBehavior = new RecreateObjectInstanceForEachTest();
        }

        public CurrentTestRunnerSettings(ITestRunner4 testRunner)
        {
            this.Combinator = testRunner.Combinator;
            this.TestDiscoverer = testRunner.TestDiscoverer;
            this.UseThreading = testRunner.UseThreading;
            this.AttributeLogicMapper = testRunner.AttributeLogicMapper;
            this.InstanceCreationBehavior = testRunner.InstanceCreationBehavior;
        }

        public override ICombinator Combinator { get;set; }

        public override ITestDiscoverer TestDiscoverer { get; set; }
        public override bool UseThreading { get; set; }

        public override IAttributeLogicMapper AttributeLogicMapper { get; set; }

        public override IInstanceCreationBehavior InstanceCreationBehavior { get; set; }
    }


    public class DefaultTestRunnerSettings : TestRunnerSettings
    {
        public DefaultTestRunnerSettings()
        {
            this.PreferDelegates = true;
        }

        public override ICombinator Combinator { get => new DefaultCombinator(); set { } }

        public override ITestDiscoverer TestDiscoverer { get => new TestDiscoverer(); set { } }

        public override bool UseThreading { get => false; set { } }

        public override IAttributeLogicMapper AttributeLogicMapper { get => new AutofixtureAutomoqAttributeAttributeLogicMapper(); set { } }

        public override IInstanceCreationBehavior InstanceCreationBehavior { get => new RecreateObjectInstanceForEachTest(); set { } }
    }
}
