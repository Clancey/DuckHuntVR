using UnityEngine;

namespace ExitGames.SportShooting
{
	public class VrUi : MonoBehaviour
	{
		public string anchorName;
		
		public bool followRotation = false;
		public bool followLocalPlayer;

		private RectTransform rectTransform;
		private VrAnchor activeAnchor;

		private bool forceUpdate;
		
		void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
		}
		
		void OnEnable()
		{
			activeAnchor = VrAnchor.FindAnchor(anchorName);
			if (activeAnchor == null)
				return;

			forceUpdate = true;
		}

		private void UpdatePosition()
		{
			rectTransform.position = activeAnchor.transform.position;
			if (followRotation)
				rectTransform.rotation = activeAnchor.transform.rotation;
			else
				rectTransform.forward = activeAnchor.transform.forward;
		}

		void LateUpdate()
		{
			if (activeAnchor == null)
				return;
			if (!followLocalPlayer && !forceUpdate)
				return;
			
			UpdatePosition();
			forceUpdate = false;
		}
	}
}