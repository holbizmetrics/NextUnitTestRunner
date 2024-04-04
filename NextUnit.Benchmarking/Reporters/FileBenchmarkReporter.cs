using System;
using System.IO;

namespace NextUnit.Benchmarking
{
    /// <summary>
    /// Outputs a given text to a file.
    /// In this case it would be overwritten every time.
    /// </summary>
    public class FileBenchmarkReporter : IBenchmarkReporter, IDisposable
    {
        private string FilePath { get; } = string.Empty;
        private bool Append { get; set; } = false;
        private StreamWriter StreamWriter = null;
        private bool disposedValue;

        public FileBenchmarkReporter(string filePath, bool append = false)
        {
            FileStream fileStream = new FileStream(filePath, append ? FileMode.Append : FileMode.CreateNew);
            StreamWriter = new StreamWriter(fileStream);

            FilePath = filePath;
            Append = append;
        }

        public void Report(string message)
        {
            StreamWriter.Write(message);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    StreamWriter.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FileBenchmarkReporter()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
