using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float max_speed;
    public float jump_power;
    bool is_jump = false;
    bool is_damaged = false;

    public GameManager gameManager;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // 키 떼면 멈춤
        if (Input.GetButtonUp("Horizontal") && !is_damaged)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
        // 점프

        if (Input.GetButtonDown("Jump") && !is_jump && !is_damaged)
        {
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            is_jump = true;
        }


        // 걷기 애니메이션
        animator.SetInteger("Horizon", (int)Input.GetAxisRaw("Horizontal"));
        if (animator.GetInteger("Horizon") == -1)
        {
            spriteRenderer.flipX = true;
        }
        else if (animator.GetInteger("Horizon") == 1)
        {
            spriteRenderer.flipX = false;
        }

        // 점프 애니메이션
        animator.SetBool("IsJump", is_jump);



    }

    void FixedUpdate()
    {
        // 이동
        if (!is_damaged)
        {
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > max_speed)
                rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
            if (rigid.velocity.x < max_speed * (-1))
                rigid.velocity = new Vector2(max_speed * (-1), rigid.velocity.y);
        }


        // 레이 캐스트(2단점프 방지)
        if (rigid.velocity.y < 0 && is_jump)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 0.5f, LayerMask.GetMask("PlatForm"));

            if (rayhit.collider != null)
            {
                is_jump = false;
            }
        }

        // 레이 캐스트 공격 스크립트(y속도가 들쭉날쭉함)
        if (gameObject.layer == 6 && rigid.velocity.y < 0)
        {
            RaycastHit2D attck_ray = Physics2D.Raycast(rigid.position, Vector3.down, 0.6f, LayerMask.GetMask("Enemy"));
            if (attck_ray.collider != null)
            {
                Attack(attck_ray.collider.transform);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnDameged(collision.transform.position);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.name.Contains("Bronze"))
                gameManager.itemScore += 50;
            if (collision.gameObject.name.Contains("Silver"))
                gameManager.itemScore += 100;
            if (collision.gameObject.name.Contains("Gold"))
                gameManager.itemScore += 300;
            collision.gameObject.SetActive(false);
        }
    }

    // 피격 이벤트
    void OnDameged(Vector2 targetPos)
    {
        gameManager.HpDown();

        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        animator.SetTrigger("Damaged");
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc * 8, 5), ForceMode2D.Impulse);
        is_damaged = true;

        Invoke("MoveReStart", 0.5f);
        Invoke("OffDameged", 3);
    }

    // 피격후 무적 해제 함수
    void OffDameged()
    {
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1);
    }

    // 경직 해제
    void MoveReStart()
    {
        is_damaged = false;
    }

    void Attack(Transform enemy)   // 충돌 오브젝트의 트랜스폼
    {
        // 적 스크립트의 함수 불러오기
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        EnemyMove enemymove = enemy.gameObject.GetComponent<EnemyMove>();
        enemymove.EnemyDamaged();
    }

    // 죽음
    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        capsuleCollider.enabled = false;
        spriteRenderer.flipY = true;
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}