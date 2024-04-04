namespace NextUnit.TestRunner
{
    /// <summary>
    /// Behavior to recreate the objects for each test. No object will be cached, yet.
    /// </summary>
    public class RecreateObjectInstanceForEachTest : IInstanceCreationBehavior
    {
        public bool OnlyInitializeAtStartBehavior => false;

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
