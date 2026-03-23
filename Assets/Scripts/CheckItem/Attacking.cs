using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// - Tính toán và điều khiển giả lập hướng tấn công (sử dụng tất cả vũ khí) 
/// - rest trạng thái của weapon mỗi khi thay đổi parent (chi Class weapon)
/// </summary>
public class Attacking : MonoBehaviour
{
    private Transform parentObject; // Tham chiếu đến transform của parent
    [SerializeField] AudioClip attackingClip; // Đặt tên biến theo quy tắc viết thường

    private Vector2 lastObjectPosition; // Lưu trữ vị trí trước đó của parent

    private void Start()
    {
        parentObject = transform.parent;// Tìm parent
        lastObjectPosition = parentObject.position;// lưu vị trí ban đầu
    }
    private void Update()
    {
        CheckNullAndUpdate();
        //Debug.LogWarning($"{gameObject.name} có Cha là: {parentObject.name}");
        CheckParentMovementDirection();
        ResetStatus();
    }
    private void CheckNullAndUpdate()
    {
        
        if (transform.parent != parentObject)// Cập nhật lại tham chiếu parentObject nếu nó thay đổi
        {
            parentObject = transform.parent;
        }
        if (parentObject == null)
        {
            Debug.LogError("Parent không được tìm thấy cho Attacking script.");
            return;
        }
        if (parentObject.name != "UseItem")
        {
            gameObject.SetActive(false);
        }
    }
    private void CheckParentMovementDirection() // Tính toán hướng di chuyển theo parent
    {
        if (parentObject != null)
        {
            Vector2 currentObjectPosition = parentObject.position;
            Vector2 movementDirection = currentObjectPosition - lastObjectPosition;

            if (movementDirection != Vector2.zero) // Nếu parent đang di chuyển
            {
                RotateAttackingDirection(movementDirection); // Xoay theo hướng di chuyển
            }
            lastObjectPosition = currentObjectPosition;
        }
    }
    private void RotateAttackingDirection(Vector2 direction) // Tính toán và xoay theo hướng của parent
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void ResetStatus() // reset trạng thái weapon khi thay đổi parent ( chỉ lớp weapon)
    {
        if (parentObject != null)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                // Đặt lại vị trí của weapon về 0,0,0 theo parent
                transform.localPosition = Vector3.zero;

                if (parentObject.name == "UseItem") // Nếu parent là UseItem, không reset rotation
                {
                    //Debug.Log("Đặt lại trạng thái weapon trong: " + parentObject.name);
                }
                else // Reset rotation
                {
                    transform.rotation = parentObject.rotation;
                    //Debug.Log("Reset trạng thái weapon ngoài : UseItem");
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // (chưa dùng)
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("wall"))
        {
            //AudioSource.PlayClipAtPoint(attackingClip, player.position);
        }
    }
}
