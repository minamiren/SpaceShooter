using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    public GameObject bolt;

    private float shotWait = 0.3f;
    private float timeElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputValue = InputManager.Instance.moveAction.ReadValue<Vector2>();
        Vector3 moveVec = new Vector3(inputValue.x, 0, inputValue.y);
        //Vector3 changePos = rb.position + moveVec * speed * Time.deltaTime;
        //changePos = new Vector3(Mathf.Clamp(changePos.x, -16, 16), changePos.y, 
        //    Mathf.Clamp(changePos.z, -2, 13));
        //rb.MovePosition(changePos); // plays more like a classic arcade shooter
        rb.AddForce(moveVec * speed); // increase drag drastically and this works
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, -16, 16), rb.position.y,
            Mathf.Clamp(rb.position.z, -2, 13));
        float rotationValue = 0;
        rotationValue = Mathf.Clamp(-1 * rb.GetPointVelocity(rb.position).x * rotationSpeed * Time.deltaTime, -45, 45);
        rb.rotation = Quaternion.Euler(0,0,rotationValue);
        if(timeElapsed == 0)
        {
            if(InputManager.Instance.fireAction.ReadValue<float>() == 1)
            {
                timeElapsed = Time.deltaTime;
                Instantiate(bolt, GameObject.Find("BoltSpawn").transform);
            }
        } else {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > shotWait) timeElapsed = 0;
        }
    }
}
