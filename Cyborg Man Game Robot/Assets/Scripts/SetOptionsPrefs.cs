using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SetOptionsPrefs : MonoBehaviour
{
    //penis
    public Slider volumeSlider;
    public Slider mouseSlider;
    public Slider camHeightSlider;
    public Slider camDistSlider;
    public Slider renderDistSlider;
    public LightingSettings l;


    public GameObject thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f);
        mouseSlider.value = PlayerPrefs.GetFloat("sens", 3);
       
        camDistSlider.value = PlayerPrefs.GetFloat("camdist", 2);
        camHeightSlider.value = PlayerPrefs.GetFloat("camheight", 2);

        renderDistSlider.value = PlayerPrefs.GetFloat("renderdist", 175);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetFloat("sens", mouseSlider.value);
        PlayerPrefs.SetFloat("camdist", camDistSlider.value);
        PlayerPrefs.SetFloat("camheight", camHeightSlider.value);
        PlayerPrefs.SetFloat("renderdist", renderDistSlider.value);
        
        
        if (SceneManager.GetActiveScene().name != "menu") 
        {
            RenderSettings.fogDensity = (float)( (-0.00027586206 * renderDistSlider.value) + 0.063275862068966);
            thirdPersonCamera.GetComponent<vThirdPersonCamera>().defaultDistance = camDistSlider.value;
            thirdPersonCamera.GetComponent<vThirdPersonCamera>().height = camHeightSlider.value;
            thirdPersonCamera.GetComponent<Camera>().farClipPlane = renderDistSlider.value;
        }
        // Debug.Log("options " + AudioListener.volume + PlayerPrefs.GetFloat("sens") + PlayerPrefs.GetFloat("volume"));
    }
}
