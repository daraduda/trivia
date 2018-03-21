using System;
using System.Collections.Generic;
using System.Linq;
using Trivia.Model;

namespace Trivia
{
	public class Game
	{
		private int currentPlayer = -1;
		private List<Player> players = new List<Player>();
		private Dictionary<QuestionCategory, List<string>> questions = new Dictionary<QuestionCategory, List<string>>();
		private readonly ConsoleColor[] colors = { ConsoleColor.Gray, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red };

		public enum QuestionCategory
		{
			Pop,
			Science,
			Sport,
			Rock
		}

		public Game()
		{
			InitQuestions();
		}

		protected void InitQuestions()
		{
			//TODO: Here we can read questions data from json or xml
			questions.Add(QuestionCategory.Pop, new List<string>());
			questions.Add(QuestionCategory.Science, new List<string>());
			questions.Add(QuestionCategory.Sport, new List<string>());
			questions.Add(QuestionCategory.Rock, new List<string>());

			foreach (var key in questions.Keys)
			{
				for (int i = 0; i < 50; i++)
				{
					questions[key].Add($"{key} Question {i}");
				}
			}
		}

		public Player CurrentPlayer
		{
			get
			{
				return players[currentPlayer];
				
			}
		}

		public bool IsPlayable()
		{
			return HowManyPlayers() >= 2;
		}

		public void AddPlayer(string playerName)
		{
			Player player = new Player();
			player.Name = playerName;
			players.Add(player);

			player.Index = players.IndexOf(player);
			player.Color = player.Index < colors.Length ? colors[player.Index] : ConsoleColor.White;

			Console.WriteLine($"{playerName} was added");
			Console.WriteLine($"They are player number {player.Index + 1}");
		}

		public int HowManyPlayers()
		{
			return players.Count;
		}

		public void SelectNexPlayer()
		{
			currentPlayer++;

			if (currentPlayer == players.Count)
				currentPlayer = 0;
		}

		public void Roll(int roll)
		{
			Player player = players[currentPlayer];
			
			Console.ForegroundColor = player.Color;
			Console.WriteLine($"{player.Name} is the current player");
			Console.WriteLine($"They have rolled a {roll}");

			if (player.IsPenaltyBox)
			{
				if (roll % 2 != 0)
				{
					player.IsGettingOutOfPenaltyBox = true;

					Console.WriteLine($"{player.Name} is getting out of the penalty box");

					player.Place += roll;

					if (player.Place > 11)
						player.Place -= 12;

					QuestionCategory questionCategory = GetCurrentCategory(player.Place);

					Console.WriteLine($"{player.Name}\'s new location is {player.Place}");
					Console.WriteLine($"The category is {questionCategory}");

					AskQuestions(player);
				}
				else
				{
					Console.WriteLine($"{player.Name} is not getting out of the penalty box");

					player.IsGettingOutOfPenaltyBox = false;
				}
			}
			else
			{
				player.Place += roll;

				if (player.Place > 11)
					player.Place -= 12;

				QuestionCategory questionCategory = GetCurrentCategory(player.Place);

				Console.WriteLine($"{player.Name}\'s new location is {player.Place}");
				Console.WriteLine($"The category is {questionCategory}");

				AskQuestions(player);
			}
		}

		public bool Turn(int roll)
		{
			Player player = players[currentPlayer];

			if (player.IsPenaltyBox && !player.IsGettingOutOfPenaltyBox)
			{
				Console.ResetColor();
				return false;
			}

			bool isWinner = roll == 7 ? WrongAnswer(player) : WasCorrectlyAnswered(player);
			Console.ResetColor();

			return isWinner;
		}

		#region private methods
		private void AskQuestions(Player player)
		{
			QuestionCategory category = GetCurrentCategory(player.Place);

			if (questions.Count == 0)
			{
				Console.WriteLine("Questions in this category is over.");
				return;
			}

			string question = questions[category].First();
			questions[category].RemoveAt(0);

			Console.WriteLine(question);
		}

		private QuestionCategory GetCurrentCategory(int place)
		{
			QuestionCategory[] categories =
			{
				QuestionCategory.Pop,
				QuestionCategory.Science,
				QuestionCategory.Sport,
				QuestionCategory.Rock,

				QuestionCategory.Pop,
				QuestionCategory.Science,
				QuestionCategory.Sport,
				QuestionCategory.Rock,

				QuestionCategory.Pop,
				QuestionCategory.Science,
				QuestionCategory.Sport,
				QuestionCategory.Rock,

				QuestionCategory.Pop,
				QuestionCategory.Science,
				QuestionCategory.Sport,
				QuestionCategory.Rock
			};

			return categories[place];
		}

		private bool WasCorrectlyAnswered(Player player)
		{
			bool isWinner = false;

			if (player.IsPenaltyBox)
			{
				if (player.IsGettingOutOfPenaltyBox)
				{
					Console.WriteLine("Answer was correct!");
					player.Purse++;
					Console.WriteLine($"{player.Name} now has {player.Purse} Gold Coins.");

					isWinner = DidPlayerWin(player);
				}

				return isWinner;
			}

			Console.WriteLine("Answer was corrent!");

			player.Purse++;

			Console.WriteLine($"{player.Name} now has {player.Purse} Gold Coins.");

			isWinner = DidPlayerWin(player);

			return isWinner;
		}

		private bool WrongAnswer(Player player)
		{
			Console.WriteLine("Question was incorrectly answered.");
			Console.WriteLine($"{player.Name} was sent to the penalty box");

			player.IsPenaltyBox = true;

			return false;
		}

		private bool DidPlayerWin(Player player)
		{
			return player.Purse >= 6;
		}
		#endregion
	}
}
