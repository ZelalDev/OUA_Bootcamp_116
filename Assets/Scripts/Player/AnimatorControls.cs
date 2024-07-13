using System;
using UnityEngine;
using TMPro;

namespace Player
{
    public class AnimatorControls : MonoBehaviour
    {
        private Animator _anim;
        [SerializeField] private GameObject _key;
        [SerializeField] private TextMeshProUGUI _itemInfoText;
        
        [SerializeField] private string itemName;
        [SerializeField] private int quantity;
        [SerializeField] private Sprite sprite;
        [TextArea]
        [SerializeField] private string itemDescription;

        private inventoryManager inventoryManager;
        
        void Start()
        {
            inventoryManager = GameObject.Find("inventoryCanvas").GetComponent<inventoryManager>();
        }
        private void Awake()
        {

            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            RaycastSource();
        }

        public void TakeItem()
        {
            _key.SetActive(true);
        }
        
        void RaycastSource()
        {
            _anim.SetBool("isPicking", true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("item"))
                {
                    _itemInfoText.text = hit.collider.name;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        TakeItem();
                        inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
                        Destroy(hit.collider.gameObject);
                    }
                }else _itemInfoText.text = "";

            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                _anim.SetBool("isPicking", false);
            }
        }

        

    }
}