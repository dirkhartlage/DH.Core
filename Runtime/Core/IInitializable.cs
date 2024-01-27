using System;

namespace DH.Core
{
    public interface IInitializable
    {
        Action Initialized { get; set; }

        InitializationState InitializationState { get; }
    }
}