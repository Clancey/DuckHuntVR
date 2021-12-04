using System.Collections.Generic;
using UnityEngine;

namespace ExitGames.SportShooting
{
	public class VrAnchor : MonoBehaviour
	{
		private static Dictionary<string, VrAnchor> anchors = new Dictionary<string, VrAnchor>();
		
		public string anchorName;

		private void OnEnable()
		{
			anchors[anchorName] = this;
		}

		void OnDisable()
		{
			anchors[anchorName] = null;
		}

		public static VrAnchor FindAnchor(string s)
		{
			return anchors[s];
		}
	}
}