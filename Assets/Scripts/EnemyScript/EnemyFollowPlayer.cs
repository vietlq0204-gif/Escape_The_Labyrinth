using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour  
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float visionRadius = 3f; // bán kính tầm nhìn của địch
    private GameObject player;

    [SerializeField] private bool hasLineOfSight = false;
    [SerializeField] private LayerMask ignoreLayers; // Layer mask để bỏ qua các layer không cần thiết
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveFollowGameObject();
    }

    private void FixedUpdate()
    {
        CheckRaycast();
    }
    public void MoveFollowGameObject()
    {
        if (player != null)
        {
            if (hasLineOfSight)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // đi theo player
            }
            else
            {
                return;
            }
        }

    }
    public void CheckRaycast()
    {
        if (player != null)
        {
            // Kiểm tra khoảng cách giữa địch và người chơi
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= visionRadius) // Nếu người chơi nằm trong tầm nhìn, tiến hành kiểm tra raycast
            {
                //RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
               
                RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position,
                    Mathf.Infinity, ~ignoreLayers); // Raycast với layer mask để bỏ qua các layer cụ thể

                if (ray.collider != null)
                {
                    Debug.Log("Raycast hit: " + ray.collider.gameObject.name);  // Hiển thị tên của đối tượng mà tia đang chạm vào.

                    hasLineOfSight = ray.collider.CompareTag("Player");
                    if (hasLineOfSight)
                    {
                        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                    }
                }
            }
            else
            {
                // Nếu người chơi không nằm trong tầm nhìn
                hasLineOfSight = false;
            }

        }

    }
    private void OnDrawGizmos() // Vẽ vùng bán kính tầm nhìn của địch (test game)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}
