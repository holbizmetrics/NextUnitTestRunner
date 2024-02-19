namespace NextUnit.TestRunner
{
    public interface IInstanceCreationBehavior
    {
        public object CreateInstance(Type type);

        /// <summary>
        /// For now this will be used to mark if an instance creationbehavior will be called at start.
        /// This should normally become a relict if the design further progresses.
        /// </summary>
        public bool OnlyInitializeAtStartBehavior{ get; }
    }
}
