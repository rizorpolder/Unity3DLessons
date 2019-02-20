using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateEnemy)), ExecuteInEditMode]

public class GenerateEnemyesEditor : UnityEditor.Editor
{


    private  int _enemyesCount;
    private int _radius;
    public GameObject ObjectInstantiate;
    private int _key=0;
    private GameObject[] gameObjects { get; set; }
    public string[] namesOfPref { get; set; }

   
    public override void OnInspectorGUI()
    {
        GenerateEnemy ge = (GenerateEnemy) target;

        FillString();

        GUILayout.Label("Random Enemy Placer", EditorStyles.boldLabel);

        ObjectInstantiate =EditorGUILayout.ObjectField("Enemy pfb",ObjectInstantiate,typeof(GameObject),true) as GameObject;
        
        _key = EditorGUILayout.Popup("Prefabs",_key,namesOfPref);
        
        _enemyesCount = EditorGUILayout.IntField("Enemyes Count",Mathf.Clamp(_enemyesCount,0,100));

        _radius = EditorGUILayout.IntField("Radius from 0", Mathf.Clamp(_radius,0, 100));
        GUILayout.Space(10);

        
        
        if (GUILayout.Button("GetPrefabs"))
        {
            FillString();
        }
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate"))
        {
            ge.PlaceEnemy(ObjectInstantiate, _enemyesCount, _radius);
        }
        if(GUILayout.Button("Remove"))
        {
            ge.RemoveEnemyes();
        }
        GUILayout.EndHorizontal();
       

    }

    private string [] FillString()
    {
        GetContents();
        namesOfPref = new string [gameObjects.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            namesOfPref[i] = gameObjects[i].name.ToString();
        }

        return namesOfPref;
    }
    public GameObject[] GetContents()
    {
        gameObjects = Resources.LoadAll<GameObject>("Assets/Prefab");
        
        return gameObjects;

    }
}
