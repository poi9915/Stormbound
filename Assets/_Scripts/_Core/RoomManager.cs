using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;

    public Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connect to Photon Server , waiting ...");
        PhotonNetwork.ConnectUsingSettings();
    }


    // connect vào master server
    // sau đó sẽ tự động join vào lobby
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    // logic join lobby , có thể tạo room hoặc join room (lobby == room)
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby , waiting for room...");
        PhotonNetwork.JoinOrCreateRoom("TestRoom", null, null);
    }

    
    // logic sau khi join room thành công 
    // spawn player prefab tại SpawnPoint
    // và gọi hàm IsLocalPlayer() trong PhotonPlayerSetup để thiết lập local player (player của client đang chạy game )
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room successfully!");
        GameObject _player = PhotonNetwork.Instantiate(PlayerPrefab.name, SpawnPoint.position, Quaternion.identity);
        _player.GetComponent<PhotonPlayerSetup>().IsLocalPlayer();
    }
}