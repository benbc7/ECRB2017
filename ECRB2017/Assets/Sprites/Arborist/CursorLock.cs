using UnityEngine;
public class CursorLock : MonoBehaviour
{
    public bool cursorHide;
    void Start()
    {
        cursorHide = true;
        UpdateCursor();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            cursorHide = false;
            UpdateCursor();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            cursorHide = true;
            UpdateCursor();
        }
    }
    void UpdateCursor()
    {
        if (cursorHide)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}


