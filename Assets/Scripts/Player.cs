using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    //[SyncVar(hook = "OnTurnChange")] 
    public bool isTurn = false;

    //[SyncVar(hook = "UpdateTimeDisplay")] 
    public float time = 100;

    public PlayerController playerController;
    
    public List<GameObject> hand;

    [SyncVar]
    public bool ready = false;
    public int actions;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("player created");
    }

    // Update is called once per frame
    [Server]
    void Update()
    {
        if (isTurn)
        {
            time -= Time.deltaTime;
            if (time <= 0)
                MultiplayerMatch.networkManager.AlterTurns();
        }
    }

    void OnPlayerInput(PlayerAction action, float amount)
    {
        
    }
    
    public override void OnStartClient()
    {
        DontDestroyOnLoad(this);
        base.OnStartClient();
        Debug.Log("Client Network Player start");
        StartPlayer();
        
        MultiplayerMatch.networkManager.AddNetworkPlayer(this);
    }
    

    private void StartPlayer()
    {
        ready = true;
    }

    public void StartGame()
    {
        TurnStart();
    }

    public void TurnStart()
    {
        isTurn = true;
        time = 90;
    }

    public void TurnEnd()
    {
        throw new System.NotImplementedException();
    }
}
