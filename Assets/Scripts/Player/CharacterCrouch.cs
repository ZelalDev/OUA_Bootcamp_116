using UnityEngine;

public class CharacterCrouch : MonoBehaviour
{
    public float crouchHeight = 1.0f;
    public float normalHeight = 2.0f;
    public float crouchSpeed = 5.0f;
    private CapsuleCollider characterCollider;
    private bool isCrouching = false;

    void Start()
    {
        characterCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        float targetHeight = isCrouching ? crouchHeight : normalHeight;
        characterCollider.height = Mathf.Lerp(characterCollider.height, targetHeight, Time.deltaTime * crouchSpeed);

        Vector3 scale = transform.localScale;
        scale.y = characterCollider.height / normalHeight;
        transform.localScale = scale;
    }
}