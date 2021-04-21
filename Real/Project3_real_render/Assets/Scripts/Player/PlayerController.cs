using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : NetworkBehaviour
{
    public GameObject PPrefab;
    public float movementSpeed;
    public Behaviour[] disableOnLoad;
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
        // Create Player object on server and sets position
        Vector3 position = new Vector3(Random.Range(-8.0F, 8.0F), 30, -33);
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
}
