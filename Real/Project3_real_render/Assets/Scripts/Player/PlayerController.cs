using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class PlayerController : NetworkBehaviour
{
    public GameObject PPrefab;
    public GameObject[] PPrefabs;
    public GameObject[] infectedPPrefabs;
    public float movementSpeed;
    public Behaviour[] disableOnLoad;

    public Transform[] playerSpawnPoints;
    public Transform[] infectedSpawnPoints;
    

    NetworkLobbyManager lm;
    public ServerManager sm;
    public LobbyScript ls;

    Scene currentScene;
    GameObject myPlayerUnit;


    [SyncVar(hook = "OnIDSet")]
    public int playerID;



    // Start is called before the first frame update
    void Start()
    {


        lm = GameObject.Find("NetworkManager").GetComponent<NetworkLobbyManager>();
        ls = GameObject.Find("ServerStateManager").GetComponent<LobbyScript>();


        if (isServer)
        {
            



        }

        if (isLocalPlayer)
        {



            Debug.Log("PlayerObject Loading");
            movementSpeed = 2;

            //sm = GameObject.Find("ServerStateManager").GetComponent<ServerManager>();


            CmdChangePlayerID();
            CmdIncrementPlayers();

            Debug.Log("I am Player #:" + playerID.ToString());

        }

    }

    void OnIDSet(int ID)
    {
        playerID = ID;
    }

    [Command]
    void CmdChangePlayerID()
    {
        playerID = ls.totalPlayers;
    }

    // Update is called once per frame
    void Update()
    {



        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "InGame" && myPlayerUnit == null && isLocalPlayer && ls.infectedPlayerId !=-100f)
        {
            Debug.Log(currentScene.name);
            CmdSpawnMyPlayer();
        }
        //if (isLocalPlayer)
        //{
        //    Vector3 movement = Vector3.zero;
        //    if (Input.GetKey("d"))
        //    {
                
        //        movement += new Vector3(1, 0, 0);
        //    }

        //    if (Input.GetKey("s"))
        //    {
        //        movement += new Vector3(0, 0, -1);
        //    }

        //    if (Input.GetKey("a"))
        //    {
        //        movement += new Vector3(-1, 0, 0);
        //    }

        //    if (Input.GetKey("w"))
        //    {
        //        movement += new Vector3(0, 0, 1);
        //    }

        //    CmdMove(Vector3.ClampMagnitude(movement, 1) * movementSpeed);
        //}



        if (!isLocalPlayer)
        {
            return;
        }

        if (isLocalPlayer)
        {
            Debug.Log("Local Player Here: " + playerID.ToString());
           // CmdUpdate();
        }

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    CmdJump();
        //}
        
    }

  
    // Ask server to create player object
    [Command]
    void CmdSpawnMyPlayer()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "InGame") return;

        if (connectionToClient.isReady)
        {
            if(myPlayerUnit == null) Spawn();
        }
        else
        {
            StartCoroutine(WaitForReady());
        }
    }

    IEnumerator WaitForReady()
    {
        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }
        if (myPlayerUnit == null)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Debug.Log("SPAWN REACHED");
        Debug.Log("Spawn Infected Player #: " + ls.infectedPlayerId.ToString());
        // You are Infected
        Vector3 position;
        int ridx;
        if (ls.infectedPlayerId == playerID)
        {
            Debug.Log("Spawn Player " + playerID.ToString() + " You are Infected");
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * infectedSpawnPoints.Length);
            position = infectedSpawnPoints[ridx].position;
            ridx = (int) Mathf.Floor(UnityEngine.Random.value * infectedPPrefabs.Length);
            PPrefab = infectedPPrefabs[ridx];
        }
        else
        {
            Debug.Log("Spawn Player " + playerID.ToString() + " You are a Halo");
            // Create Player object on server and sets position
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * playerSpawnPoints.Length);
            position = playerSpawnPoints[ridx].position;
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * PPrefabs.Length);
            PPrefab = PPrefabs[ridx];
        }

       
        GameObject go = Instantiate(PPrefab, position, Quaternion.identity);

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        myPlayerUnit = go;

        // Make server propagate object to clients
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void CmdUpdate()
    {
        if (myPlayerUnit == null) return;

        //myPlayerUnit.GetComponent<PlayerObjectController>().serverUpdate();
    }

    [Command]
    void CmdMove(Vector3 movement)
    {
        if (myPlayerUnit == null) return;

        myPlayerUnit.transform.localPosition += movement;

    }


  


   

    //IEnumerator StartRPCChooseInfectedCall()
    //{
    //    yield return new WaitForSeconds(0.01f);
    //    RpcChooseInfected();
    //}
    //int numPlayersInLobby()
    //{
    //    int count = 0;
    //    for (int i = 0; i < lm.lobbySlots.Length; i++)
    //    {
    //        if (lm.lobbySlots[i] != null) count++;
    //    }
    //    return count;
    //}

    [Command]
    void CmdIncrementPlayers()
    {
        ls.totalPlayers += 1;

    }



}
