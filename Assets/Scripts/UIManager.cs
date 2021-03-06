﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Canvas
    private Canvas Canvas;
    #endregion
    #region ToNextLevelButton
    private Button ToNextLevelButtonPrefab;
    private Button ToNextLevelButton;
    private Text ToNextLevelButtonText;
    private static string ToNextLevelTextIfLevel0 = "开始";
    private static string ToNextLeveLTextIfLevelNot0 = "下一关";
    #endregion
    #region ResetLevelButton
    private Button ResetLevelButtonPrefab;
    private Button ResetLevelButton;
    #endregion
    private static string UIPrefabPath = "Prefabs";
    void Start()
    {
        InitCanvas();
        InitToNextLevelButton();
        InitResetLevelButton();
    }
    private void InitCanvas()
    {
        var CanvasObject = new GameObject();
        CanvasObject.name = "Canvas";
        Canvas = CanvasObject.AddComponent<Canvas>();
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasObject.AddComponent<CanvasScaler>();
        CanvasObject.AddComponent<GraphicRaycaster>();
    }
    private void InitToNextLevelButton()
    {
        ToNextLevelButtonPrefab = Resources.Load<Button>(UIPrefabPath + "/" + "ToNextLevelButton");
        ToNextLevelButton = Instantiate(ToNextLevelButtonPrefab, Canvas.transform);
        ToNextLevelButtonText = ToNextLevelButton.transform.Find("Text").gameObject.GetComponent<Text>();

        ToNextLevelButton.onClick.AddListener(LoadNextLevel);

        GameManager.Instance.OnLevelAccomplish += ShowToNextButton;
    }
    private void InitResetLevelButton()
    {
        ResetLevelButtonPrefab = Resources.Load<Button>(UIPrefabPath + "/" + "ResetLevelButton");
        ResetLevelButton = Instantiate(ResetLevelButtonPrefab, Canvas.transform);

        ResetLevelButton.onClick.AddListener(ResetLevel);
        HideResetLevelButton();

        GameManager.Instance.OnLevelStart += ShowResetLevelButton;
        GameManager.Instance.OnLevelAccomplish += HideResetLevelButton;
    }
    private void SetToNextLevelText()
    {
        ToNextLevelButtonText.text = Level == 0 ? ToNextLevelTextIfLevel0 : ToNextLeveLTextIfLevelNot0;
    }
    private void LoadNextLevel()
    {
        GameManager.Instance.LoadLevel(Level + 1);
        ToNextLevelButton.gameObject.SetActive(false);
    }
    private void ResetLevel()
    {
        GameManager.Instance.ResetLevel();
    }
    public void ShowToNextButton()
    {
        ToNextLevelButton.gameObject.SetActive(true);
        SetToNextLevelText();
    }
    public void HideResetLevelButton()
    {
        ResetLevelButton.gameObject.SetActive(false);
    }
    public void ShowResetLevelButton()
    {
        ResetLevelButton.gameObject.SetActive(true);
    }
    private int Level { get { return GameManager.Instance.Level; } }
}
