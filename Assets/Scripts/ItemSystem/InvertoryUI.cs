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
            print("tryingCloseUI");
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

        //Dictionary<GameObject, GameObject> invent = new Dictionary<GameObject, GameObject>();

        foreach(GameObject item in inentory.backpack)
        {
            GameObject _item = Instantiate(itemUIPrefab);
            _item.GetComponent<RectTransform>().SetParent(content);
            //invent.Add(_item, item);

            ItemUI itemUi      = _item.GetComponent<ItemUI>();
            itemUi.inventory   = inentory;
            itemUi.item        = item.GetComponent<Item>();

        }
        //print("Dictionary have" + invent.Count);
    }

    public void HideInventoryUI()
    {
        Transform[] objs = content.GetComponentsInChildren<Transform>();
        invUI.SetActive(false);
        for (int i=1; i<=objs.Length; i++)
        {
            Destroy(objs[i].gameObject);
        }
    }
}
