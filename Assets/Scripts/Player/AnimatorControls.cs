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
        
        private void Awake()
        {

            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            RaycastSource();
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
                        Destroy(hit.collider.gameObject);
                    }
                }else _itemInfoText.text = "";

                
            }
            

            if (Input.GetKeyUp(KeyCode.E))
            {
                _anim.SetBool("isPicking", false);
            }
        }

        public void TakeItem()
        {
            _key.SetActive(true);
        }

    }
}