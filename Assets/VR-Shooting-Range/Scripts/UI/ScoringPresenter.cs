using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ExitGames.SportShooting
{
    public class ScoringPresenter : MonoBehaviour
    {
        [SerializeField]
        Text[] _idFields;

        [SerializeField]
        Text[] _scoreFields;

        [SerializeField]
        private Text microphoneIndicator;
        
        void OnEnable()
        {

        }

        void OnDisable()
        {

        }

        void Start()
        {
            UpdateScore();


        }

        void Update()
        {
        }
        
        void UpdateScore()
        {
            ClearTable(_idFields);
            ClearTable(_scoreFields);
            
            //foreach(Player player in PhotonNetwork.PlayerList)
            //{
            //    object positionObj = player.CustomProperties["position"];
            //    if (positionObj != null)
            //    {
            //        int position = (int)positionObj;
                    
            //        _idFields[position].color = PlayerFactory.GetColor(position);
            //        _idFields[position].text = player.CustomProperties["name"].ToString();

            //        if (player.IsLocal)
            //        {
            //            _idFields[position].text += " (YOU)";
            //        }

            //        _scoreFields[position].color = PlayerFactory.GetColor(position);
            //        _scoreFields[position].text = player.CustomProperties["roundScore"].ToString();
            //    }
            //}
        }

        void ClearTable(Text[] table)
        {
            for(int i = 0; i < table.Length; i++)
            {
                table[i].text = "";
            }
        }
    }
}
