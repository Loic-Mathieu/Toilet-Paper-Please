using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryInteraction : MonoBehaviour {

    public ItemColor color;
    public GameObject shelve;

    public int respawnDelay = 5;

    // Update is called once per frame
    void Update() {

    }

    public bool checkColor(ItemColor color) {
        return this.color.Equals(color);
    }

    public void deliver(ItemInteraction item) {
        item.playSound();
        this.shelve.GetComponent<ShelveInteraction>().storeItem();
        ScoreScript.score += 3;

        // far away
        item.transform.position = new Vector2(item.transform.position.x, item.transform.position.y + 100);

        // Trigger in 5 seconds
        StartCoroutine(this.respawnItem(item));
    }

    private IEnumerator respawnItem(ItemInteraction item) {
        yield return new WaitForSeconds(this.respawnDelay);
        item.respawn();
    }
}
