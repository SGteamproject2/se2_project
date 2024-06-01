using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject player;
    public GameObject LimitRight, LimitLeft;
    public Camera MinimapCamera;
    public float speed;

    // ī�޶� ������Ʈ �Լ�
    public void Camera_Update()
    {
        Camera_MovetoPlayer();
        Camera_MoveLimit();
    }

    // ī�޶��� ��ġ�� �÷��̾�� �����ϴ� �Լ�
    private void Camera_MovetoPlayer()
    {
        transform.position = new Vector3(Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime).x, Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime).y, -10);
        MinimapCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    // ī�޶��� �̵��� �����ϴ� �Լ�
    // �� ������ ī�޶� ������ �ʰԲ� �����ϴ� ���
    private void Camera_MoveLimit()
    {
        float ClampX = Mathf.Clamp(transform.position.x, LimitLeft.transform.position.x + Camera.main.orthographicSize * 1.8f, LimitRight.transform.position.x - Camera.main.orthographicSize * 1.8f);
        float ClampX_mini = Mathf.Clamp(MinimapCamera.transform.position.x, LimitLeft.transform.position.x + MinimapCamera.orthographicSize, LimitRight.transform.position.x - MinimapCamera.orthographicSize);

        transform.position = new Vector3(ClampX, transform.position.y, -10);
        MinimapCamera.transform.position = new Vector3(ClampX_mini, transform.position.y, -10);
    }
}
