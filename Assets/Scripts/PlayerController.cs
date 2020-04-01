using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float MovementSpeed = 5f;

    private Vector3 up = Vector3.zero,
    right = new Vector3(0, 90, 0),
    down = new Vector3(0, 180, 0),
    left = new Vector3(0, 270, 0),
    currentDirection = Vector3.zero;
    bool isMoving = true;

    private Vector3 initialPosition = Vector3.zero;

    public void Reset()
    {
        transform.position = initialPosition;
        currentDirection = right;
    }
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        initialPosition = transform.position;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = true;
        if (Input.GetKey(KeyCode.UpArrow)) currentDirection = up;
        else if (Input.GetKey(KeyCode.RightArrow)) currentDirection = right;
        else if (Input.GetKey(KeyCode.DownArrow)) currentDirection = down;
        else if (Input.GetKey(KeyCode.LeftArrow)) currentDirection = left;
        else isMoving = false;

        transform.localEulerAngles = currentDirection;

        if (isMoving)
        {
            transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        isMoving = false;
    }
    void OnCollisionExit(Collision col)
    {
        isMoving = true;
    }

}
