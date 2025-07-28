using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // Это игрок
    public Vector3 offset;     // Насколько отстает камера
    public float speed = 5f;   // Скорость следования

    void LateUpdate()
    {
        if (target == null) return;

        // Новая позиция — это позиция игрока + смещение
        Vector3 targetPosition = target.position + offset;

        // Плавно двигаем камеру к цели
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
