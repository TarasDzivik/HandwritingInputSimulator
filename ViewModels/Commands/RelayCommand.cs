using HandwritingInputSimulator.ViewModels.Commands.Abstract;
using System;

namespace HandwritingInputSimulator.ViewModels.Commands;

/// <summary>
/// Command used to execute associated routine, mostly used as a link with the View Buttons.
/// </summary>
public class RelayCommand : CommandBase
{
    private readonly Action<object>? _executeAction;
    private readonly Predicate<object>? _canExecute;

    /// <summary>
    /// Create a Relay Command based on a given function and optional predicate.
    /// </summary>
    /// <param name="executeAction"> Routine of one parameter and without return value, to be executed when required. </param>
    /// <param name="canExecute"> Predicate determining permission for command execution. </param>
    /// <exception cref="ArgumentNullException"> Critical error, if the action that was tried to be executed was null. </exception>
    public RelayCommand(Action<object> executeAction, Predicate<object>? canExecute = null)
    {
        _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter!);
    public override void Execute(object? parameter) => _executeAction!(parameter ?? "<N/A>");
}

