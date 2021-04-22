using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class LobbyScript : NetworkBehaviour
{
    NetworkLobbyManager lm;
    NetworkLobbyPlayer lp; 
    // Start is called before the first frame update
    void Start()
    {
        lm = GetComponent<NetworkLobbyManager>();
        lp = lm.lobbyPlayerPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if(lm.lobbySlots.Length >=4)
        {
            
        }
    }
}
