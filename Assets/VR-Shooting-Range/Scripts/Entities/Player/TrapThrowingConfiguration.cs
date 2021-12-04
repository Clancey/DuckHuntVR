using UnityEngine;

namespace ExitGames.SportShooting
{
	[CreateAssetMenu(menuName = "Vr Shooting Range/Throw Configuration")]
	public class TrapThrowingConfiguration : ScriptableObject
	{
		[SerializeField]
		float _minTrapForce;

		[SerializeField]
		float _maxTrapFoce;

		[SerializeField]
		float _minXAngle;

		[SerializeField]
		float _maxXAngle;

		[SerializeField]
		float _minYAngle;

		[SerializeField]
		float _maxYAngle;

		[SerializeField]
		public bool throwForward;

		[SerializeField]
		public Vector3 throwDirection;
		
		public float RandomTrapForce
		{
			get
			{
				return Random.Range(_minTrapForce, _maxTrapFoce);
			}
		}

		public float RandomTrapXAngle
		{
			get
			{
				return Random.Range(_minXAngle, _maxXAngle);
			}
		}
		
		public float RandomTrapYAngle
		{
			get
			{
				return Random.Range(_minYAngle, _maxYAngle);
			}
		}
	}
}