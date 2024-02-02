using System.CodeDom.Compiler;

namespace NextUnit.Compiler.CompileCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class CompilerErrorEventArgs : EventArgs
    {
        private CompilerErrorCollection m_CompilerErrorCollection;
        private string m_sSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerErrorEventArgs"/> class.
        /// </summary>
        public CompilerErrorEventArgs()
        {
            m_CompilerErrorCollection = null;
            m_sSource = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerErrorEventArgs"/> class.
        /// </summary>
        /// <param name="compilerErrorCollection">The compiler error collection.</param>
        public CompilerErrorEventArgs(CompilerErrorCollection compilerErrorCollection)
        {
            m_CompilerErrorCollection = compilerErrorCollection;
            m_sSource = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompilerErrorEventArgs"/> class.
        /// </summary>
        /// <param name="compilerErrorCollection">The compiler error collection.</param>
        /// <param name="_sSource">The s source.</param>
        public CompilerErrorEventArgs(CompilerErrorCollection compilerErrorCollection,
            string _sSource) : this(compilerErrorCollection)
        {
            m_sSource = _sSource;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string str = string.Empty;
            foreach (CompilerError error in m_CompilerErrorCollection)
            {
                str = str + error.ToString();
            }
            return str;
        }

        /// <summary>
        /// Gets the compiler error collection.
        /// </summary>
        /// <value>
        /// The compiler error collection.
        /// </value>
        public CompilerErrorCollection CompilerErrorCollection
        {
            get { return m_CompilerErrorCollection; }
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source
        {
            get { return m_sSource; }
        }
    }
}