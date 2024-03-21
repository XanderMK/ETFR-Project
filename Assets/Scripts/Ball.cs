using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameManager gameManager;
    Vector3 initialPosition;
    Rigidbody rb;

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject);

            transform.position = initialPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            gameManager.Score++;
            gameManager.UpdateScoreText();
        }
        else if (other.gameObject.CompareTag("Ball Deletion Trigger")) {
            transform.position = initialPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
