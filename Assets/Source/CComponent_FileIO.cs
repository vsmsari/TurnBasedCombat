using System.IO;
using UnityEngine;

public class CComponent_FileIO : MonoBehaviour
{
    public void WriteJsonToFile(string _Name, CGameData _Data)
    {
        string filePath = Application.persistentDataPath + "/" + _Name;
        string rawJson = JsonUtility.ToJson(_Data);
        System.IO.File.WriteAllText(filePath, rawJson);        
    }
    public string LoadJsonFromFile(string _Name)
    {
        string output = string.Empty;
        string filePath = Application.persistentDataPath + "/" + _Name;
        if (File.Exists(filePath))  output = File.ReadAllText(filePath);
        return output;
    }
    
}