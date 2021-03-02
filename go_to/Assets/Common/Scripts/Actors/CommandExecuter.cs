using System;
using System.Collections.Generic;
using UniRx.Triggers;

namespace Common
{
    public class CommandExecuter
    {
        IBehaviourCommand currentCommand = null;
        List<IBehaviourCommand> commands = new List<IBehaviourCommand>();
        public Action OnFinish { get; set; }
        public bool IsEmpty => (commands.Count == 0);

        public void AddCommand(IBehaviourCommand command)
        {
            if (command == null) return;
            commands.Add(command);
        }

        public void RemoveCommand(IBehaviourCommand command)
        {
            if (command == null) return;
            commands.Remove(command);
        }

        void Execute()
        {
            if (commands.Count > 0)
            {
                currentCommand = commands[0];
                currentCommand.Execute();
            }
        }

        public void InterruptCurrentCommand()
        {
            currentCommand?.Interrupt();
        }

        public void Update()
        {
            if (currentCommand == null)
            {
                Execute();
            }
            else
            {
                if (currentCommand.IsFinished)
                {
                    RemoveCommand(currentCommand);
                    OnFinish?.Invoke();
                    currentCommand = null;
                    Execute();
                }
            }
        }
    }
}