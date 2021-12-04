using UnityEngine;
namespace ExitGames.SportShooting
{
	public class ShootingRangeRift : ShootingRangeBasePlayer
	{
		[Header("Player Specific")]
		public Transform leftHandPivot;
		public Transform rightHandPivot;

		public OvrCursor menuCursor;

		public override void GameSetup()
		{
			base.GameSetup();
			menuCursor.gameObject.SetActive(false);
		}

		public override void MenuSetup()
		{
			base.MenuSetup();
			menuCursor.gameObject.SetActive(true);
		}

		public override bool GunTriggerPressed(int gunIndex)
		{
			return false;
			//return OVRInput.GetDown(gunIndex == 0 ? OVRInput.Button.PrimaryIndexTrigger : OVRInput.Button.SecondaryIndexTrigger);
		}

		public override bool AimingLaserPressed(int gunIndex)
		{
			return false;
			//return OVRInput.GetDown(gunIndex == 0 ? OVRInput.Button.PrimaryHandTrigger : OVRInput.Button.SecondaryHandTrigger);
		}

		public override Vector3 GetGunPosition(int gunIndex)
		{
			return gunIndex == 0 ? leftHandPivot.position : rightHandPivot.position;
		}

		public override Quaternion GetGunRotation(int gunIndex)
		{
			return gunIndex == 0 ? leftHandPivot.rotation: rightHandPivot.rotation;
		}
	}
}