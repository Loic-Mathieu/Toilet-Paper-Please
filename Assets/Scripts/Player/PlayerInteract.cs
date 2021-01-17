using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    private bool _grabbing = false;
    private Collider2D _heldItem;

    public float radius = 0.75f;

    // Update is called once per frame
    void Update()
    {
        if (ScoreScript.gameOver) {
            return;
        }

        // Get objects around player
        Collider2D[] objectsNearPlayer = Physics2D.OverlapCircleAll(this.transform.position, this.radius);

        // Check player interaction
        foreach (Collider2D collider in objectsNearPlayer) {
            if (!this._grabbing && collider.CompareTag("ITEM")) {
                // Pick up item
                this._heldItem = collider;
                this._grabbing = true;

                // Pick up sound
                AudioSource audioSource = collider.GetComponent<AudioSource>();
                audioSource.Play();
            } else if (this._grabbing && collider.CompareTag("DELIVERY")) {
                // Release held item
                ItemInteraction item = this._heldItem.gameObject.GetComponent<ItemInteraction>();
                DeliveryInteraction delivery = collider.gameObject.GetComponent<DeliveryInteraction>();
                if (delivery.checkColor(item.color)) {
                    this.throwItem();
                    delivery.deliver(item);
                }
            } else if (collider.CompareTag("CUSTOMER")) {
                // Slow down
                PlayerMovement movement = this.GetComponent<PlayerMovement>();
                movement.customerInteraction();
            }
        }

        // Carry item
        if (this._grabbing) {
            this._heldItem.transform.position = new Vector2(this.transform.position.x, (this.transform.position.y + 1.1f));
        }
    }

    private void throwItem() {
        // Let down item
        this._heldItem.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        this._heldItem = null;
        this._grabbing = false;
    }

    public bool isGrabbing() => this._grabbing;
}
