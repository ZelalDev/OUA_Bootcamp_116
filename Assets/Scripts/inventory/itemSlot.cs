using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class itemSlot : MonoBehaviour, IPointerClickHandler
{
    // item Data //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    
    
    // item slot //
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    
    public GameObject selectedShader;
    public bool thisItemSelected;

    private inventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("inventoryCanvas").GetComponent<inventoryManager>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;  
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
    }

    public void OnRightClick()
    {
        
    }
}
