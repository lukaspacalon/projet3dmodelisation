using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PunPlayerManager : MonoBehaviourPunCallbacks
{
    #region Variables
    public static GameObject LocalPlayerInstance;
    private LevelManager lm;
    #endregion

    private void Awake()
    {
        if (photonView.IsMine)
        {
            lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            lm.localPlayer = gameObject;
            gameObject.transform.GetChild(2).gameObject.SetActive(true);

        }
        DontDestroyOnLoad(this.gameObject);
    }

}
