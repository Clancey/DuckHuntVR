using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExitGames.SportShooting
{
    /// <summary>
    /// Component's container for Player
    /// </summary>
    public class ShootingRangePlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform _uiRoot;
        public Transform UIRoot
        {
            get
            {
                return _uiRoot;
            }
        }

        [SerializeField]
        private Transform _sideUIRoot;
        public Transform SideUIRoot
        {
            get
            {
                return _sideUIRoot;
            }
        }

        [SerializeField]
        GameObject _cameraRig;
        public GameObject CameraRig
        {
            get
            {
                return _cameraRig;
            }
        }

        [SerializeField]
        private List<GameObject> _rifles;
        public List<GameObject> Rifles
        {
            get { return _rifles; }      
        }

        [SerializeField]
        private GameObject _laserPointer;
        public GameObject LaserPointer
        {
            get { return _laserPointer; }   
        }
      
        private void OnEnable()
        {
            var vrAnchors = GetComponentsInChildren<VrAnchor>();
            foreach (var vrAnchor in vrAnchors)
                vrAnchor.enabled = true;
        }

        public virtual void SetupForGame()
        {
            
        }
    }
}
