using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clicableLayer;

    public Texture2D pointer;
    public Texture2D target;
    public Texture2D doorway;
    public Texture2D sword;


    public EventVector3 OnClickEnviroment;
    public EventGameObject OnClickAttackable;


    private bool _useDefaultCursor = false;

    
    

    private void Awake()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
    //private void Start()
    //{
    //    GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    //}

    public void HandleGameStateChanged(GameManager.GameState currentSate, GameManager.GameState previousState)
    {
        _useDefaultCursor = currentSate == GameManager.GameState.PAUSED;
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

            bool chest = false;
            if (hit.collider.gameObject.tag == "Chest")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            

            bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
            if (isAttackable)
            {
                Cursor.SetCursor(sword, new Vector2(16,16),CursorMode.Auto);
            }


            if (Input.GetMouseButton(0))
            {
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    OnClickEnviroment.Invoke(doorway.position+doorway.forward*10);
                }
                else if (isAttackable)
                {
                    GameObject attackable = hit.collider.gameObject;
                    OnClickAttackable.Invoke(attackable);
                }
                else if (!chest)
                {
                    OnClickEnviroment.Invoke(hit.point);
                }
                
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
[System.Serializable]
public class EventGameObject : UnityEvent<GameObject> { }