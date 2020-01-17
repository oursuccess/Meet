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
    public const string BombScript = "BOMB";
    public const string KeyScript = "KEY";
    public const string ElecSwitchScript = "ELECSWITCH";
    public const string ElecSpringScript = "ELECSPRING";
    public const string DoorScript = "DOOR";
    public const string ElecDoorScript = "ELECDOOR";
    public const string NormalCharacterScript = "NORMALCHARACTER";
    public const string HorizontalCharacterScript = "HORIZONTALCHARACTER";
    public const string VerticalCharacterScript = "VERTICALCHARACTER";
    public const string ChainCharacterScript = "CHAINCHARACTER";
    public const string CharacterDirectionSwitchScript = "CHARACTERDIRECTIONSWITCH";
    public const string NoGrid = "NOGRID";
    public const string DontHandleThisGrid = "DONTHANDLETHISGRID";
    #endregion
    #region Prefab
    private GameObject WallPrefab;
    private GameObject FloorPrefab;
    private GameObject ExitPrefab;
    private GameObject BoxPrefab;
    private GameObject BombPrefab;
    private GameObject KeyPrefab;
    private GameObject ElecSwitchPrefab;
    private GameObject ElecSpringPrefab;
    private GameObject DoorPrefab;
    private GameObject ElecDoorPrefab;
    private GameObject NormalCharacterPrefab;
    private GameObject HorizontalCharacterPrefab;
    private GameObject VerticalCharacterPrefab;
    private GameObject ChainCharacterPrefab;
    private GameObject CharacterDirectionSwitchPrefab;
    private GameObject GridPrefab;
    private void InitPrefabs()
    {
        string PrefabPath = "Prefabs";
        WallPrefab = Resources.Load<GameObject>(PrefabPath + "/Wall");
        FloorPrefab = Resources.Load<GameObject>(PrefabPath + "/Floor");
        ExitPrefab = Resources.Load<GameObject>(PrefabPath + "/Exit");
        BoxPrefab = Resources.Load<GameObject>(PrefabPath + "/Box");
        BombPrefab = Resources.Load<GameObject>(PrefabPath + "/Bomb");
        KeyPrefab = Resources.Load<GameObject>(PrefabPath + "/Key");
        ElecSwitchPrefab = Resources.Load<GameObject>(PrefabPath + "/ElecSwitch");
        ElecSpringPrefab = Resources.Load<GameObject>(PrefabPath + "/ElecSpring");
        DoorPrefab = Resources.Load<GameObject>(PrefabPath + "/Door");
        ElecDoorPrefab = Resources.Load<GameObject>(PrefabPath + "/ElecDoor");
        NormalCharacterPrefab = Resources.Load<GameObject>(PrefabPath + "/NormalCharacter");
        HorizontalCharacterPrefab = Resources.Load<GameObject>(PrefabPath + "/HorizontalCharacter");
        VerticalCharacterPrefab = Resources.Load<GameObject>(PrefabPath + "/VerticalCharacter");
        ChainCharacterPrefab = Resources.Load<GameObject>(PrefabPath + "/ChainCharacter");
        CharacterDirectionSwitchPrefab = Resources.Load<GameObject>(PrefabPath + "/CharacterDirectionSwitch");
        GridPrefab = Resources.Load<GameObject>(PrefabPath + "/Grid");
    }
 #endregion
    #region GridsNElements
    public List<List<Grid>> Grids { get; private set; }
    private List<Character> Characters;
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
                    if (Grid.Ground != null)
                    {
                        Destroy(Grid.Ground.gameObject);
                        Grid.Ground = null;
                    }
                    Destroy(Grid.gameObject);
                }
            }
        }
        Grids = new List<List<Grid>>();
        Characters = new List<Character>();
    }
    public void RemoveElement(Element element)
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
                    if (Scripts[y][x] != DontHandleThisGrid)
                    {
                        GameObject ElementPrefab = null;
                        GameObject GroundPrefab = null;
                        var Script = Scripts[y][x];
                        string ScriptValue = null;
                        if (Script.Contains(":"))
                        {
                            var ScriptKeyValue = Script.Split(':');
                            Script = ScriptKeyValue[0];
                            ScriptValue = ScriptKeyValue[1];
                        }
                        switch (Script)
                        {
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
                            case BoxScript:
                                {
                                    ElementPrefab = BoxPrefab;
                                }
                                break;
                            case BombScript:
                                {
                                    ElementPrefab = BombPrefab;
                                }
                                break;
                            case KeyScript:
                                {
                                    ElementPrefab = KeyPrefab;
                                }
                                break;
                            case ElecSwitchScript:
                                {
                                    GroundPrefab = ElecSwitchPrefab;
                                }
                                break;
                            case ElecSpringScript:
                                {
                                    GroundPrefab = ElecSpringPrefab;
                                }
                                break;
                            case DoorScript:
                                {
                                    GroundPrefab = DoorPrefab;
                                }
                                break;
                            case ElecDoorScript:
                                {
                                    GroundPrefab = ElecDoorPrefab;
                                }
                                break;
                            case NormalCharacterScript:
                                {
                                    ElementPrefab = NormalCharacterPrefab;
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
                            case ChainCharacterScript:
                                {
                                    ElementPrefab = ChainCharacterPrefab;
                                }
                                break;
                            case CharacterDirectionSwitchScript:
                                {
                                    GroundPrefab = CharacterDirectionSwitchPrefab;
                                }
                                break;
                        }
                        if(GroundPrefab != null)
                        {
                            var GroundObject = Instantiate(GroundPrefab, new Vector3(xBegin + x, yBegin - y), Quaternion.identity);
                            var Ground = GroundObject.GetComponent<Ground>();
                            Ground.Board = this;
                            Ground.SetPosition(new PositionInGrid(x, y));
                            Grid.Ground = Ground;

                            if(Ground is ElecDoor elecDoor)
                            {
                                elecDoor.InitElecSwitchScript(ScriptValue);
                            }
                        }
                        if(ElementPrefab != null)
                        {
                            var ElementObject = Instantiate(ElementPrefab, new Vector3(xBegin + x, yBegin - y), Quaternion.identity);
                            var Element = ElementObject.GetComponent<Element>();
                            Element.Board = this;
                            Element.SetPosition(new PositionInGrid(x, y));
                            Grid.Element = Element;

                            if(Element is Character character)
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
        OnBoardInitFinished?.Invoke();
    }
    public delegate void BoardInitFinishDel();
    public event BoardInitFinishDel OnBoardInitFinished;
    public bool CanMoveTo(Element element, int xChange, int yChange)
    {
        bool CanMove = false;
        int yBase = element.PositionInGrid.y, yTarget = yBase - yChange;
        int xBase = element.PositionInGrid.x, xTarget = xBase + xChange;
        if(Grids[yBase][xBase].Element == element && 0 <= yTarget && yTarget < Grids.Count && 0 <= xTarget && xTarget < Grids[yTarget].Count)
        {
            var elementInTargetGrid = Grids[yTarget][xTarget].Element;
            var groundInTargetGrid = Grids[yTarget][xTarget].Ground;
            if ((elementInTargetGrid == null || elementInTargetGrid.ThingCanMoveToMe(element, new PositionInGrid(xChange, yChange))) && (groundInTargetGrid == null || groundInTargetGrid.ThingCanMoveToMe(element)))
            {
                CanMove = true;
            }
        }
        return CanMove;
    }
    public void ElementMoveTo(Element element, int xChange , int yChange)
    {
        int yBase = element.PositionInGrid.y, yTarget = yBase - yChange;
        int xBase = element.PositionInGrid.x, xTarget = xBase + xChange;
        var nextElement = Grids[yTarget][xTarget].Element;
        if(nextElement != null) nextElement.ThingMoveToMe(element, new PositionInGrid(xChange, yChange));
        Grids[yTarget][xTarget].Element = element;
        Grids[yBase][xBase].Element = null;
        element.SetPosition(new PositionInGrid(xTarget, yTarget));
    }
    public void CharacterApproachExit(Character character)
    {
        if (character.IsChained)
        {
            character.ChainedCharacter.ApproachExit();
        }
        else
        {
            Characters.Remove(character);
            RemoveElement(character);
        }
        if (Characters.Count == 0)
        {
            GameManager.Instance.ChangeState(GameManager.gameState.LevelAccomplish);
        }
    }
    public Element GetElementOfPosition(int xPos, int yPos)
    {
        Element element = null;
        if(yPos < 0 || yPos >= Grids.Count || xPos < 0 || xPos >= Grids[yPos].Count || Grids[yPos][xPos] == null)
        {
        }
        else
        {
            element = Grids[yPos][xPos].Element;
        }
        return element;
    }
    public Ground GetGroundOfPosition(int xPos, int yPos)
    {
        Ground ground = null;
        if(yPos < 0 || yPos >= Grids.Count || xPos < 0 || xPos >= Grids[yPos].Count || Grids[yPos][xPos] == null)
        {

        }
        else
        {
            ground = Grids[yPos][xPos].Ground;
        }
        return ground;
    }
    public bool IsClosedTo(Element element1, Element element2)
    {
        return Mathf.Abs(element1.PositionInGrid.y - element2.PositionInGrid.y) + Mathf.Abs(element1.PositionInGrid.x - element2.PositionInGrid.x) == 1;
    }
}
