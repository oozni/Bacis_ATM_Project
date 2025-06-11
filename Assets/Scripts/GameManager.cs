using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    [SerializeField] private UserData _userData;

    public UserDataBase _userDataBase;
   
    [HideInInspector] public string path;
    [HideInInspector] public int userIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "UserData.json");
        JsonLaod();
    }
    public void JsonSave()
    {
        string jsonSave = JsonUtility.ToJson(_userDataBase, true);
        File.WriteAllText(path, jsonSave);

        Debug.Log($"세이브 : {jsonSave}");
    }
    public void JsonLaod()
    {

        if (File.Exists(path))
        {
            string jsonRead = File.ReadAllText(path);
            _userDataBase = JsonUtility.FromJson<UserDataBase>(jsonRead);

            Debug.Log($"로드 : {jsonRead}");
        }
        else
        {
            _userDataBase = new UserDataBase();
        }

    }
}