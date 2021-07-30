using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class dialougeController : MonoBehaviour
{
    private bool inSign;
    public bool talking;
    public GameObject dialogueCanv, popupcanv;
    public Text dialogueText, popupText;
    public dialouge d;
    private int num;
    public GameObject button1, button2;
    public GameObject player;

    public Animator anim;
   

    public GameObject shopGameobject;

    // Start is called before the first frame update
    void Start()
    {
        popupcanv.SetActive(false);
        inSign = false;
        dialogueText.text = "";
        dialogueCanv.SetActive(false);
        num = 0;
        talking = false;
        button1.SetActive(false);
        button2.SetActive(false);
        inSign = false;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log("115:" + PlayerPrefs.GetInt("beforeSewer"));
        if (talking)
        {
            anim.enabled = false;
        } else
        {
            anim.enabled = true;
        }



        if (EventSystem.current != null)
        {

            EventSystem.current.SetSelectedGameObject(null);
        }
        if (inSign)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

                talking = true;
                if (num < d.lines.Length)
                {

                    dialogueCanv.SetActive(true);
                    dialogueText.text = d.lines[num] + " (F to continue)";
                    if (d.button1Text != "" && num == d.lines.Length -1)
                    {
                        button1.SetActive(true);
                        button1.GetComponentInChildren<Text>().text = d.button1Text;
                    }
                    else
                    {
                        button1.SetActive(false);

                    }
                    if (d.button2Text != "" && num == d.lines.Length - 1)
                    {
                        button2.SetActive(true);
                        button2.GetComponentInChildren<Text>().text = d.button2Text;
                    }
                    else
                    {

                        button2.SetActive(false);
                    }
                    num++;
                }
                else
                {




                    if (d.changeVar)
                    {
                        if (d.objectFunction)
                        {
                            if (!(d.button1Text != "") && !(d.button2Text != "") && d.changeVar == true && d.obj.GetComponent<objFunctions>().requiredPref == ""
                                || PlayerPrefs.GetInt(d.obj.GetComponent<objFunctions>().requiredPref) == 1)
                            {
                                PlayerPrefs.SetInt(d.varToChange1, int.Parse(d.varValue1));
                                Debug.Log("1");
                            }
                            else
                            {
                                popupText.text = d.cantDoLine;

                                StartCoroutine(PopUp());


                            }
                        }
                        else
                        {
                            if (!(d.button1Text != "") && !(d.button2Text != "") && d.changeVar == true)
                            {
                                PlayerPrefs.SetInt(d.varToChange1, int.Parse(d.varValue1));
                                Debug.Log("2");
                            }
                        }
                    }
                    if (d.obj != null)
                    {
                        if (d.objectFunction && d.obj.GetComponent<objFunctions>().requiredPref == "" && !(d.button1Text != "") && !(d.button2Text != ""))
                        {
                            if (d.obj.GetComponent<objFunctions>() != null)
                            {
                                d.obj.GetComponent<objFunctions>().RunFunction(int.Parse(d.functionIndex1));

                            }
                            d.obj.GetComponentInParent<objFunctions>().RunFunction(int.Parse(d.functionIndex1));
                        }
                        else if (PlayerPrefs.GetInt(d.obj.GetComponent<objFunctions>().requiredPref) == 1 && !(d.button1Text != "") && !(d.button2Text != ""))
                        {
                            if (d.obj.GetComponent<objFunctions>() != null && d.button1Text == "" && d.button2Text == "")
                            {
                                d.obj.GetComponent<objFunctions>().RunFunction(int.Parse(d.functionIndex1));
                            }
                            else
                            {
                                d.obj.GetComponentInParent<objFunctions>().RunFunction(int.Parse(d.functionIndex1));
                            }
                        }
                        else
                        {
                            if (!(d.button1Text != "") && !(d.button2Text != ""))
                            {
                                popupText.text = d.cantDoLine;


                                StartCoroutine(PopUp());
                            }
                        }
                    }
                    dialogueCanv.SetActive(false);
                    num = 0;
                    talking = false;
                }
               

            }
        }
        else
        {
            shopGameobject.SetActive(false);
            dialogueCanv.SetActive(false);
            num = 0;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "dialogue")
        {
            d = other.GetComponent<dialouge>();
            inSign = true;
            //Debug.Log("collisison!");
        }
        else
        {
        //    inSign = false;
        }
    }

   


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "dialogue")
        {
            d = other.GetComponent<dialouge>();
            inSign = true;
            //Debug.Log("collisison!");
            
        }
        if (other.tag == "barrier")
        {
            popupText.text = "My Robotic Parts wont let me go any further - I should finish other business that I have first";

            StartCoroutine(PopUp());
        }
        if(other.tag == "pickup")
        {
            ChipPickup cp = other.gameObject.GetComponent<ChipPickup>();
            popupText.text = "Got " + cp.getType() + " Part " + cp.getName() + "!";
            StartCoroutine(PopUp());
        }
    }
    void OnTriggerExit(Collider other)
    {

        if (other.tag == "dialogue")
        {
            inSign = false;
            talking = false;
        }
    }
    public void Button1Press()
    {
        if (d.giveScrap)
        {
            player.GetComponent<newController>().scrap += d.scrapAmmnt;
        }
        if (d.changeVar == true)
        {
            PlayerPrefs.SetInt(d.varToChange1, int.Parse(d.varValue1));

        }
        if (d.objectFunction == true)
        {
            d.obj.GetComponent<objFunctions>().RunFunction(int.Parse(d.functionIndex1));
        }
        inSign = false;
        talking = false;
    }
    public void Button2Press()
    {
        if (d.giveScrap)
        {
            player.GetComponent<newController>().scrap += d.scrapAmmnt;
        }
        if (d.changeVar == true)
        {
            PlayerPrefs.SetInt(d.varToChange2, int.Parse(d.varValue2));
        }
        if (d.objectFunction == true)
        {
            d.obj.GetComponent<objFunctions>().RunFunction(int.Parse(d.functionIndex2));
        }
        inSign = false;
        talking = false;
    }
    IEnumerator PopUp()
    {
        popupcanv.SetActive(true);
        yield return new WaitForSeconds(1f + popupText.text.Length / 30);
        popupcanv.SetActive(false);
    }
}