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

        private Outline currentOutline; // Mevcut Outline bileşeni

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

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("item"))
                {
                    _itemInfoText.text = hit.collider.name;

                    // Mevcut Outline bileşenini kontrol et ve ekle
                    Outline outline = hit.collider.gameObject.GetComponent<Outline>();
                    if (outline == null)
                    {
                        outline = hit.collider.gameObject.AddComponent<Outline>();
                        outline.OutlineMode = Outline.Mode.OutlineAll;
                        outline.OutlineColor = Color.white;
                        outline.OutlineWidth = 2f;
                    }

                    currentOutline = outline;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        TakeItem();
                        Destroy(hit.collider.gameObject);
                        currentOutline = null; // Mevcut Outline bileşeni yok et
                    }
                }
                else
                {
                    _itemInfoText.text = "";
                    RemoveCurrentOutline();
                }
            }
            else
            {
                RemoveCurrentOutline();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                _anim.SetBool("isPicking", false);
            }
        }

        void RemoveCurrentOutline()
        {
            if (currentOutline != null)
            {
                Destroy(currentOutline);
                currentOutline = null;
            }
        }

        public void TakeItem()
        {
            _key.SetActive(true);
        }
    }
}
