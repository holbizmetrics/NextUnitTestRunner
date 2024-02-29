namespace NextUnit.Core.AttributeLogic
{
    public delegate bool CombinableAttributeDelegate(object obj);
    
    /// <summary>
    /// Use this to define combinations of allowed objects, e.g. Attribute.
    /// </summary>
    public class Combine
    {
        private CombinableAttributeDelegate[] m_ApredToCombine = null;

        public static CombinableAttributeDelegate And(bool @return, params CombinableAttributeDelegate[] apredToCombine)
        {
            return new Combine(apredToCombine).And;
        }

        public static CombinableAttributeDelegate Nand(params CombinableAttributeDelegate[] apredToCombine)
        {
            return new Combine(new Combine(apredToCombine).And).Not;
        }

        public static CombinableAttributeDelegate Or(params CombinableAttributeDelegate[] apredToCombine)
        {
            return new Combine(apredToCombine).Or;
        }

        public static CombinableAttributeDelegate Nor(params CombinableAttributeDelegate[] apredToCombine)
        {
            return new Combine(new Combine(apredToCombine).Or).Not;
        }

        public static CombinableAttributeDelegate Xor(params CombinableAttributeDelegate[] apredToCombine)
        {
            return new Combine(apredToCombine).Xor;
        }

        public static CombinableAttributeDelegate Not(CombinableAttributeDelegate predToCombine)
        {
            return new Combine(predToCombine).Not;
        }

        protected Combine(params CombinableAttributeDelegate[] apredToCombine)
        {
            m_ApredToCombine = apredToCombine;
        }

        protected bool And(object obj)
        {
            CombinableAttributeDelegate[] apredToCombine = m_ApredToCombine;
            foreach (CombinableAttributeDelegate Attribute in apredToCombine)
            {
                if (!Attribute(obj))
                {
                    return false;
                }
            }
            return true;
        }

        protected bool Or(object obj)
        {
            CombinableAttributeDelegate[] apredToCombine = m_ApredToCombine;
            foreach (CombinableAttributeDelegate Attribute in apredToCombine)
            {
                if (Attribute(obj))
                {
                    return true;
                }
            }
            return false;
        }

        protected bool Xor(object obj)
        {
            bool flag = false;
            CombinableAttributeDelegate[] apredToCombine = m_ApredToCombine;
            foreach (CombinableAttributeDelegate Attribute in apredToCombine)
            {
                flag = (Attribute(obj) != flag);
            }
            return flag;
        }

        protected bool Not(object obj)
        {
            if (m_ApredToCombine.Length != 1)
            {
                throw new Exception("Not only be used for exactly one Attribute");
            }
            return !m_ApredToCombine[0](obj);
        }

        public void Test()
        {
            Not(Xor(m_ApredToCombine));
        }
    }
}
