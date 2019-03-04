using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
[CustomEditor(typeof(GenerateEnemy)), ExecuteInEditMode]

public class GenerateEnemyesEditor : UnityEditor.Editor
{
    LayerMask clicableLayer;
    List<Transform> pointsList = new List<Transform>();

    public  void OnSceneGUI()
    {

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50, clicableLayer.value))
        {
            pointsList.Add(hit.transform);
        }


    }
    public override void OnInspectorGUI()
    {
        GenerateEnemy ge = (GenerateEnemy) target;

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pointsList"), true);

       
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate"))
        {
           
        }

        if (GUILayout.Button("Remove"))
        {
          
        }

        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

   

}
