using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinController : MonoBehaviour
{

    LobbyScript ls;
    public static WinController instance;
    public Text winText;
    public string winStr;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ls = GameObject.Find("ServerStateManager").GetComponent<LobbyScript>();
        if (ls != null)
        {
            winStr = ls.winMessage;
            winText.text = winStr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ls!=null)
        {
            winStr = ls.winMessage;
            winText.text = winStr;
        }
    }
}
