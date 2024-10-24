using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private Vector2 inputValue;

    // Start is called before the first frame update
    void Start()
    {
        inputValue = new Vector2(0, 0);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputValue = InputManager.Instance.moveAction.ReadValue<Vector2>();
        Vector3 moveVec = new Vector3(inputValue.x, 0, inputValue.y);
        rb.MovePosition(rb.position + moveVec * speed * Time.deltaTime);
    }
}
