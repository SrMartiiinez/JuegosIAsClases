using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public FirstPersonMovement movement;

    public GameOver gameOver;

    void Start()
    {
        movement = GetComponent<FirstPersonMovement>();
        gameOver = FindObjectOfType<GameOver>();
    }

    public void GameOver()
    {
        if (!gameOver.isGameOver)
        {
            movement.enabled = false;
            gameOver.IsGameOver();
        }
    }
}