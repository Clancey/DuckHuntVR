using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace ExitGames.SportShooting
{
	public class ShootingRangeRifle : MonoBehaviour
	{
		[FormerlySerializedAs("_bulletTrace")]
		public LineRenderer bulletTrace;

		[SerializeField]
		Transform _tip;

		[SerializeField]
		LayerMask _hitLayer;

		[SerializeField]
		float _maxBulletDistance;

		[SerializeField]
		private GameObject aimingLaser;

		RaycastHit _hitInfo;

		private AudioSource _shotSound;

		[Header("Feedback")]
		[SerializeField]
		GameObject muzzleMesh;

		[SerializeField]
		ParticleSystem muzzleFx;

		[SerializeField]
		float decreaseSpeed = 2;

		public IShootingRangePlayer Player { get; set; }
		public int GunIndex { get; set; }

		private bool aimingLaserState = true;
		
		public void Awake()
		{
			_shotSound = GetComponent<AudioSource>();
			bulletTrace.enabled = false;
			SetLaserState(true);
		}

		void Update()
		{
			if (Player == null)
				return;

			if (Player.GunTriggerPressed(GunIndex))
			{
				ShootAttempt();
			}

			if (Player.AimingLaserPressed(GunIndex))
			{
				ToggleAimingLaser();
			}

			transform.position = Player.GetGunPosition(GunIndex);
			transform.rotation = Player.GetGunRotation(GunIndex);
		}

		private void ToggleAimingLaser()
		{
			SetLaserState(!aimingLaserState);
		}

		void SetLaserState(bool state)
		{
			aimingLaserState = state;
			aimingLaser.SetActive(state);
		}
		
		public void ShootAttempt()
		{
			Vector3 finalPosition = new Vector3();
			//Check if we've hit the target
			if (Physics.Raycast(_tip.position, _tip.forward, out _hitInfo, _maxBulletDistance, _hitLayer))
			{
				finalPosition = _hitInfo.transform.position;
				var hittedObject = _hitInfo.collider.GetComponent<Destructible>();
				if (hittedObject != null)
				{
					hittedObject.MarkToDestroy();
				}

				var b = _hitInfo.collider.GetComponent<NonUiButton>();
				if (b != null)
				{
					if (b.CompareTag("EndMatchButton"))
					{
						b.EndMatch();
					}
					else if (b.CompareTag("LogoutButton"))
					{
						b.LeaveMatch();
					}
				}
			}
			else
			{
				finalPosition = _tip.position + _tip.forward * _maxBulletDistance;
			}

			PlayShotFeedback(_tip.position, finalPosition);
		}
		public void PlayShotFeedback(Vector3 position, Vector3 finalPosition)
		{
			var direction = finalPosition - position;
			direction = direction.normalized;
			bulletTrace.SetPosition(0, position);
			bulletTrace.SetPosition(1, finalPosition);

			PlayShotSound();
			StopAllCoroutines();
			StartCoroutine(DisableBulletTrace(direction, position));
		}

		// Disable bullet trace with short timeout to create visual effect
		IEnumerator DisableBulletTrace(Vector3 direction, Vector3 initialPosition)
		{
			bulletTrace.enabled = true;

			muzzleFx.Play();
			muzzleMesh.SetActive(true);
			yield return null;

			Material m = bulletTrace.material;
			Color color = m.GetColor("_TintColor");
			color.a = 1;
			while (color.a >= 0.1f)
			{
				color.a -= Time.deltaTime * decreaseSpeed;
				m.SetColor("_TintColor", color);
				yield return null;
				initialPosition += direction.normalized * decreaseSpeed / 2;
				bulletTrace.SetPosition(0, initialPosition);
				muzzleMesh.SetActive(false);
			}

			color.a = 0;
			m.SetColor("_TintColor", color);
			bulletTrace.enabled = false;
		}

		public void PlayShotSound()
		{
			_shotSound.PlayOneShot(_shotSound.clip);
		}
	}
}