﻿namespace NextUnit.Core.TestAttributes
{
    /// <summary>
    /// All combinations (permutated) of params in a method (see e.g. XUnit.Combinatorial)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PermutationAttribute : CommonTestAttribute
    {
    }
}