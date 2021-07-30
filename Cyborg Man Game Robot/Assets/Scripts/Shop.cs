using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<shopItem> items;
    public bool isOpen;

    private Canvas screen;

    // Start is called before the first frame update
    void Update()
    {
       
    }
    void Start()
    {
        isOpen = false;
        screen = GameObject.Find("samplePlayer").GetComponent<dialougeController>().shopGameobject.GetComponent<Canvas>();
        screen.gameObject.SetActive(false);
    }

    //opens the Shop Screen canvas, displays items for sale at this shop instance
    public void Open()
    {

        screen.gameObject.SetActive(true);

        shopButton currentButton;
        for(int b = 1; b <= 5; b++)
        {
            currentButton = screen.transform.Find("Background").transform.Find("ItemFrame" + b).GetComponent<shopButton>();
            currentButton.shop = this;
            currentButton.OnNewShop();
        }

       
    }

    //closes Shop Screen canvas
    public void Close()
    {
        isOpen = false;
        screen.gameObject.SetActive(false);
    }
}
