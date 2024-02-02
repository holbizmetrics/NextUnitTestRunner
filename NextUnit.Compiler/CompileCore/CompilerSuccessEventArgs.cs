using System.Reflection;

namespace NextUnit.Compiler.CompileCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class CompilerSuccessEventArgs : EventArgs
    {
        private Assembly m_Assembly;
        private string m_Source;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerSuccessEventArgs"/> class.
        /// </summary>
        public CompilerSuccessEventArgs()
        {
            m_Assembly = null;
            m_Source = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerSuccessEventArgs"/> class.
        /// </summary>
        /// <param name="_compilerErrorCollection">The compiler error collection.</param>
        public CompilerSuccessEventArgs(Assembly assembly)
        {
            m_Assembly = null;
            m_Source = string.Empty;
            m_Assembly = assembly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerSuccessEventArgs"/> class.
        /// </summary>
        /// <param name="_compilerErrorCollection">The compiler error collection.</param>
        /// <param name="source">The s source.</param>
        public CompilerSuccessEventArgs(Assembly assembly,
            string source) : this(assembly)
        {
            m_Source = source;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return m_Assembly != null ? m_Assembly.ImageRuntimeVersion : "";
        }

        /// <summary>
        /// Gets the compiler error collection.
        /// </summary>
        /// <value>
        /// The compiler error collection.
        /// </value>
        public Assembly Assembly
        {
            get { return m_Assembly; }
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source
        {
            get { return m_Source; }
            set { m_Source = value; }
        }

    }
}