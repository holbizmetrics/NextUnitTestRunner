﻿namespace NextUnit.TestRunner
{
    public class RecreateObjectInstanceForEachTest : IInstanceCreationBehavior
    {
        public bool OnlyInitializeAtStartBehavior => false;

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
