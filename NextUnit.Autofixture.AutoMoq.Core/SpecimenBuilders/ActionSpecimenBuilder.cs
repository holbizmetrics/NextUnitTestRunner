using AutoFixture.Kernel;

namespace NextUnit.Autofixture.AutoMoq.Core.SpecimenBuilders
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Action<object, ISpecimenContext> _action;

        public ActionSpecimenBuilder(Action<object, ISpecimenContext> action)
        {
            _action = action;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var specimen = context.Resolve(request);
            _action(specimen, context);

            return specimen;
        }
    }
}
