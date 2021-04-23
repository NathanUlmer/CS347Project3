using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScript : NetworkBehaviour
{
    public static LobbyScript instance;
    NetworkLobbyManager lm;
    NetworkLobbyPlayer lp;

    [SyncVar(hook = "OnPlayersUpdated")]
    public int numPlayers = 0;

    [SyncVar(hook = "OnInfectedPlayersUpdated")]
    public int numInfectedPlayers = 0;

    [SyncVar(hook = "OnRoundChange")]
    public int roundNum = 0;

    public Text roundtext;

    public Text winText;

    TimeController timecontrol;


    [SyncVar(hook = "OnWinState")]
    public string winMessage;


    [SyncVar(hook = "OnInfectedChange")]
    public float infectedPlayerId = -100f;

    [SyncVar(hook = "OnPlayIncrement")]
    public int totalPlayers = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);


        //PlayerID = numPlayersInLobby();
    }

    // Update is called once per frame
    void Update()
    {

        if(timecontrol == null)
        {
            try
            {
                timecontrol = GameObject.Find("HUD").GetComponent<TimeController>();
                timecontrol.BeginTime();
                Debug.Log("SUCCESS BEGIN TIME");
            }
            catch
            {
                Debug.Log("Fail BEGIN TIME");
            }
        }

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

                if (numPlayers + numInfectedPlayers > 0)
                {
                    if (numInfectedPlayers == 0)
                    {
                        winMessage = "The Normies Have Won!";
                        RpcIncrementRound();
                    }

                    if (numPlayers == 0)
                    {
                        winMessage = "The Infected Have Won!";
                        RpcIncrementRound();
                    }
                }
            }
            Debug.Log("*****Players Count: " + (numPlayers + numInfectedPlayers).ToString());
        }
    }


    //public int numIsInfectedAlive()
    //{
    //    int numAlive = 0;
    //    for(int i = 0; i < players.Count; i++)
    //    {
    //        Debug.Log("Infected Player?: " + players[i].ToString());
    //        if (players[i] == 2)
    //        {
                
    //            numAlive += 1;
    //        }
    //    }
    //    return numAlive;
    //}


    //public int numNormalAlive()
    //{
    //    int numAlive = 0;
    //    for (int i = 0; i < players.Count; i++)
    //    {
    //        if (players[i] == 1)
    //        {
    //            numAlive += 1;
    //        }
    //    }
    //    return numAlive;
    //}


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

    void OnWinState(string state)
    {
        winMessage = state;
    }

    void OnPlayIncrement(int newVar)
    {
        totalPlayers = newVar;
    }

    void OnPlayersUpdated(int np)
    {
        numPlayers = np;
    }

    void OnInfectedPlayersUpdated(int np)
    {
        numInfectedPlayers = np;
    }


    void OnInfectedChange(float newvar)
    {
        infectedPlayerId = newvar;
    }


    void OnRoundChange(int newRound)
    {
        roundNum = newRound;
        //roundtext.text = "Round " + roundNum.ToString();
    }

    [ClientRpc]
    void RpcIncrementRound()
    {
        roundNum += 1;
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

