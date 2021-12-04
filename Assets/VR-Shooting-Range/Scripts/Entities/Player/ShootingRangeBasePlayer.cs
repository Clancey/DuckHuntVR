using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace ExitGames.SportShooting
{
	public class ShootingRangeBasePlayer : MonoBehaviour, IShootingRangePlayer
	{
		[SerializeField]
		protected GameObject cameraRig;
			
		[SerializeField]
		protected Transform uiRoot;

		[SerializeField]
		protected Transform sideUiRoot;

		[SerializeField]
		protected int numberOfGuns = 1;

		[SerializeField]
		protected GameObject speakingFeedback;

		public GameObject GunPrefab;

		protected List<ShootingRangeRifle> rifles = new List<ShootingRangeRifle>();

		public virtual GameObject GameObject
		{
			get { return gameObject; }
		}

		public virtual Transform UiRoot
		{
			get { return uiRoot; }
		}

		public virtual Transform SideUiRoot
		{
			get { return sideUiRoot; }
		}

		public void Start()
        {
			for (int i = 0; i < numberOfGuns; i++)
			{
				var rifle = Instantiate(GunPrefab, transform.position, transform.rotation)
				.GetComponent<ShootingRangeRifle>();

				rifle.Player = this;
				rifle.GunIndex = i;
				rifles.Add(rifle);
			}
        }
		
		public void OnDestroy()
        {
			foreach (var rifle in rifles)
				Destroy(rifle);

		}
		
		public virtual void GameSetup()
		{
			UiRoot.gameObject.SetActive(true);
			SideUiRoot.gameObject.SetActive(true);
			
			foreach (var rifle in rifles)
				rifle.gameObject.SetActive(true);

			cameraRig.SetActive(true);
			
		}

		public virtual void MenuSetup()
		{
			UiRoot.gameObject.SetActive(true);
			SideUiRoot.gameObject.SetActive(true);

			foreach (var rifle in rifles)
				rifle.gameObject.SetActive(false);

			cameraRig.SetActive(true);
		}

		public virtual bool GunTriggerPressed(int gunIndex)
		{
			return false;
		}

		public virtual bool AimingLaserPressed(int gunIndex)
		{
			return false;
		}

		public virtual Vector3 GetGunPosition(int gunIndex)
		{
			return transform.position;
		}

		public virtual Quaternion GetGunRotation(int gunIndex)
		{
			return Quaternion.identity;
		}

		public virtual List<ShootingRangeRifle> GetRifles()
		{
			return rifles;
		}
	}
}