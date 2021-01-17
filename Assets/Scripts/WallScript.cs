using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    // Timer
    private float _time;
    private float _maxtime;

    // customer to spawn
    public GameObject customer;

    // Spawn
    public GameObject spawnArea;

    // End position
    public GameObject otherWall;

    // Start is called before the first frame update
    void Start()
    {
        this.setTimer(Random.Range(5, 10));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ScoreScript.gameOver) {
            return;
        }

        this._time += Time.deltaTime;
        if (this._time >= this._maxtime) {
            CustomerScript customer = this.customer.GetComponent<CustomerScript>();
            if (!customer.isMoving()) {
                customer.spawn(this.getRandomSpawn(), otherWall.transform.position);
                this.setTimer(Random.Range(5, 10));
            }
        }
    }

    private Vector2 getRandomSpawn() {
        Collider2D collider = this.spawnArea.GetComponent<Collider2D>();
        return new Vector2(
           this.transform.position.x,
           Random.Range(collider.bounds.min.y, collider.bounds.max.y)
        );
    }

    private void setTimer(float maxTime) {
        this._maxtime = maxTime;
        this._time = 0;
    }
}
