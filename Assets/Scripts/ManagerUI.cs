using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ManagerUI : MonoBehaviour
{
    public GameObject UIShop;
    public GameObject shopItems;
    public GameObject inventoryItems;
    public GameObject dialogueBox;
    public TextMeshProUGUI balance;

    public GameObject itemButtonPrefab;

    public GameObject curr_SelectedItem;

    // Start is called before the first frame update
    void Start()
    {
        balance.text = "Balance: " + GameManager.instance.playerBalance.ToString() + " Gold";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PopulatePanel(Sprite itemSprite, string type)
    {
        GameObject reference = null;

        if(type.Equals("shop")) reference = Instantiate(itemButtonPrefab, Vector2.zero, Quaternion.identity, shopItems.transform);
        if (type.Equals("inventory")) reference = Instantiate(itemButtonPrefab, Vector2.zero, Quaternion.identity, inventoryItems.transform);

        if (reference != null)
        {
            reference.name = "Item_" + itemSprite.name + "_" + type;
            reference.transform.GetChild(0).GetComponent<Image>().sprite = itemSprite;
            reference.GetComponent<Button>().onClick.AddListener(OnClick_ItemButton);
        }
        else
        {
            Debug.LogError("Missing Panel type");
        }
    }
    public void OnClick_ItemButton()
    {
        curr_SelectedItem = EventSystem.current.currentSelectedGameObject;
    }
    public void OnClick_BuyButton()
    {
        if (curr_SelectedItem != null && curr_SelectedItem.name.Contains("shop"))
        {
            var priceText = curr_SelectedItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Replace(" Gold", "");
            int itemPrice = Convert.ToInt32(priceText);

            if (GameManager.instance.playerBalance > itemPrice)
            {
                GameManager.instance.playerBalance -= itemPrice;
                balance.text = "Balance: " + GameManager.instance.playerBalance.ToString() + " Gold";

                PopulatePanel(curr_SelectedItem.transform.GetChild(0).GetComponent<Image>().sprite, "inventory");
            }

            Destroy(curr_SelectedItem);
        }
        else
        {
            dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Please select an item from the shop!";
        }
    }
    public void OnClick_SellButton()
    {
        if (curr_SelectedItem != null && curr_SelectedItem.name.Contains("inventory"))
        {
            var spriteName = curr_SelectedItem.transform.GetChild(0).GetComponent<Image>().sprite.name;

            if (!GameManager.instance.player.isEquiped(spriteName))
            {
                var priceText = curr_SelectedItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Replace(" Gold", "");
                int itemPrice = Convert.ToInt32(priceText);

                GameManager.instance.playerBalance += itemPrice;
                balance.text = "Balance: " + GameManager.instance.playerBalance.ToString() + " Gold";

                PopulatePanel(curr_SelectedItem.transform.GetChild(0).GetComponent<Image>().sprite, "shop");

                Destroy(curr_SelectedItem);
            }
            else
            {
                dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Cannot sell equiped items!";
            }
        }
        else
        {
            dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Please select an item from the inventory!";
        }
    }
    public void OnClick_EquipItem()
    {
        if (curr_SelectedItem != null && curr_SelectedItem.name.Contains("inventory"))
        {
            var spriteName = curr_SelectedItem.transform.GetChild(0).GetComponent<Image>().sprite.name;

            if (spriteName.Contains("Hair"))
            {
                GameManager.instance.player.EquipHair(spriteName);
            }
            if (spriteName.Contains("Dress"))
            {
                GameManager.instance.player.EquipDress(spriteName);
            }

            GameManager.instance.player.animator.SetTrigger("Wave");
        }
        else
        {
            dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Please select an item from the inventory!";
        }
    }
    public void OnClick_Continue()
    {
        UIShop.SetActive(true);
        dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Pick carefully, the items are very expensive...";
    }
}
