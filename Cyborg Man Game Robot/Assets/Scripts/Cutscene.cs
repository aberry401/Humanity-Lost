using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Cutscene : MonoBehaviour
{
    public string PrefabToCheck;
    bool hasrun,hasrunDialog;
   public Camera self;
    public Camera other;
    AudioSource aa;
    public float animTime;
    public string LoadScene;
    public List<string> lines;
    public Text text;
    public Animator anim;
    public AudioSource background;

    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().farClipPlane = Mathf.Clamp((PlayerPrefs.GetFloat("renderdist", 60) * 3),80,400);
        aa = GetComponent<AudioSource>();
        self = GetComponent<Camera>();
        other = Camera.main;
        self.enabled = false;
        if(PlayerPrefs.GetInt("hasrun" + PrefabToCheck, 0) == 0)
        {
            hasrun = false;
        }else
        {
            hasrun = true;
        }
        
        hasrunDialog = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(PlayerPrefs.GetInt(PrefabToCheck, 0));
        //for debugging
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt(PrefabToCheck, 1);
            Start();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !hasrun && self.isActiveAndEnabled)
        {
            GameObject.Find("New DDOL(Clone)").GetComponent<NoDestroy>().isHide = false;
            other.enabled = true;
            self.enabled = false;
            hasrun = true;
            PlayerPrefs.SetInt("hasrun" + PrefabToCheck, 1);
            SceneManager.LoadScene(LoadScene);
        }

            //-------------------------------------------------//
            if (hasrun)
        {
            if (LoadScene != "")
            {
                
            }
        }
            if ((PlayerPrefs.GetInt(PrefabToCheck,0) == 1 || PrefabToCheck =="") && !hasrun)
        {
            StartCoroutine(Play());
            if (!hasrunDialog)
            {
                hasrunDialog = true;
                StartCoroutine(PlayDialog());
                background.Play();
            }
            

        }
    }
    IEnumerator Play()
    {
        anim.SetTrigger("cut");
       other.enabled = false;
        self.enabled = true;
        if (GameObject.Find("New DDOL(Clone)") != null)
        {
            GameObject.Find("New DDOL(Clone)").GetComponent<NoDestroy>().isHide = true;
        }
        yield return new WaitForSeconds(animTime);
        if (GameObject.Find("New DDOL(Clone)") != null)
        {
            GameObject.Find("New DDOL(Clone)").GetComponent<NoDestroy>().isHide = false;
        }
        other.enabled = true;
        self.enabled = false;
        hasrun = true;
        PlayerPrefs.SetInt("hasrun" + PrefabToCheck, 1);
        SceneManager.LoadScene(LoadScene);

    }


    IEnumerator PlayDialog()
    {
        Debug.Log("running");
        for(int i = 0; i < lines.Count; i++) 
        {
            text.text = "";
            yield return new WaitForSeconds(1f);
            for (int x = 0; x < lines[i].Length; x++)
            {
                
                text.text += lines[i].Substring(x,1);
                
                    if(lines[i].Substring(x, 1) == " ")
                    {
                        yield return new WaitForSeconds(.065f);
                    }
                    else
                    {
                        aa.pitch = Random.Range(0.7f, 0.85f);
                        aa.Play();
                        yield return new WaitForSeconds(.045f);
                    }
                    
                
            }
            yield return new WaitForSeconds(2f);
        }
        text.text = "";
    }
}
