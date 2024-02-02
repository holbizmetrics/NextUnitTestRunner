namespace NextUnit.Compiler.CompileCore
{
    public class TypeCreationErrorEventArgs : EventArgs
    {
        private string m_TypeName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeCreationErrorEventArgs"/> class.
        /// </summary>
        /// <param name="typeName">Name of the s type.</param>
        public TypeCreationErrorEventArgs(string typeName)
        {
            m_TypeName = typeName;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return m_TypeName;
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <value>
        /// The name of the type.
        /// </value>
        public string TypeName
        {
            get { return m_TypeName; }
        }
    }
}

