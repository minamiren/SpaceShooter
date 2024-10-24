using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public InputActionAsset actionsAsset;
    public InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        moveAction = actionsAsset.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
