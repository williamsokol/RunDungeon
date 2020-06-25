using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertoryUI : MonoBehaviour
{
    public Invertory     inentory;
    public GameObject    invUI;
    public RectTransform content;
    public GameObject    itemUIPrefab;

    
    public void GetInventory()
    {
        inentory = Invertory.instance; 
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && invUI.activeInHierarchy)
        {
            HideInventoryUI();
        }
        if (Input.GetKeyDown(KeyCode.I) && !invUI.activeInHierarchy)
        {
            ShowInventoryUI();
        }
    }

    public void ShowInventoryUI()
    {
        invUI.SetActive(true);
        Time.timeScale = 0f;
        /*
        foreach(GameObject item in inentory.backpack)
        {
            print("test1");
            GameObject _item = Instantiate(itemUIPrefab);
            _item.GetComponent<RectTransform>().SetParent(content);

            ItemUI itemUi       = _item.GetComponent<ItemUI>();
            itemUi.inventory    = inentory;
            itemUi.item         = item.GetComponent<Item>();

            itemUi._name.text   = item.GetComponent<Item>().name.ToString();
            itemUi._attack.text = item.GetComponent<Item>().Attack.ToString();
            itemUi._defend.text = item.GetComponent<Item>().Def.ToString();
        }*/
       
    }

    public void HideInventoryUI()
    {
        print("test");
        invUI = gameObject.transform.GetChild(0).gameObject;

        Time.timeScale = 1f;
        Transform[] objs = content.GetComponentsInChildren<Transform>();
        invUI.SetActive(false);
        if (objs.Length > 1)
        {
            for (int i = 1; i <= objs.Length; i++)
            {
                Destroy(objs[i].gameObject);
            }
        }
    }
}
