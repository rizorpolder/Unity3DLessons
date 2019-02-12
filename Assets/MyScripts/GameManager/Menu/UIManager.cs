using UnityEngine;


class UIManager: MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Camera _dummyCamera;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _mainMenu.FadeOut();
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

}

