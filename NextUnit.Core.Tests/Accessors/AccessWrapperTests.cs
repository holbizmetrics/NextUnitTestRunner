using NextUnit.Core.Accessors;
using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;

namespace NextUnit.Core.Tests.Accessors
{
    public class AccessWrapperTests
    {
        [Test]
        public void SetPropertyOrField_GetPropertyOrField_Test()
        {
            AccessWrapperTestClass accessWrapperTestClassInstance = new AccessWrapperTestClass();
            AccessWrapper accessWrapper = new AccessWrapper(accessWrapperTestClassInstance);

            //public name
            string publicName = "PublicName";
            accessWrapper.SetPropertyOrField(publicName, publicName);
            Assert.AreEqual(publicName, accessWrapper.GetPropertyOrField(publicName));

            string privateName = "PrivateName";
            accessWrapper.SetPropertyOrField(privateName, privateName);
            Assert.AreEqual(privateName, accessWrapper.GetPropertyOrField(privateName));

            //internal name
            string internalName = "InternalName";
            accessWrapper.SetPropertyOrField(internalName, internalName);
            Assert.AreEqual(internalName, accessWrapper.GetPropertyOrField(internalName));
        }

        [Test]
        public void SetPropertyOrField_GetPropertyOrField_ForArrays_Test()
        {
            AccessWrapperTestClass accessWrapperTestClassInstance = new AccessWrapperTestClass();

            AccessWrapper accessWrapper = new AccessWrapper(accessWrapperTestClassInstance);

            //public name
            string publicNames = "PublicNames";
            int index = 2;
            accessWrapper.SetPropertyOrField(publicNames, publicNames, index);
            Assert.AreEqual(publicNames, accessWrapper.GetPropertyOrField(publicNames, index));

            string privateNames = "PrivateNames";
            accessWrapper.SetPropertyOrField(privateNames, privateNames, index);
            Assert.AreEqual(privateNames, accessWrapper.GetPropertyOrField(privateNames, index));

            //internal name
            string internalNames = "InternalNames";
            accessWrapper.SetPropertyOrField(internalNames, internalNames, index);
            Assert.AreEqual(internalNames, accessWrapper.GetPropertyOrField(internalNames, index));
        }

        [Test]
        public void AccessWrapperInvokeMethodTest()
        {
            AccessWrapperTestClass accessWrapperTestClassInstance = new AccessWrapperTestClass();

            AccessWrapper accessWrapper = new AccessWrapper(accessWrapperTestClassInstance);
            Assert.IsTrue(accessWrapper.AsDynamic().Test());
        }

        public class AccessWrapperTestClass
        {
            public string PublicName { get; set; } = string.Empty;
            private string PrivateName { get; set; } = string.Empty;
            internal string InternalName { get; set; } = string.Empty;

            public List<string> PublicNames { get;set; } = new List<string>();
            private List<string> PrivateNames { get; set; } = new List<string>();
            internal List<string> InternalNames { get; set; } = new List<string>();

            public AccessWrapperTestClass()
            {
                PublicNames.AddRange(new string[] { "Smith", "Jones", "Miller", "Haskell", "Beautenon", "Hasgard", "Romired", "Lagarde" });
                PrivateNames.AddRange(new string[] { "Brown", "Wilson", "Willams", "Robbins", "Brown", "Black", "Martinez", "Davis"});
                InternalNames.AddRange(new string[] { "Sue", "John", "Miley", "Sabrina", "Sandra", "Alex", "Ang", "Melany", "Stephanie"});
            }

            /// <summary>
            /// To see if we can call the method.
            /// </summary>
            /// <returns></returns>
            public bool Test()
            {
                return true;
            }
        }
    }
}
