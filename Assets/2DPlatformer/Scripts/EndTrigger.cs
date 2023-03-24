using UnityEngine;
using UnityEngine.SceneManagement;
public class EndTrigger : MonoBehaviour {

    public PlayerPlatformerController gameManager;

    void OnTriggerEnter2D (Collider2D Collision)
    {
        if(Collision.CompareTag("Player"))
        {
            gameManager.CompleteLevel();
        }
      

    }
}
