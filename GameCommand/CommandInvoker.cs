using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private readonly Stack<ICommand> _commandStack = new Stack<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commandStack.Push(command);
    }

    public void ExecuteCommand()
    {
        if (_commandStack.Count > 0)
        {
            ICommand command = _commandStack.Pop();
            command.Execute();
        }
    }
}