using AutoFixture.AutoMoq;
using AutoFixture;

namespace NextUnit.Autofixture.AutoMoq.Core.Customizations
{
    /// <summary>
    /// This enables where being used to setup properties in an object automatically.
    /// </summary>
    public class PropertiesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            // Ensure members are automatically setup, including properties
            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });

            // This additional customization is not strictly necessary for setting up properties,
            // as ConfigureMembers = true already handles this. It's provided here for completeness
            // and could be omitted or replaced with other customizations as needed.

            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });

            //This is commented out code below is an example so you know what else you can build in.
            //And then you'd just need to implement where it is marked.
            //fixture.Customizations.Add(new ActionSpecimenBuilder((specimen, context) =>
            //{
            //    if (specimen is Mock mock)
            //    {
            //        //mock.SetupAllProperties(); <- in a lot of cases it might be enough to build your customization here.
            //    }
            //}));
        }
    }
}
