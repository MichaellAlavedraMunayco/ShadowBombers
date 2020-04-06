using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemy : MonoBehaviour
{
    public GlobalStateManager globalManager;

    [Range(1, 3)]
    public int playerNumber;
    public GameObject bombPrefab;
    public Transform bombsContainer;
    public float initialPositionX;
    public float initialPositionZ;
    public AudioClip lifeSound;

    private RaycastHit playerTarget;
    private float moveSpeed = 5f;
    private Rigidbody rigidBody;
    public LayerMask levelMask;
    public string layerName;
    public float distance;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out playerTarget, 5f, levelMask))
        {
            layerName = playerTarget.transform.tag;

            if (layerName == "Brick" || layerName == "Player")
            {
                if (Vector3.Distance(transform.position, playerTarget.transform.position) <= 1.5f)
                {
                    DropBomb();
                }
            }
        }
        Move();
    }

    void Move()
    {
        switch ((int)Random.Range(1f, 5f))
        {
            case 1: // top
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
                transform.rotation = Quaternion.Euler(0, 0, 0); break;
            case 2: // left
                rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
                transform.rotation = Quaternion.Euler(0, 270, 0); break;
            case 3: // down
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
                transform.rotation = Quaternion.Euler(0, 180, 0); break;
            case 4: //right
                rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
                transform.rotation = Quaternion.Euler(0, 90, 0); break;
        }
    }

    private void DropBomb()
    {
        if (bombPrefab)
        {
            Vector3 bombPosition = new Vector3(
                Mathf.RoundToInt(transform.position.x),
                bombPrefab.transform.position.y,
                Mathf.RoundToInt(transform.position.z));

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
            if (globalManager.PlayerDied(playerNumber))
            {
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Life"))
        {
            AudioSource.PlayClipAtPoint(lifeSound, transform.position);
            Destroy(other.gameObject);
            globalManager.PlayerLife(playerNumber);
        }
    }

}
