using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions.Must;

[CustomEditor(typeof(GenerateEnemy)), ExecuteInEditMode]

public class GenerateEnemyesEditor : UnityEditor.Editor
{


    private  int _enemyesCount;
    private int _radius;
    public GameObject ObjectInstantiate;


    public override void OnInspectorGUI()
    {
        GenerateEnemy ge = (GenerateEnemy) target;

        GUILayout.Label("Random Enemy Placer", EditorStyles.boldLabel);
        ObjectInstantiate =EditorGUILayout.ObjectField("Enemy pfb",ObjectInstantiate,typeof(GameObject),true) as GameObject;
        
        
        _enemyesCount = EditorGUILayout.IntField("Enemyes Count",Mathf.Clamp(_enemyesCount,0,100));

        _radius = EditorGUILayout.IntField("Radius from 0", Mathf.Clamp(_radius,0, 100));
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
}
