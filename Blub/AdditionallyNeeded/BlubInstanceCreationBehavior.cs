using NextUnit.TestRunner;

namespace Blub.AdditionallyNeeded
{
    public class BlubInstanceCreationBehavior : IInstanceCreationBehavior
    {
        public bool OnlyInitializeAtStartBehavior => false;

        public object CreateInstance(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
