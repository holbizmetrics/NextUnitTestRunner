using NextUnit.Core.Asserts;

namespace NextUnit.TestRunner.NewFolder
{
    internal class NunitAssertExtensions
    {
        public static class AssertExtensions
        {
            public static void EventFired<TEventArgs>(Action<EventHandler<TEventArgs>> subscribe, Action<EventHandler<TEventArgs>> unsubscribe, Action actionToTriggerEvent)
                where TEventArgs : EventArgs
            {
                bool eventFired = false;

                void Handler(object sender, TEventArgs e)
                {
                    eventFired = true;
                }

                subscribe(Handler);

                try
                {
                    actionToTriggerEvent();
                    Assert.IsTrue(eventFired, $"Expected event of type {typeof(TEventArgs).Name} to be fired.");
                }
                finally
                {
                    unsubscribe(Handler);
                }
            }
        }
    }
}
