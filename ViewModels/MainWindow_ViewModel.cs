using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using HandwritingInputSimulator.ViewModels.Commands;

namespace HandwritingInputSimulator.ViewModels;

public class MainWindow_ViewModel : ViewModelBase
{
    private int currentCharIndex;
    private DispatcherTimer? timer;
    public string Lines;
    public string? DisplayText
    {
        get => displayText;
        set
        {
            displayText = value;
            OnPropertyChanged(nameof(DisplayText));
        }
    }
    private string? displayText;
    public ICommand StartCommand { get; }
    private static HashSet<string> charactersWithLongDelay = new HashSet<string> { "🎶", "💻", "🚀" };

    public MainWindow_ViewModel()
    {
        // TODO: Instead the hardcoded text It could be implementation like download txt file, with read lines and other technics
        Lines = "🎶 Amidst the keystrokes, where logic aligns,\n" +
               "A C# poet, seeking code that shines.\n" +
               "Open to roles, a dance on the IDE floor,\n" +
               "In the syntax ballet, I yearn for more.\n\n" +

               "💻 Lines of poetry in binary attire,\n" +
               "In the realm of coding, where dreams transpire.\n" +
               "Connect with me, let's craft the verse,\n" +
               "In the coding universe, let's converse.\n\n" +

               "🚀 Algorithms hum, a rhythmic refrain,\n" +
               "Open to opportunities, breaking the chain.\n" +
               "C# is my language, my digital prose,\n" +
               "Together, let's compose where the code flows.\n";

        StartCommand = new RelayCommand(c => StartHandWriteSimulation());
    }

    #region Command_implementations
    private void StartHandWriteSimulation()
    {
        Thread.Sleep(500);
        currentCharIndex = 0;
        DisplayText = "";

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(30);
        timer.Tick += Timer_Tick!;
        timer.Start();
    }
    private void Timer_Tick(object sender, EventArgs e)
    {
        if (currentCharIndex >= Lines.Length - 1)
        {
            timer!.Stop();
            return;
        }
        if (Lines[currentCharIndex] == '\n')
        {
            Thread.Sleep(200);
            DisplayText += Environment.NewLine;
            currentCharIndex++;
        }
        AddCharToDisplay(Lines[currentCharIndex]);
        currentCharIndex++;
    }

    private void AddCharToDisplay(char c)
    {
        string characterString = c.ToString();
        DisplayText += characterString;

        // Add a longer delay for specific characters
        if (charactersWithLongDelay.Contains(characterString))
        {
            Thread.Sleep(200);
        }
        else
        {
            Thread.Sleep(20);
        }
    }
    #endregion
}
