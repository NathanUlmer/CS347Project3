using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : NetworkBehaviour
{
    public GameObject PPrefab;
    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer == false)
        {
            // this object is not mine
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
        //    if (Input.GetKeyDown("w"))
        //    {
        //        movement += new Vector3(1, 0, 0);
        //    }

        //    if (Input.GetKeyDown("a"))
        //    {
        //        movement += new Vector3(0, 0, -1);
        //    }

        //    if (Input.GetKeyDown("s"))
        //    {
        //        movement += new Vector3(-1, 0, 0);
        //    }

        //    if (Input.GetKeyDown("d"))
        //    {
        //        movement += new Vector3(0, 0, 1);
        //    }

        //    transform.position += Vector3.ClampMagnitude(movement, 1) * movementSpeed;
        //}


        if(!isLocalPlayer)
        {
            return;
        }

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
        // Create Player object on server and sets position
        Vector3 position = new Vector3(Random.Range(-8.0F, 8.0F), 21, -33);
        GameObject go = Instantiate(PPrefab, position, Quaternion.identity);

        //go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        myPlayerUnit = go;

        // Make server propagate object to clients
        NetworkServer.SpawnWithClientAuthority(go,connectionToClient);
    }

    [Command]
    void CmdJump()
    {
        if (myPlayerUnit == null) return;

        myPlayerUnit.transform.Translate(0, 1, 0);
    }
}
