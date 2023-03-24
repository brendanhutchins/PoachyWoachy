using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {
    public Transform[] backgrounds; //array of all the back and foregrounds to be parallaxed
    private float[] parallaxScales; //proportion of the cameras movement to move the backgrounds by
    public float smoothing= 1f; // How smooth the parallax is going to be. Make sure to set above zero.
                                // Use this for initialization
    private Transform cam; //reference to the main cameras transform
    private Vector3 previousCamPos; //stores position of the camera in the previos frame.
    //called before start, great for refferences
    void Awake()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }
    void Start () {
        //the previous frame had the currents frames camera position
        previousCamPos = cam.position;

        //assigning coresponding parallax scales.
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}

	
	// Update is called once per frame
	void Update () {
		// for each bg
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement because the previous frame multiplyed b scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            // creates a target position which is the backgrounds current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }
        //set the previousCamPos to the cameras position at the end of the frame
        previousCamPos = cam.position;
	}
}
