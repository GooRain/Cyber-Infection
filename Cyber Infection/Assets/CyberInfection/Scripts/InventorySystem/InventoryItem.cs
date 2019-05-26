using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;

public class InventoryItem : MonoBehaviour
{
    public Button myButton;
    public Sprite icon;
    public Image outline;
    public Color inactiveOutlineColor;
    public Color activeOutlineColor;
    private Item myItem = null;

    public void SetItem(Item _item)
    {
        myItem = _item;
        if (_item != null)
        {
            icon = _item.icon;
            outline.color = activeOutlineColor;
            myButton.interactable = true;
        }
        else
        {
            icon = null;
            outline.color = inactiveOutlineColor;
            myButton.interactable = false;
        }
    }
}
