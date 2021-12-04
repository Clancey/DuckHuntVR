using UnityEngine;
using ExitGames.SportShooting;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private int spawnpointId;

    public void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.grey;
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        GetComponent<MeshRenderer>().material.color = PlayerFactory.GetColor(spawnpointId);
    }

    public void RestoreDefaults()
    {
        GetComponent<MeshRenderer>().material.color = Color.grey;
    }
}