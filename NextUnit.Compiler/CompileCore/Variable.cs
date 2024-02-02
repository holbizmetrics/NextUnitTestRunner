namespace NextUnit.Compiler.CompileCore
{
    public class Variable
    {
        private object m_Value = null;
        private string m_Name = string.Empty;

        public Variable(string _sName, object _oValue)
        {
            m_Name = _sName;
            Value = _oValue;
        }

        public override string ToString()
        {
            return m_Name + " : " + m_Value.ToString();
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public object Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
                if (m_Value == null || m_Value.ToString() == string.Empty)
                {
                    m_Value = 0.0;
                }
            }
        }
    }
}

