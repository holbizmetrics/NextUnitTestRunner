﻿using System.Diagnostics;
using System.Reflection;

namespace FileSourcesParser
{
    public class FilesourcesParser
    {
        public void GetSourceCodeLine(string assemblyPath)
        {
            var blub = ValueTask.CompletedTask;
            var assemblyDefinition = Assembly.LoadFrom(assemblyPath);

            foreach (var module in assemblyDefinition.Modules)
            {
                foreach (var type in module.GetTypes())
                {
                    foreach (var method in type.GetMethods())
                    {
                        //if (method.HasMetadataToken && method.DebugInformation.HasSequencePoints)
                        {
                            //foreach (var seqPoint in method.DebugInformation.SequencePoints)
                            {
                                Trace.WriteLine($"Method: {method.Name}");
                                //Trace.WriteLine($"File: {seqPoint.Document.Url}");
                                //Trace.WriteLine($"Start Line: {seqPoint.StartLine}");
                                //Trace.WriteLine($"End Line: {seqPoint.EndLine}");
                                // Break or continue as needed
                            }
                        }
                    }
                }
            }
        }
    }
}
