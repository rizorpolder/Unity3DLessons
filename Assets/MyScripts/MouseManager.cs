using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clicableLayer;

    public Texture2D pointer;
    public Texture2D target;
    public Texture2D doorway;

    public EventVector3 OnClickEnviroment;
    private bool _useDefaultCursor = false;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void HandleGameStateChanged(GameManager.GameState currentSate, GameManager.GameState previousState)
    {
        _useDefaultCursor = currentSate == GameManager.GameState.PAUSED;
    }
    

    private void Awake()
    {
        //if (GameManager.Instance != null)
        //    GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void Update()
    {
        if (_useDefaultCursor)
        {
            Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            return;
        }


        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clicableLayer.value))
        {
            bool door = false;
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway,new Vector2(16,16),CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(target,new Vector2(16,16),CursorMode.Auto );
            }

            if (Input.GetMouseButton(0))
            {
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    OnClickEnviroment.Invoke(doorway.position+doorway.forward*10);
                }
                OnClickEnviroment.Invoke(hit.point);
            }
        }
        else
        {
            Cursor.SetCursor(pointer, new Vector2(16, 16), CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
