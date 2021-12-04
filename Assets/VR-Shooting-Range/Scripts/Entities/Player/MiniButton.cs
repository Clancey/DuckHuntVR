namespace ExitGames.SportShooting
{
	public class MiniButton
	{
		public bool Pressed
		{
			set
			{
				previous = pressed;
				pressed = value;
			}
		}

		private bool pressed = false;
		private bool previous = false;

		public bool IsDown()
		{
			return pressed && !previous;
		}

		public bool IsUp()
		{
			return !pressed && previous;
		}
	}
}