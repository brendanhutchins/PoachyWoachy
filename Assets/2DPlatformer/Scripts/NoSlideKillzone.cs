using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoSlideKillzone : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.CompareTag("Player"))
          {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

          }
          //else if (collision.gameObject.GetComponent<PlayerPlatformerController>().slide)
          //{
          //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          //  Debug.Log(collision.gameObject);
          //}
    }
}

