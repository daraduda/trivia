using System;

namespace Trivia
{
	public class GameRunner
	{
		public static void Main(string[] args)
		{
			Random random = new Random();

			Game game = new Game();
			game.AddPlayer("Chet");
			game.AddPlayer("Pat");
			game.AddPlayer("Sue");

			try
			{
				if (game.IsPlayable())
				{
					Console.WriteLine("Start the game!\r\n");
					
					bool isWinner = false;

					do
					{
						game.SelectNexPlayer();

						int roll = random.Next(5) + 1;
						game.Roll(roll);

						int turn = random.Next(9);
						isWinner = game.Turn(turn);

						WinnerShow(isWinner, game);

					} while (!isWinner);
				}
				else
				{
					Console.WriteLine("The game is not playable, because not enough players.");
				}
			}
			catch (Exception ex)
			{
				//Here we can define logger (Log4net or NLog) functionality.
				Console.WriteLine("The GameRunner throwed exception {0}", ex);
			}

			// Keep the console window open in debug mode.
			Console.WriteLine("-----------------------");
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}

		private static void WinnerShow(bool isWinner, Game game)
		{
			if (isWinner)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\r\n");
				Console.WriteLine($"The winner is {game.CurrentPlayer.Name}!");
				Console.WriteLine("\r\n");
				Console.ResetColor();
			}
		}
	}
}

