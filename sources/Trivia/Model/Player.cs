using System;

namespace Trivia.Model
{
	/// <summary>
	/// The class player
	/// </summary>
	public class Player
	{
		/// <summary>
		/// The class constructor.
		/// </summary>
		public Player()
		{
			Index = -1;
			Name = string.Empty;
			Place = 0;
			Purse = 0;
			IsPenaltyBox = false;
			IsGettingOutOfPenaltyBox = false;
			Color = ConsoleColor.White;
		}

		/// <summary>
		/// Current index property
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// Gets or sets the name of the player.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the place of the player.
		/// </summary>
		public int Place { get;set; }

		/// <summary>
		/// Gets or sets the purse of the player.
		/// </summary>
		public int Purse { get; set; }

		/// <summary>
		/// Gets or sets the isPenaltyBox of the player.
		/// </summary>
		public bool IsPenaltyBox { get; set; }

		/// <summary>
		/// Gets or sets the isGettingOutOfPenaltyBox of the player.
		/// </summary>
		public bool IsGettingOutOfPenaltyBox { get; set; }

		/// <summary>
		/// Gets or sets the console color of the player.
		/// </summary>
		public ConsoleColor Color { get; set; }
	}
}