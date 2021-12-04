using UnityEngine;

namespace ExitGames.SportShooting
{
	public class AutoRotation : MonoBehaviour
	{
		public Vector3 rotationSpeed;

		private void Update()
		{
			var newAngle = transform.localRotation.eulerAngles;
			newAngle += rotationSpeed * Time.deltaTime;

			transform.localRotation = Quaternion.Euler(newAngle);
		}
	}
}