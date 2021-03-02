using UnityEngine;

namespace Common
{
    public interface IBehaviourCommand
    {
        bool IsFinished { get; }
        void Execute();
        void Interrupt();
    }
}