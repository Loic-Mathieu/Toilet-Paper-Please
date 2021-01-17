using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Animator animator;

    public static int score = 100;
    public static bool gameOver = false;

    public float resetTime = 5f;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        ScoreScript.score = 100;
        ScoreScript.gameOver = false;
        InvokeRepeating("decreaseScore", 1.0f, 1.0f);
    }

    void decreaseScore() {
        if (score >= 0) {
            ScoreScript.score--;
            // Game over
            if (ScoreScript.score == 0) {
                this.triggerGameOver();
            }

            this.animator.SetInteger("Score", ScoreScript.score);
        }
    }

    private void triggerGameOver() {
        Debug.Log("Game Over");
        ScoreScript.gameOver = true;

        this.gameOverScreen.transform.position = new Vector2(0, 0);

        Invoke("restart", this.resetTime);
    }

    private void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
