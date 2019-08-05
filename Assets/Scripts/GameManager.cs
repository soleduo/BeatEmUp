using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public static PlayerController playerController;

    private void Awake()
    {
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
    }

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 30;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Character GetPlayer()
    {
        return playerController.player;
    }
}
