using System.Diagnostics.Metrics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
namespace MathGame
{
	// мега игра от Михаила
	// версия 0.0.4
	public partial class MainWindow : Window
	{
		DispatcherTimer timer = new DispatcherTimer();
		int tenthsOfSecondsElapsed;
		public int counter;
		private float Max = 1000;

		public MainWindow()
		{

			InitializeComponent();

			timer.Interval = TimeSpan.FromSeconds(.1);
			timer.Tick += Timer_Tick;
			SetUpGame();
			this.Loaded += (s, e) => this.Focus();

			// Обработка клавиш для всего окна
			this.PreviewKeyDown += (s, e) =>
			{
				if (e.Key == Key.R)
				{
					SetUpGame();
				}
			};

		}

		
		private void Timer_Tick(object sender, EventArgs e)
		{
			tenthsOfSecondsElapsed++;
			timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0");
			if (Count.Text == "8")
			{
				timer.Stop();

			}
		}
		


		private void SetUpGame()
		{



			foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
			{
				textBlock.Visibility = Visibility.Visible;
			}
			List<string> AnimalsRep = new List<string>()
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
			Random random = new Random();
			int count = 0;
			foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
			{

				int index = random.Next(AnimalsRep.Count);
				string nextEmoji = AnimalsRep[index];
				textBlock.Text = nextEmoji;
				AnimalsRep.RemoveAt(index);
				count++;
				if (count == 16)
				{
					break;
				}
			}
			
			tenthsOfSecondsElapsed = 0;
			Count.Text = "0";
			counter = 0;
		}
		TextBlock lastTextgBlockClicked;
		bool findingMath = false;

		private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			timer.Start();
			TextBlock Count = (TextBlock)mainGrid.FindName("Count");

			TextBlock textBlock = sender as TextBlock;

			if (findingMath == false)
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
		


		private void TimeTextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{

			if (Count.Text == "8")
			{
				TextBlock BestTime = (TextBlock)mainGrid.FindName("BestTime");
				if (float.Parse(timeTextBlock.Text) <= Max)
				{
					Max = float.Parse(timeTextBlock.Text);
					BestTime.Text = Max.ToString();

				}

				timeTextBlock.Text = "0";
				SetUpGame();
			}


		}

		
	}
}