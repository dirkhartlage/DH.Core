using System;

namespace DH.Core.Dependencies
{
    public abstract class Dependency
    {
        public Action Satisfied;
        public bool IsSatisfied { get; protected set; }
    }
}