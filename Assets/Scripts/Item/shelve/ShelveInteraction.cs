using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelveInteraction : MonoBehaviour {

    public GameObject model;


    private List<GameObject> items = new List<GameObject>();
    public readonly int MaxStock = 12;
    private int stock = -1;
    public readonly int ItemPerRow = 4;

    // Update is called once per frame
    void Start() {
        this.placeItems();
    }

    public void storeItem() {
        if (this.stock + 1 < this.MaxStock) {
            this.items[++this.stock].SetActive(true);
        } else {
            foreach (GameObject item in this.items) {
                item.SetActive(false);
            }
            this.stock = -1;

            ScoreScript.score += 10;
        }
    }

    public void takeItem() {
        if (this.stock - 1 >= 0) {
            this.items[this.stock--].SetActive(false);
        }
    }

    private void placeItems() {
        Vector2 basePostition = new Vector2(this.transform.position.x - 0.5f, this.transform.position.y - 2);
        for (int i = 0; i < this.MaxStock; i++) {
            // Create the vector
            Vector2 itemPosition = new Vector2(basePostition.x + (i % 2), basePostition.y + (i / 4) * 2);

            // Duplicate an item
            GameObject item = Instantiate(this.model, itemPosition, Quaternion.identity);
            
            // Place the item on layer
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "Decor";
            spriteRenderer.sortingOrder = ((i / this.ItemPerRow) * 10 + (i % this.ItemPerRow)) + 1; // sort order

            // Hide
            item.SetActive(false);

            // Save the item
            this.items.Add(item);
        }
    }
}
