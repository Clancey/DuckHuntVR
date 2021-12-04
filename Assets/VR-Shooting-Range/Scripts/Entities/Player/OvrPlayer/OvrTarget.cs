using UnityEngine;

public class OvrTarget : MonoBehaviour
{
	[SerializeField]
	private GameObject _anchor;
    
	private float _maxDistance = 20f;
    
	// Update is called once per frame
	void Update ()
	{
		transform.position = _anchor.transform.position;
	}

}