using System;
using System.Windows.Input;

namespace HandwritingInputSimulator.ViewModels.Commands.Abstract;

public abstract class CommandBase : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public virtual bool CanExecute(object? parameter) => true;

    public abstract void Execute(object? parameter);

    /// <summary>
    /// Change occured that affect whether command should be executed or not.
    /// </summary>
    protected void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, new EventArgs());
}
