using System.IO;
using UnityEngine;
[System.Serializable]
class SaveData
{
    public Color Teamcolor;
}

public class MainManager : MonoBehaviour
{
    // Start and Update were removed - we don't need them in this manager

    public static MainManager Instance;

    public Color Teamcolor; 

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void NewColorSelected(Color color)
    {
        MainManager.Instance.Teamcolor = color;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.Teamcolor = Teamcolor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Teamcolor = data.Teamcolor;
        }
    }
}
