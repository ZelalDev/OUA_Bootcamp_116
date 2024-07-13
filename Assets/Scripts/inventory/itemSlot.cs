using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour//, IPointerClickHandler
{
// item Data //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
// item slot //
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

// item description //
    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;
    public GameObject selectedShader;
    public bool thisItemSelected;

    private inventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("inventoryCanvas").GetComponent<inventoryManager>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        Debug.Log("Adding item: " + this.itemName + ", quantity: " + this.quantity + ", sprite: " + this.itemSprite);

        // Update UI text for quantity
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        // Update UI image for item
        itemImage.sprite = this.itemSprite;
        

        // Debug logs for verification
        Debug.Log("itemImage.sprite: " + itemImage.sprite);
        Debug.Log("itemImage is null: " + (itemImage == null));
        Debug.Log("itemImage.sprite: " + itemDescriptionImage.sprite);
        Debug.Log("itemImage is null: " + (itemDescriptionImage == null));

        // Add any additional UI update logic here, if needed
    }




    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        ItemDescriptionNameText.text = itemName;
        ItemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }

    public void OnRightClick()
    {
    }
}