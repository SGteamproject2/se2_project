using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;

    public int nextMove;

    public GameManager gameManager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Invoke("Move", 3);
    }

    void Update()
    {

        // 적 움직임 애니메이션
        animator.SetInteger("EnemyMove", nextMove);
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
    }

    void FixedUpdate()
    {
        // 적 움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 적 낭떨어지 방지
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D hit = Physics2D.Raycast(frontVec, Vector3.down, 1);
        if (hit.collider == null && !spriteRenderer.flipY)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Move", 3);
        }
    }

    // 적 움직임 함수
    void Move()
    {
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(2, 5);
        Invoke("Move", nextThinkTime);
    }

    // 적 죽음
    public void EnemyDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        capsuleCollider.enabled = false;
        spriteRenderer.flipY = true;
        gameManager.itemScore += 100;
        Invoke("DeActive", 2);
    }

    // 비활성화
    void DeActive()
    {
        gameObject.SetActive(false);
    }
}