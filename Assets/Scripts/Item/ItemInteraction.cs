using UnityEngine;

public class ItemInteraction : MonoBehaviour {
    // Color
    public ItemColor color;

    public GameObject spawn;

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkColor(ItemColor color) {
        return this.color.Equals(color);
    }

    public void respawn() {
        this.transform.position = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
    }

    public void playSound() {
        AudioSource audio = this.gameObject.GetComponent<AudioSource>();
        audio.Play();
    }
}
