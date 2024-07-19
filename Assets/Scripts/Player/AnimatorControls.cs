using System;
using UnityEngine;
using TMPro;

namespace Player
{
    public class AnimatorControls : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemInfoText;

        [SerializeField] private LayerMask itemLayer;
        private inventoryManager inventoryManager;

        void Start()
        {
            // inventoryManager referansını al
            GameObject inventoryCanvas = GameObject.Find("inventoryCanvas");
            if (inventoryCanvas != null)
            {
                inventoryManager = inventoryCanvas.GetComponent<inventoryManager>();
                Debug.Log("inventoryManager initialized: " + (inventoryManager != null));
            }
            else
            {
                Debug.LogError("inventoryCanvas not found in the scene!");
            }
        }

        private void Update()
        {
            RaycastForItem();
        }

        void RaycastForItem()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f, itemLayer))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    var item = hit.collider.GetComponent<Item>();
                    if (item != null)
                    {
                        _itemInfoText.text = item.itemName;
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Debug.Log("Item picked up: " + item.itemName);
                            inventoryManager.AddItem(item.itemName, item.quantity, item.sprite, item.itemDescription);
                            Destroy(hit.collider.gameObject); // İtemi sahneden kaldır
                        }
                    }
                }
            }
            else
            {
                _itemInfoText.text = "";
            }
        }
    }
}