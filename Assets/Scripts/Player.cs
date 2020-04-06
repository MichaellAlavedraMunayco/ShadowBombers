using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public GlobalStateManager globalManager;

    [Range(0, 3)]
    public int playerNumber = 0;
    public float initialPositionX;
    public float initialPositionZ;
    public GameObject bombPrefab;
    public Transform bombsContainer;
    public AudioClip lifeSound;
    float moveSpeed = 5f;
    private Rigidbody playerRigidBody;
    private Transform playerTransform;
    // private Animator animator;
    protected Joystick moveJoystick;
    protected DropBombJoystick dropBombJoystick;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerTransform = transform;
        // animator = playerTransform.Find("PlayerModel").GetComponent<Animator>();
        moveJoystick = FindObjectOfType<Joystick>();
        dropBombJoystick = FindObjectOfType<DropBombJoystick>();
    }

    void Update()
    {
        if (Mathf.Round(moveJoystick.Vertical) == 1) // top
        {
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, playerRigidBody.velocity.y, moveSpeed);
            playerTransform.rotation = Quaternion.Euler(0, 0, 0); ;
        }
        if (Mathf.Round(moveJoystick.Vertical) == -1) // down
        {
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, playerRigidBody.velocity.y, -moveSpeed);
            playerTransform.rotation = Quaternion.Euler(0, 180, 0); ;
        }
        if (Mathf.Round(moveJoystick.Horizontal) == 1) // right
        {
            playerRigidBody.velocity = new Vector3(moveSpeed, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
            playerTransform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Mathf.Round(moveJoystick.Horizontal) == -1) // left
        {
            playerRigidBody.velocity = new Vector3(-moveSpeed, playerRigidBody.velocity.y, playerRigidBody.velocity.z);
            playerTransform.rotation = Quaternion.Euler(0, 270, 0);
        }

        if (dropBombJoystick.Pressed) { DropBomb(); }
    }

    private void DropBomb()
    {
        if (bombPrefab)
        {
            Vector3 bombPosition = new Vector3(
                Mathf.RoundToInt(playerTransform.position.x),
                bombPrefab.transform.position.y,
                Mathf.RoundToInt(playerTransform.position.z));

            foreach (Transform bomb in bombsContainer)
            {
                if (bomb.position == bombPosition) { return; }
            }
            Instantiate(bombPrefab, bombPosition, bombPrefab.transform.rotation, bombsContainer);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            transform.position = new Vector3(initialPositionX, transform.position.y, initialPositionZ);
            if (globalManager.PlayerDied(playerNumber)) { Invoke("Restart", 2f); }
        }
        if (other.CompareTag("Life"))
        {
            AudioSource.PlayClipAtPoint(lifeSound, transform.position);
            Destroy(other.gameObject);
            globalManager.PlayerLife(playerNumber);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
