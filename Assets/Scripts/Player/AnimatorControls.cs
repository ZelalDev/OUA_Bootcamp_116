using System;
using UnityEngine;
using TMPro;

namespace Player
{
    public class AnimatorControls : MonoBehaviour
    {
        private Animator _anim;
        private GameObject _key;
        [SerializeField] private TextMeshProUGUI _itemInfoText;
        [SerializeField] private GameObject _itemHolder; // Item holder referansı

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
                        TakeItem(hit.collider.gameObject);
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
                _itemInfoText.text = "";
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

        void TakeItem(GameObject item)
        {
            // Item'i item holder'a parent olarak ata
            item.transform.SetParent(_itemHolder.transform);

            // Item'in yerel pozisyonunu ve rotasyonunu ayarla
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            Outline outline = item.GetComponent<Outline>();
            if (outline != null)
            {
                Destroy(outline);
            }

            _key.SetActive(true); // Anahtar gibi bir şeyin etkinleştirilmesi

            // Burada gerekirse başka işlemler yapılabilir (örneğin, itemin etkileşimini veya bilgilerini güncellemek)
        }
    }
}
