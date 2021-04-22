using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class LobbyScript : NetworkBehaviour
{
    NetworkLobbyManager lm;
    NetworkLobbyPlayer lp;


    [SyncVar(hook = "OnInfectedChange")]
    public float infectedPlayerId = -100f;

    [SyncVar(hook = "OnPlayIncrement")]
    public int totalPlayers = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        //PlayerID = numPlayersInLobby();
    }

    // Update is called once per frame
    void Update()
    {
        if (lm == null)
        {
            lm = GameObject.Find("NetworkManager").GetComponent<NetworkLobbyManager>();
            if(lm != null) lp = lm.lobbyPlayerPrefab;
        }
        else
        {


            if (isServer)
            {
                if (infectedPlayerId == -100f)
                {
                    if (numPlayersInLobby() >= 2)
                    {
                        RpcChooseInfected();
                        //StartCoroutine(StartRPCChooseInfectedCall());
                    }

                }
            }
        }
    }



   public  int numPlayersInLobby()
    {
        if (lm == null) return 0;
        int count = 0;
        for (int i = 0; i < lm.lobbySlots.Length; i++)
        {
            if (lm.lobbySlots[i] != null) count++;
        }
        return count;
    }



    void OnPlayIncrement(int newVar)
    {
        totalPlayers = newVar;
    }



    void OnInfectedChange(float newvar)
    {
        infectedPlayerId = newvar;
    }




    [ClientRpc]
    void RpcChooseInfected()
    {
        Debug.Log("CHOOSING INFECTED----------------");
        if (infectedPlayerId == -100f)
        {
            infectedPlayerId = Mathf.Floor(UnityEngine.Random.value * numPlayersInLobby());
            Debug.Log("Infected Player: " + infectedPlayerId.ToString());
            //Debug.Log("Server?: " + isServer);
        }
        //RpcChooseInfected(infectedPlayerId);
    }
}

