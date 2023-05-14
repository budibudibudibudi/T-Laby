using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameState gameState;
    public GameState getGameState() { return gameState; }
    public delegate void OnGameStateChange(GameState state);
    public OnGameStateChange onGameStateChange;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeState(GameState state)
    {
        if (state == gameState)
            return;
        gameState = state;
        onGameStateChange?.Invoke(gameState);
    }
}