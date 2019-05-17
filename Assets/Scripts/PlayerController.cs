using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    SHOOT,
    JUMP
}

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerInputCallback(PlayerAction action, float deg);
    public event PlayerInputCallback OnPlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


