using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviourPunCallbacks
{
    public static LevelManager lm;
    [SerializeField] private GameObject player;

    internal GameObject localPlayer;
    private GameObject playerPrefab;
    public List<Transform> spawnPoints = new List<Transform>();



    private void Start()
    {
        lm = this;
        playerPrefab = player;

        if(PunPlayerManager.LocalPlayerInstance == null)
        {
            int spIndex = Random.Range(0, spawnPoints.Count);
            var p = PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPoints[spIndex].position, Quaternion.identity, 0);
        }
    }
}
