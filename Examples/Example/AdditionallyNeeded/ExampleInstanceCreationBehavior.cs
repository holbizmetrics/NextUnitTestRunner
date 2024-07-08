using NextUnit.TestRunner;

namespace Blub.AdditionallyNeeded
{
    public class ExampleInstanceCreationBehavior : IInstanceCreationBehavior
    {
        public bool OnlyInitializeAtStartBehavior => false;

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
