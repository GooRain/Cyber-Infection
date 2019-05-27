using System.Collections;
using CyberInfection.UI.Game;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;
using System;
using CyberInfection.GameMechanics.Entity.Units;

public class NewItemWindow : MonoBehaviour
{
    private CanvasGroup bgBlack;
    public GameObject windowObject;
    public CanvasGroup myCG;
    public float windowFadeTime;
    public Text infoText, nameText, okButtonText;
    public Image itemIcon;
    public Button okButton;
    public bool windowOnUI = false;
    private bool isInit = false;

    public EventHandler<EventArgs> OnWindowDisapeared;

    void Init()
    {
        bgBlack = GameCanvas.instance.bgBlack;
        okButtonText.text = "Interface:OK";
        isInit = true;
    }

    public void ShowQuestItem(string _itemId)
    {
        var item = ItemAsset.GetItem(_itemId);
        Sprite icon = item.icon;
        string name = item.name.ToUpper();
        Player.instance.inventory.AddItem(item);
        ActivateWindow(icon, name);
    }
    public void ActivateWindow(Sprite _icon, string _name)
    {
        if (!isInit)
            Init();
        if (windowOnUI)
            return;
        Player.instance.blockers.Add("NewItemWindow");
        infoText.text = "Interface:GotItem";
        itemIcon.sprite = _icon;
        nameText.text = _name;
        myCG.alpha = 0;
        windowObject.SetActive(true);
        StartCoroutine(WindowAppear());
    }

    IEnumerator WindowAppear()
    {
        okButton.Select();
        while (myCG.alpha < 1f)
        {
            myCG.alpha += Time.deltaTime * (1 / windowFadeTime);
            yield return null;
        }
    }

    public void EndWindow()
    {
        if (!windowOnUI)
        {
            Debug.LogError("Window is already not working");
            return;
        }
        StopAllCoroutines();
        StartCoroutine(WindowDisappear());
        Player.instance.blockers.Remove("NewItemWindow");
    }

    IEnumerator WindowDisappear()
    {
        while (myCG.alpha > 0)
        {
            myCG.alpha -= Time.deltaTime * (1 / windowFadeTime);
            yield return null;
        }
        OnWindowDisapeared?.Invoke(this, EventArgs.Empty);
        windowObject.SetActive(false);
    }
}
