using UnityEngine;

namespace ExitGames.SportShooting
{
    //[RequireComponent(typeof(PhotonView))]
    public class Holder : MonoBehaviour
    {      
        protected virtual  void Awake()
        {
            //var photonView = GetComponent<PhotonView>();
            //if (!photonView.IsMine)
            //{
            //    Destroy(this);
            //}
        }
    }
}
