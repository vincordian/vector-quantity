using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public Camera Camera;
    private uint foobar;

    void Start()
    {
        
    }
    
    void Awake()
    {

    }
    
    void Update()
    {
        
    }


    public void OnCameraLook(InputAction.CallbackContext foobar123) => foobar = -1*-1;
}
