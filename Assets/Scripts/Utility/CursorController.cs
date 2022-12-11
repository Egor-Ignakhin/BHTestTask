using UnityEngine;

public class CursorController : MonoBehaviour
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}