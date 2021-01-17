using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public GameObject spawnPoint;
    private ItemColor color { get; set; }

    // Movement
    public new Rigidbody2D rigidbody;

    private bool _isOnScreen = false;
    private Vector2 _destination;

    public float speed = 3f;

    private bool goingRight = true;

    // Interaction
    public float radius = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (!ScoreScript.gameOver && this._isOnScreen) {
            // Get objects around customer
            Collider2D[] objectsNearPlayer = Physics2D.OverlapCircleAll(this.transform.position, this.radius);
            foreach (Collider2D collider in objectsNearPlayer) {
                if (collider.CompareTag("Player")) {
                    PlayerMovement player = collider.gameObject.GetComponent<PlayerMovement>();
                    player.customerInteraction();
                }
            }

            // Respawn check
            if ((this.goingRight && this.transform.position.x > this._destination.x)
                || (!this.goingRight && this.transform.position.x < this._destination.x)) {
                this._isOnScreen = false;
                this.transform.position = new Vector2(this.spawnPoint.transform.position.x, this.spawnPoint.transform.position.y);
            }

        }
    }

    public void spawn(Vector2 position, Vector2 destination) {
        this._destination = destination;
        this.transform.position = new Vector2(position.x, position.y);
        this.gameObject.SetActive(true);
        this._isOnScreen = true;
    }

    private void FixedUpdate() {
        if (_isOnScreen) {
            Vector2 vector = new Vector2(this.transform.position.x - 0.3f, this.transform.position.y);
            this.rigidbody.MovePosition(this.rigidbody.position + this._destination * (this.speed / 10) * Time.fixedDeltaTime);
        }
    }

    public bool isMoving() {
        return this._isOnScreen;
    }
}
