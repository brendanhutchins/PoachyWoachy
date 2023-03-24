using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour {
    public Animator animator;
    private int levelToLoad;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // levelToLoad = levelIndex;
            animator.SetTrigger("Fade_out");
            //FadeToLevel(1); // 
        }
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    void Update () {
		
	}
}
