using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : NetworkBehaviour
{
    public GameObject PPrefab;
    public GameObject[] PPrefabs;
    public GameObject[] infectedPPrefabs;
    public float movementSpeed;
    public Behaviour[] disableOnLoad;
    private int playerID;

    NetworkLobbyManager lm;
    public ServerManager sm;


    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer == false)
        {
            // this object is not mine
            for(int i = 0; i< disableOnLoad.Length;i++)
            {
                disableOnLoad[i].enabled = false;
            }
            return;
        }
        Debug.Log("PlayerObject Loading");
        movementSpeed = 2;
        lm = GameObject.Find("NetworkManager").GetComponent<NetworkLobbyManager>();
        //sm = GameObject.Find("ServerStateManager").GetComponent<ServerManager>();
        playerID = numPlayersInLobby();
        Debug.Log("I am Player #:" + playerID.ToString());
        CmdSpawnMyPlayer();
    }

    // Update is called once per frame
    void Update()
    {

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

        Debug.Log("Local Player Here: " + playerID.ToString());
        CmdUpdate();

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    CmdJump();
        //}
        
    }

    GameObject myPlayerUnit;

    // Ask server to create player object
    [Command]
    void CmdSpawnMyPlayer()
    {
        if (connectionToClient.isReady)
        {
            Spawn();
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
        Spawn();
    }

    void Spawn()
    {
        Debug.Log("SPAWN REACHED");
        Debug.Log("Infected Player #: " + sm.infectedPlayerId.ToString());
        // You are Infected
        Vector3 position;
        if (sm.infectedPlayerId == playerControllerId)
        {
            Debug.Log("Player " + playerID.ToString() + " You are Infected");
            position = new Vector3(Random.Range(-8.0F, 8.0F), 80, -33);
            PPrefab = infectedPPrefabs[0];
        }
        else
        {
            Debug.Log("Player " + playerID.ToString() + " You are a Halo");
            // Create Player object on server and sets position
            position = new Vector3(Random.Range(-8.0F, 8.0F), 30, -33);
            PPrefab = PPrefabs[0];
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


    int numPlayersInLobby()
    {
        int count = 0;
        for (int i = 0; i < lm.lobbySlots.Length; i++)
        {
            if (lm.lobbySlots[i] != null) count++;
        }
        return count;
    }
}
