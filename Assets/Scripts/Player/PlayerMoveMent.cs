using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float nomal_JumpFurce = 5.0f;
    [SerializeField] float up_JumpFurce = 8.0f;
    [SerializeField] float delay_Count = 2f;
    public Rigidbody2D player_Rigidbody;
    public Collider2D player_Collider;

    public bool isGround;
    public bool isJump;
    public bool isDown = false;

    private void Start()
    {
        player_Rigidbody = GetComponent<Rigidbody2D>();
        player_Collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        

        float HrizontalInput = Input.GetAxis("Horizontal");

        Vector3 distans = new Vector3(HrizontalInput,0,0).normalized * speed * Time.deltaTime;
        transform.position += distans;


        // 점프 
        if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftAlt) && isGround && !isJump)
        {
            player_Rigidbody.AddForce(Vector2.up * up_JumpFurce, ForceMode2D.Impulse);
            Debug.Log("높은 점프");
            JumpSystem();
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftAlt) && isGround && !isJump)
        {
            isDown = true;
        }

        else if(Input.GetKey(KeyCode.LeftAlt) && isGround && !isJump)
        {
            if (!isDown)
            {
                player_Rigidbody.AddForce(Vector2.up * nomal_JumpFurce, ForceMode2D.Impulse);
                Debug.Log("일반 점프");
                JumpSystem();
            }
            else if(isDown)
            {
                Debug.Log("아래로 이동");
                JumpSystem();
            }
        }
    }

    void JumpSystem()
    {
        isJump = true;
        isGround = false;
        StartCoroutine(EnableColliderAfterDelay(delay_Count));
    }

    IEnumerator EnableColliderAfterDelay(float delay)
    {
        player_Collider.enabled = false;
        yield return new WaitForSeconds(delay);
        player_Collider.enabled = true;
    }

    // 착지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isJump = false;
            isDown = false;
            Debug.Log("착지");
        }
    }
}
