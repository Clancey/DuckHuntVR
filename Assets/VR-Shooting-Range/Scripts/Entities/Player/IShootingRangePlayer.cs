using UnityEngine;

namespace ExitGames.SportShooting
{
	public interface IShootingRangePlayer
	{
		GameObject GameObject { get; }

		Transform UiRoot { get; }
		Transform SideUiRoot { get; }
		
		void GameSetup();
		void MenuSetup();
		
		bool GunTriggerPressed(int gunIndex);
		bool AimingLaserPressed(int gunIndex);
        
		Vector3 GetGunPosition(int gunIndex);
		Quaternion GetGunRotation(int gunIndex);
	}
}