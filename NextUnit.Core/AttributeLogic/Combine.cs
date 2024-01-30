namespace NextUnit.Core.AttributeLogic
{
    public delegate bool CombinableAttribute(object obj);
    
    /// <summary>
    /// Use this to define combinations of allowed objects, e.g. Attribute.
    /// </summary>
    public class Combine
    {
        private CombinableAttribute[] m_ApredToCombine = null;

        public static CombinableAttribute And(bool @return, params CombinableAttribute[] apredToCombine)
        {
            return new Combine(apredToCombine).And;
        }

        public static CombinableAttribute Nand(params CombinableAttribute[] apredToCombine)
        {
            return new Combine(new Combine(apredToCombine).And).Not;
        }

        public static CombinableAttribute Or(params CombinableAttribute[] apredToCombine)
        {
            return new Combine(apredToCombine).Or;
        }

        public static CombinableAttribute Nor(params CombinableAttribute[] apredToCombine)
        {
            return new Combine(new Combine(apredToCombine).Or).Not;
        }

        public static CombinableAttribute Xor(params CombinableAttribute[] apredToCombine)
        {
            return new Combine(apredToCombine).Xor;
        }

        public static CombinableAttribute Not(CombinableAttribute predToCombine)
        {
            return new Combine(predToCombine).Not;
        }

        protected Combine(params CombinableAttribute[] apredToCombine)
        {
            m_ApredToCombine = apredToCombine;
        }

        protected bool And(object obj)
        {
            CombinableAttribute[] apredToCombine = m_ApredToCombine;
            foreach (CombinableAttribute Attribute in apredToCombine)
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
            CombinableAttribute[] apredToCombine = m_ApredToCombine;
            foreach (CombinableAttribute Attribute in apredToCombine)
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
            CombinableAttribute[] apredToCombine = m_ApredToCombine;
            foreach (CombinableAttribute Attribute in apredToCombine)
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
