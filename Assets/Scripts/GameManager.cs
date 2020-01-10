using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion
    #region Manager
    private LevelManager LevelManager;
    private InputManager InputManager;
    private UIManager UIManager;
    #endregion
    #region Init
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitVariables();
    }
    private void InitVariables()
    {
        InputManager = gameObject.AddComponent<InputManager>();
        LevelManager = gameObject.AddComponent<LevelManager>();
        UIManager = gameObject.AddComponent<UIManager>();
    }
    #endregion
    #region Level
    public int Level { get { return LevelManager.Level; } }
    public void LoadLevel(int levelNo)
    {
        LevelManager.LoadLevel(levelNo);
        ChangeState(gameState.LevelStart);
    }
    public void ResetLevel()
    {
        LevelManager.ResetLevel();
    }
    public delegate void StateChangeDel();
    public event StateChangeDel OnLevelAccomplish;
    public event StateChangeDel OnLevelStart;
    #endregion
    #region GameState
    public enum gameState
    {
        LevelStart,
        InLevel,
        LevelAccomplish,
    }
    public gameState GameState { get; private set; }
    public void ChangeState(gameState gameState)
    {
        GameState = gameState;
        switch (GameState)
        {
            case gameState.LevelStart:
                {
                    OnLevelStart?.Invoke();
                }
                break;
            case gameState.LevelAccomplish:
                {
                    OnLevelAccomplish?.Invoke();
                }
                break;
        }
    }
    #endregion
    #region Screen
    public static float ScreenHeight { get { return Screen.height; } }
    public static float ScreenWidth { get { return Screen.width; } }
    #endregion
}
