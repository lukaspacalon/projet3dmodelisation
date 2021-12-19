using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyController : MonoBehaviourPunCallbacks
{

    #region variables
    public static LobbyController lc;
    public GameObject roomListingPrefab;
    public Transform roomsPanel;
    public GameObject connectingBtn, playBtn;
    public TextMeshProUGUI roomSizeTxt; //osef probablement
    public TMP_InputField roomNameIF;

    private string roomName;
    private int roomSize; //osef probablement
    private float masterServerTimer = 6;
    private int sceneNumber = 1;
    #endregion

    #region MonoBehaviour Function
    private void Awake()
    {
        lc = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        roomName = "Room" + Random.Range(1, 99999);
        roomSize = 10;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    private void Update()
    {
        roomSizeTxt.SetText(roomSize.ToString());
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            connectingBtn.SetActive(true);
            playBtn.SetActive(false);
            masterServerTimer = 6;
        }
        else if (PhotonNetwork.IsConnectedAndReady)
        {
            if (masterServerTimer <= 0)
            {
                connectingBtn.SetActive(false);
                playBtn.SetActive(true);
            }
            else
            {
                masterServerTimer -= Time.deltaTime;
            }
        }
    }
    #endregion

    #region Photon Function 
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RemoveRoomListings();
        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    #endregion

    #region
    private void RemoveRoomListings()
    {
        while(roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    private void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }

    public void CreateRoom()
    {
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public void OnRoomNameChanged()
    {
        roomName = roomNameIF.text.ToString();
        print(roomName);
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void RoomSize(int size)
    {
        roomSize = size;
        roomSizeTxt.SetText(roomSize.ToString());
    }

    public void RoomRefresh()
    {
        PhotonNetwork.LeaveLobby();
        JoinLobbyOnClick();
    }
    #endregion


}
