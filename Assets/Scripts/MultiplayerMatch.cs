using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class MultiplayerMatch : NetworkManager 
{
    public static MultiplayerMatch networkManager;
    private int roundCount = 1;

    private int iActivePlayer = 0;
    public bool isHost = false;
    private bool gameStarts = false;
    private List<Player> players;
    public List<Gift> gifts;
    public List<Gift> deck;
 
    // Use this for initialization
    public int ActivePlayer
    {
        get { return iActivePlayer; }
    }

    private void Awake()
    {
        if (!networkManager)
            networkManager = this;
        DontDestroyOnLoad(this);
    }
    
    private void RemoveOneCard()
    {
        deck.RemoveAt(deck.Count - 1);
    }

    private void CreateDeck()
    {
        foreach (var gift in gifts)
            for (int i = 0; i < gift.giftCount; i++)
                deck.Add(gift);
    }

    private void ShuffleDeck()
    {
        System.Random rand = new System.Random();

        for (int j = 0; j < 10; j++)
        for (int i = 0; i < deck.Count; i++)
        {
            int r = rand.Next(deck.Count);
    
            Gift gift = deck[r];
            deck[r] = deck[i];
            deck[i] = gift;
        }
    }

    public bool RoundEnd()
    {
        if (players[0].actions == 0 && players[1].actions == 0)
            return true;
        return false;
    }
    
    public bool WinnerSelected()
    {
        throw new System.NotImplementedException();
    }
    
    void Start ()
    {
        //isHost = false;
        players = new List<Player>();
        Debug.Log(isHost.ToString());
        SceneManager.sceneLoaded += OnSceneLoaded;
        networkManager.StartMatchMaker();
    }

    void Update()
    {
        if (players.Count > 0 && !gameStarts && CheckPlayersReady())
        {
            //CheckPlayersReady();
            PrepareGame();
        }
            
        //if (WinnerSelected())
        //    EndMatch();
    }

    private void PrepareGame()
    {
        CreateDeck();
        ShuffleDeck();
        RemoveOneCard();
        gameStarts = true;
    }

    private void EndMatch()
    {
        throw new System.NotImplementedException();
    }
    
    bool CheckPlayersReady()
    {
        bool playersReady = true;
        foreach (var player in players)
            playersReady &= player.ready;

        if (playersReady)
            players[iActivePlayer].StartGame();
        return playersReady;
    }

    public void ReTurn()
    {
        Debug.Log("Turn: " + iActivePlayer);
        players[iActivePlayer].TurnStart();
    }

    public void AlterTurns()
    {
        Debug.Log("Turn: " + iActivePlayer);

        players[iActivePlayer].TurnEnd();
        iActivePlayer = (iActivePlayer + 1) % players.Count;
        players[iActivePlayer].TurnStart();
    }

    public void AddNetworkPlayer(Player player)
    {
        if (players.Count <= 2)
            players.Add(player);
    }

    public void DeregisterPlayer(Player player)
    {
        players.Remove(player);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
            SceneManager.sceneLoaded -= OnSceneLoaded;
        if (networkManager.isHost)
            networkManager.StartHost();
        else networkManager.StartClient();
    }

    public void JoinMatch()
    {
        networkManager.matchMaker.ListMatches(0, 1, String.Empty, 
            true, 0, 0, OnMatchList);
    }

    void OnMatchList(bool success, string extendedinfo, List<MatchInfoSnapshot> matches)
    {
        networkManager.matchMaker.JoinMatch(matches[0].networkId, String.Empty,
            String.Empty, String.Empty, 0, 0, OnMatchJoined);
    }

    void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        SceneManager.LoadScene("GameScene");
    }

    public void CreateMatch() {
        networkManager.matchMaker.CreateMatch ("match", 2, true, 
            string.Empty, string.Empty, string.Empty, 
            0, 0, OnMatchCreate); 
    }
 
    void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo) {
        networkManager.isHost = true;
        SceneManager.LoadScene ("GameScene");
    }
 
}