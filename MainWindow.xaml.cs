using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MathGame;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private readonly DispatcherTimer timer = new();
	private int tenthsOfSecondsElapsed;
	public int counter;
	private float Max = float.MaxValue;

	public MainWindow()
	{
		InitializeComponent();

		timer.Interval = TimeSpan.FromSeconds(.1);
		timer.Tick += Timer_Tick;
		SetUpGame();
	}

	private void Timer_Tick(object sender, EventArgs e)
	{
		tenthsOfSecondsElapsed++;
		timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0");
		if (Count.Text == "8") timer.Stop();
	}


	private void SetUpGame()
	{
		foreach (var textBlock in mainGrid.Children.OfType<TextBlock>()) textBlock.Visibility = Visibility.Visible;
		var AnimalsRep = new List<string>
		{
			"🐵", "🐵",
			"🦏", "🦏",
			"🦊", "🦊",
			"🐺", "🐺",
			"🐗", "🐗",
			"🐁", "🐁",
			"🐘", "🐘",
			"🐑", "🐑"
		};
		var random = new Random();
		var count = 0;
		foreach (var textBlock in mainGrid.Children.OfType<TextBlock>())
		{
			var index = random.Next(AnimalsRep.Count);
			var nextEmoji = AnimalsRep[index];
			textBlock.Text = nextEmoji;
			AnimalsRep.RemoveAt(index);
			count++;
			if (count == 16) break;
		}

		timer.Start();
		tenthsOfSecondsElapsed = 0;
		Count.Text = "0";
		counter = 0;
	}

	private TextBlock lastTextgBlockClicked;
	private bool findingMath;

	private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
	{
		var Count = (TextBlock)mainGrid.FindName("Count");

		var textBlock = sender as TextBlock;

		if (!findingMath)
		{
			textBlock.Visibility = Visibility.Hidden;
			lastTextgBlockClicked = textBlock;
			findingMath = true;
		}
		else if (textBlock.Text == lastTextgBlockClicked.Text)
		{
			textBlock.Visibility = Visibility.Hidden;
			findingMath = false;
			counter++;
			Count.Text = counter.ToString();
		}
		else
		{
			lastTextgBlockClicked.Visibility = Visibility.Visible;
			findingMath = false;
		}
	}

	private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (Count.Text == "8")
		{
			var BestTime = (TextBlock)mainGrid.FindName("BestTime");
			if (float.Parse(timeTextBlock.Text) <= Max)
			{
				Max = float.Parse(timeTextBlock.Text);
				BestTime.Text = Max.ToString();
			}

			SetUpGame();
		}
	}
}