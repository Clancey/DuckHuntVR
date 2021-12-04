using UnityEngine;
using System.Collections;

namespace ExitGames.SportShooting
{
	public class OvrShooter : MonoBehaviour
	{
		public enum Hand
		{
			Any,
			Left,
			Right
		}
		
		[SerializeField]
		LineRenderer _bulletTrace;

		[SerializeField]
		Transform _tip;

		[SerializeField]
		LayerMask _hitLayer;

		[SerializeField]
		float _maxBulletDistance;

		//[SerializeField]
		//OVRCameraRig _cameraRig;

		RaycastHit _hitInfo;

		private AudioSource _shotSound;

		[Header("Feedback")]
		[SerializeField]
		GameObject muzzleMesh;

		[SerializeField]
		ParticleSystem muzzleFx;

		[SerializeField]
		float decreaseSpeed = 2;

		[SerializeField]
		private Hand controllerHand;
		
		void Awake()
		{
			_shotSound = GetComponent<AudioSource>();
			_bulletTrace.enabled = false;
		}

		void Update()
		{
			//var inputFlag = OVRInput.Button.One;
			
			//if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote) ||
			//    OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
			//	inputFlag = OVRInput.Button.PrimaryIndexTrigger;

			//if (controllerHand == Hand.Left && OVRInput.IsControllerConnected(OVRInput.Controller.LTouch))
			//	inputFlag = OVRInput.Button.PrimaryIndexTrigger;

			//if (controllerHand == Hand.Right && OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
			//	inputFlag = OVRInput.Button.SecondaryIndexTrigger;
			
			//if (_photonView.IsMine && OVRInput.GetDown(inputFlag))
			//{
			//	ShootAttempt(null, new ClickedEventArgs());
			//}
		}

		void ShootAttempt(object sender)
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

					return;
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
			_bulletTrace.SetPosition(0, position);
			_bulletTrace.SetPosition(1, finalPosition);

			PlayShotSound();
			StopAllCoroutines();
			StartCoroutine(DisableBulletTrace(direction, position));
		}

		public void PlayShotSound()
		{
			_shotSound.PlayOneShot(_shotSound.clip);
		}

		// Disable bullet trace with short timeout to create visual effect
		IEnumerator DisableBulletTrace(Vector3 direction, Vector3 initialPosition)
		{
			_bulletTrace.enabled = true;

			muzzleFx.Play();
			muzzleMesh.SetActive(true);
			yield return null;
			muzzleMesh.SetActive(false);

			Material m = _bulletTrace.material;
			Color color = m.GetColor("_TintColor");
			color.a = 1;

			while (color.a >= 0.1f)
			{
				color.a -= Time.deltaTime * decreaseSpeed;
				m.SetColor("_TintColor", color);

				yield return null;

				initialPosition += direction.normalized * decreaseSpeed / 2;
				_bulletTrace.SetPosition(0, initialPosition);
			}

			color.a = 0;
			m.SetColor("_TintColor", color);
			_bulletTrace.enabled = false;
		}
	}
}