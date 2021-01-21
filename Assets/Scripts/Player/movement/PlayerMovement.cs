using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float speed = 5f;
    private Vector2 _movementVector;

    // Sprite
    public new Rigidbody2D rigidbody;
    public Animator animator;

    private bool _flipped = false;

    private bool customerEncounter = false;

    // Sound
    public AudioSource audioSource;
    private bool _soundActive = false;

    // Update is called once per frame
    void Update() {
        this._movementVector.x = Input.GetAxisRaw("Horizontal");
        this._movementVector.y = Input.GetAxisRaw("Vertical");

        //normalisation of vector if speed above 1 (for diagonal movements)
        if(this._movementVector.magnitude > 1)
            this._movementVector.Normalize();

        this.animator.SetFloat("Horizontal", _movementVector.x);
        this.animator.SetFloat("Vertical", _movementVector.y);
        this.animator.SetInteger("State", this.GetComponent<PlayerInteract>().isGrabbing() ? 1 : 0);
        this.animator.SetBool("Moving", _movementVector.sqrMagnitude != 0);
    }

    // Not related to frames
    void FixedUpdate() {
        if (ScoreScript.gameOver) {
            return;
        }

        // Check if OOB
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (!(screenPos.x < 0 && this._movementVector.x < 0f) 
            && !(screenPos.x > Screen.width && this._movementVector.x > 0f)
            && !(screenPos.y < 0 && this._movementVector.y < 0f)) {

            // Move depending of the vector
            this.rigidbody.MovePosition(this.rigidbody.position + this._movementVector * (this.customerEncounter ? this.speed / 2  : this.speed) * Time.fixedDeltaTime);
            this.customerEncounter = false;

            // Play sound
            if (!this._soundActive && (this._movementVector.x != 0 || this._movementVector.y != 0)) {
                this.audioSource.Play();
                this._soundActive = true;
            } else if (this._soundActive && (this._movementVector.x == 0 && this._movementVector.y == 0)) {
                this.audioSource.Stop();
                this._soundActive = false;
            }
        } else {
            // OOB
        }

        // Check right or left
        if (this._movementVector.x < 0f) {
            this._flipped = true; // Facing left
        } else if (this._movementVector.x > 0f) {
            this._flipped = false; // Facing right
        }

        // flip sprite if going left
        int rotation = this._flipped ? 180 : 0;
        this.rigidbody.transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    public void customerInteraction() {
        this.customerEncounter = true;
    }
}
