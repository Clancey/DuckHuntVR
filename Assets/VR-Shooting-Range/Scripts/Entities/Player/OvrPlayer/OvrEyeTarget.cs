using UnityEngine;
using System.Collections;

public class OvrEyeTarget : MonoBehaviour 
{
    [SerializeField]
    private GameObject _leftHandAnchor;
    [SerializeField]
    private GameObject _centerEyeAnchor;
    [SerializeField]
    private GameObject _rightHandAnchor;
    
    private float _maxDistance = 20f;
    
    // Update is called once per frame
    void Update ()
    {
        var targetTransform = _centerEyeAnchor.transform;

        //if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
        //{
        //    targetTransform = _leftHandAnchor.transform;
        //}
        //else if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        //{
        //    targetTransform = _rightHandAnchor.transform;
        //}
        
        //transform.SetParent(targetTransform);
        //transform.position = targetTransform.position + targetTransform.forward * _maxDistance;
    }
}
