using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // ��� �����
    public Vector3 offset;     // ��������� ������� ������
    public float speed = 5f;   // �������� ����������

    void LateUpdate()
    {
        if (target == null) return;

        // ����� ������� � ��� ������� ������ + ��������
        Vector3 targetPosition = target.position + offset;

        // ������ ������� ������ � ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
