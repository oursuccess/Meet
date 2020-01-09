using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript
{
    public static string LevelScriptPath = "Levels";
    public List<List<string>> LevelScripts { get; private set; } = new List<List<string>>();
    public LevelScript(int levelNo)
    {
        string LevelScriptFile = levelNo.ToString().PadLeft(3, '0');
        var LevelScriptText = Resources.Load<TextAsset>( LevelScriptPath + "/" + LevelScriptFile).text;
        LevelScriptText = LevelScriptText.ToUpper();
        LevelScriptText = LevelScriptText.Replace("\r\n", "\n");
        var LevelScriptRows = LevelScriptText.Split('\n');
        foreach(var LevelScriptRow in LevelScriptRows)
        {
            if (!string.IsNullOrEmpty(LevelScriptRow))
            {
                var LevelScriptsOfRow = LevelScriptRow.Split(',');
                LevelScripts.Add(new List<string>(LevelScriptsOfRow));
            }
        }
    }
}
