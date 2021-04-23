using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundController : MonoBehaviour
{
    LobbyScript ls;
    public static RoundController instance;
    public Text roundCounterText;
    public string roundStr;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ls = GameObject.Find("ServerStateManager").GetComponent<LobbyScript>();
        if(ls != null)
        {
            roundStr = "Round: " + ls.roundNum.ToString();
            roundCounterText.text = roundStr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ls != null)
        {
            roundStr = "Round: " + ls.roundNum.ToString();
            roundCounterText.text = roundStr;
        }
    }
}
