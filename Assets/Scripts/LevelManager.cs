using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Init
    private BoardManager BoardManager;
    void Start()
    {
        BoardManager = gameObject.AddComponent<BoardManager>();
    }
    #endregion
    public int Level { get; private set; } = 0;
    public void LoadLevel(int levelNo)
    {
        Level = levelNo;
        BoardManager.CreateBoardOfLevel(levelNo);
    }
    public void ResetLevel()
    {
        BoardManager.CreateBoardOfLevel(Level);
    }
}
