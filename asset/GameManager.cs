using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Hp;
    public int itemScore;
    public int maxScore;

    public PlayerMove player;

    // 체력 감소
    public void HpDown()
    {
        if (Hp > 1)
            Hp--;
        else
        {
            player.OnDie();
        }
    }

    // collider는 안되는가보다 (해보니 안댐)
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HpDown();
            if (Hp > 0)
            {
                PlayerRePosition();
            }
        }
    }

    // 재위치
    void PlayerRePosition()
    {
        player.transform.position = new Vector3(-5, 0, 0);
        player.VelocityZero();
    }
}