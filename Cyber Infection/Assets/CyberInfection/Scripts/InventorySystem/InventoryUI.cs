using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using InventorySystem;
using CyberInfection.GameMechanics.Entity.Units;

public class InventoryUI : MonoBehaviour
{
    public CanvasGroup myCG;
    public List<InventoryItem> items;
    public AudioClip inventoryClip;
    public CanvasGroup wrongItemCG;
    public Text wrongItemText;
    private bool isInit = false;

    private void Init()
    {
        wrongItemText.text = "Interface:WrongItem";
        gameObject.SetActive(true);
        isInit = true;
    }

    public void CloseInventoryClick()
    {
        OpenInventory(false);
    }

    public void ClickAway()
    {
        OpenInventory(false);
    }
    Coroutine routineAppear = null;
    public void OpenInventory(bool _isOpen)
    {
        if (_isOpen)
        {
            if (!isInit)
                Init();
            int itemCount = 0;
            for (int i = 0; i < Player.instance.inventory.items.Count; i++)
            {
                items[i].SetItem(Player.instance.inventory.items[i]);
                itemCount++;
            }
            if (itemCount < items.Count)
            {
                for (int i = itemCount; i < items.Count; i++)
                {
                    items[i].SetItem(null);
                }
            }
            if (routineAppear != null)
                StopCoroutine(routineAppear);
            routineAppear = StartCoroutine(Appearing(true));
        }
        else
        {
            if (routineAppear != null)
                StopCoroutine(routineAppear);
            routineAppear = StartCoroutine(Appearing(false));
        }
    }

    public bool mistakePossible = true;

    IEnumerator Appearing(bool _isOpen)
    {
        myCG.blocksRaycasts = false;
        mistakePossible = true;
        if (_isOpen)
        {
            myCG.alpha = 0;
            while (myCG.alpha < 1)
            {
                myCG.alpha += Time.deltaTime * (1 / 0.2f);
                yield return null;
            }
            myCG.blocksRaycasts = true;
        }
        else
        {
            while (myCG.alpha > 0)
            {
                myCG.alpha -= Time.deltaTime * (1 / 0.2f);
                yield return null;
            }
        }
        routineAppear = null;
    }

    public void ItemPressed(Item _item)
    {
    }

    private Coroutine wrongItemCor = null;
    [ContextMenu("WrongItem")]
    public void WrongItem()
    {
        if (wrongItemCor != null)
        {
            StopCoroutine(wrongItemCor);
            wrongItemCor = null;
        }
        wrongItemCor = StartCoroutine(ShowingWrongItem());
    }

    IEnumerator ShowingWrongItem()
    {
        wrongItemCG.alpha = 0;
        while (wrongItemCG.alpha < 1)
        {
            wrongItemCG.alpha += Time.deltaTime * (1 / 0.5f);
            yield return null;
        }
        while (wrongItemCG.alpha > 0)
        {
            wrongItemCG.alpha -= Time.deltaTime * (1 / 0.5f);
            yield return null;
        }
    }
}
