using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    // 게임 재화
    public int Gold_Key;
    public int Silver_Key;

    void Start()
    {
        
    }

    void Update()
    {
        UIManager.Instance.UI_Update(Gold_Key,Silver_Key);
        CameraMovement.Instance.Camera_Update();
        Player.Instance.Player_Update();
    }

    public void Suc_Quiz()
    {
        Gold_Key++;
    }

    public void Use_Hint()
    {

    }
}
