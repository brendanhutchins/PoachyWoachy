using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck :MonoBehaviour
{

    [SerializeField]
    GameObject dustCloud;

    bool coroutineAllowed, grounded;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
            coroutineAllowed = true;
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
            coroutineAllowed = false;
        }
    }

    void Update()
    {
        if (grounded && Input.GetKeyDown(KeyCode.DownArrow) && coroutineAllowed)
        {
            StartCoroutine("SpawnCloud");
            coroutineAllowed = false;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || !grounded)
        {
            StopCoroutine("SpawnCloud");
            coroutineAllowed = true;
        }
    }

    IEnumerator SpawnCloud()
    {
        while (grounded)
        {
            Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
