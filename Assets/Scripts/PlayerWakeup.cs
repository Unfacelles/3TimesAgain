using UnityEngine;

public class PlayerWakeup : MonoBehaviour
{
    private Animator animator;
    private bool hasWokenUp = false;
    private PlayerMovement movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        movement.canMove = false; // отключаем движение в начале
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasWokenUp)
        {
            animator.SetTrigger("WakeUpTrig"); // Триггер срабатывает по пробелу
            hasWokenUp = true;

            // Временный обход, если Animation Event не работает:
            // Закомментируй строку ниже, если анимация уже вызывает EnableMovement()
            Invoke(nameof(EnableMovement), 1f);
        }
    }

    // Вызывается из Animation Event в конце WakeUp
    public void EnableMovement()
    {
        Debug.Log("Движение разрешено!");
        movement.canMove = true;
    }
}
