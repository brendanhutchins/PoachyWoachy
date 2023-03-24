
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour {

    public Animator animator;

    private int levelToLoad;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
       {
            FadeToLevel(0);
       }
	}
    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Fade_out");
    }
    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
 }  
}
