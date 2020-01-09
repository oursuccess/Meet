using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    #region Script
    private LevelScript LevelScript;
    public const string WallScript = "WALL";
    public const string FloorScript = "FLOOR";
    public const string ExitScript = "EXIT";
    public const string BoxScript = "BOX";
    public const string CharacterScript = "CHARACTER";
    public const string HorizontalCharacterScript = "HORIZONTAL" + CharacterScript;
    public const string VerticalCharacterScript = "VERTICAL" + CharacterScript;
    public const string NoGrid = "NOGRID";
    #endregion
    #region Prefab
    private GameObject WallPrefab;
    private GameObject FloorPrefab;
    private GameObject ExitPrefab;
    private GameObject CharacterPrefab;
    private GameObject BoxPrefab;
    private GameObject GridPrefab;
    private GameObject HorizontalCharacterPrefab;
    private GameObject VerticalCharacterPrefab;
    private void InitPrefabs()
    {
        string PrefabPath = "Prefabs";
        WallPrefab = Resources.Load<GameObject>(PrefabPath + "/Wall");
        FloorPrefab = Resources.Load<GameObject>(PrefabPath + "/Floor");
        ExitPrefab = Resources.Load<GameObject>(PrefabPath + "/Exit");
        CharacterPrefab = Resources.Load<GameObject>(PrefabPath + "/Character");
        BoxPrefab = Resources.Load<GameObject>(PrefabPath + "/Box");
        GridPrefab = Resources.Load<GameObject>(PrefabPath + "/Grid");
        HorizontalCharacterPrefab = Resources.Load<GameObject>(PrefabPath + "/HorizontalCharacter");
        VerticalCharacterPrefab = Resources.Load<GameObject>(PrefabPath + "/VerticalCharacter");
    }
 #endregion
    #region GridsNElements
    public List<List<Grid>> Grids { get; private set; }
    private List<CharacterBase> Characters;
    #endregion
    void Start()
    {
        InitPrefabs();
    }
    private void InitPrevBoard()
    {
        if (Grids != null)
        {
            foreach(var GridRow in Grids)
            {
                foreach(var Grid in GridRow)
                {
                    if (Grid.Element != null) RemoveElement(Grid.Element);
                    Destroy(Grid.gameObject);
                }
            }
        }
        Grids = new List<List<Grid>>();
        Characters = new List<CharacterBase>();
    }
    private void RemoveElement(Element element)
    {
        Grids[element.PositionInGrid.y][element.PositionInGrid.x].Element = null;
        Destroy(element.gameObject);
    }
    public void CreateBoardOfLevel(int levelNo)
    {
        InitPrevBoard();
        LevelScript = new LevelScript(levelNo);
        var Scripts = LevelScript.LevelScripts;
        var RowCount = Scripts.Count;
        var ColMaxCount = Scripts[0].Count;
        foreach(var Row in Scripts)
        {
            if (ColMaxCount < Row.Count) ColMaxCount = Row.Count;
        }
        var yBegin = (Camera.main.ScreenToWorldPoint(new Vector2(0, GameManager.ScreenHeight / 2)).y + RowCount) / 2;
        var xBegin = (Camera.main.ScreenToWorldPoint(new Vector2(GameManager.ScreenWidth / 2, 0)).x - ColMaxCount) / 2;
        for(var y = 0; y != RowCount; ++y)
        {
            var ColCount = Scripts[y].Count;
            Grid[] GridCol = new Grid[ColCount];
            for(var x = 0; x != ColCount; ++x)
            {
                Grid Grid = null;
                if(Scripts[y][x] != NoGrid)
                {
                    var GridObject = Instantiate(GridPrefab, new Vector3(xBegin + x, yBegin - y), Quaternion.identity);
                    Grid = GridObject.GetComponent<Grid>();
                    if (!string.IsNullOrEmpty(Scripts[y][x]))
                    {
                        GameObject ElementPrefab = null;
                        GameObject GroundPrefab = null;
                        switch (Scripts[y][x])
                        {
                            case BoxScript:
                                {
                                    ElementPrefab = BoxPrefab;
                                }
                                break;
                            case CharacterScript:
                                {
                                    ElementPrefab = CharacterPrefab;
                                }
                                break;
                            case HorizontalCharacterScript:
                                {
                                    ElementPrefab = HorizontalCharacterPrefab;
                                }
                                break;
                            case VerticalCharacterScript:
                                {
                                    ElementPrefab = VerticalCharacterPrefab;
                                }
                                break;
                            case FloorScript:
                                {
                                    GroundPrefab = FloorPrefab;
                                }
                                break;
                            case ExitScript:
                                {
                                    GroundPrefab = ExitPrefab;
                                }
                                break;
                            case WallScript:
                                {
                                    GroundPrefab = WallPrefab;
                                }
                                break;
                        }
                        if(GroundPrefab != null)
                        {
                            var GroundObject = Instantiate(GroundPrefab, new Vector3(xBegin + x, yBegin - y), Quaternion.identity);
                            var Ground = GroundObject.GetComponent<Ground>();
                            Grid.Ground = Ground;
                        }
                        if(ElementPrefab != null)
                        {
                            var ElementObject = Instantiate(ElementPrefab, new Vector3(xBegin + x, yBegin - y), Quaternion.identity);
                            var Element = ElementObject.GetComponent<Element>();
                            Element.Board = this;
                            Element.SetPosition(new Element.Position(x, y));
                            Grid.Element = Element;

                            if(Element is CharacterBase character)
                            {
                                Characters.Add(character);
                            }
                        }
                    }
                }
                GridCol[x] = Grid; 
            }
            Grids.Add(new List<Grid>(GridCol));
        }
    }
    public bool CanMoveTo(Element element, int xChange, int yChange)
    {
        bool CanMove = false;
        int yBase = element.PositionInGrid.y, yTarget = yBase - yChange;
        int xBase = element.PositionInGrid.x, xTarget = xBase + xChange;
        if(Grids[yBase][xBase].Element == element && 0 <= yTarget && yTarget < Grids.Count && 0 <= xTarget && xTarget < Grids[yTarget].Count)
        {
            var elementInTargetGrid = Grids[yTarget][xTarget].Element;
            var groundInTargetGrid = Grids[yTarget][xTarget].Ground;
            if ((elementInTargetGrid == null || elementInTargetGrid.ThingCanMoveToMe(element, new Element.Position(xChange, yChange))) && (groundInTargetGrid == null || groundInTargetGrid.ThingCanMoveToMe(element)))
            {
                Grids[yTarget][xTarget].Element = element;
                Grids[yBase][xBase].Element = null;
                element.SetPosition(new Element.Position(xTarget, yTarget));
                CanMove = true;
            }
        }
        return CanMove;
    }
    public void CharacterApproachExit(CharacterBase character)
    {
        Characters.Remove(character);
        RemoveElement(character);
        if (Characters.Count == 0)
        {
            GameManager.Instance.ChangeState(GameManager.gameState.LevelAccomplish);
        }
    }
}
