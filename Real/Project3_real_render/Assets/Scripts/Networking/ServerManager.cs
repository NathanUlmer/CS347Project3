using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class ServerManager : NetworkBehaviour
{
    //[SyncVar]
    //public float infectedPlayerId = -100f;

    //NetworkLobbyManager lm;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    if (!isServer) return;
    //    lm = GameObject.Find("NetworkManager").GetComponent<NetworkLobbyManager>();
    //    infectedPlayerId = -1;
    //    DontDestroyOnLoad(this.gameObject);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (!isServer) return;
    //    if(numPlayersInLobby() >= 2 && infectedPlayerId == -1)
    //    {
    //        RpcChooseInfected();
    //    }
    //}

    //[ClientRpc]
    //void RpcChooseInfected()
    //{
    //    infectedPlayerId = Mathf.RoundToInt(UnityEngine.Random.value * lm.lobbySlots.Length);
    //    Debug.Log("Infected Player: " + infectedPlayerId.ToString());
    //}


    //int numPlayersInLobby()
    //{
    //    int count = 0;
    //    for(int i = 0; i < lm.lobbySlots.Length; i++)
    //    {
    //        if (lm.lobbySlots[i] != null) count++;
    //    }
    //    return count;
    //}
}
