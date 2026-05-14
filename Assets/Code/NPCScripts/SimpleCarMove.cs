using UnityEngine;

public class SimpleCarMove : MonoBehaviour
{
    [Header("Настройки движения")]
    public Transform[] waypoints; // Массив точек маршрута
    public float speed = 5f;      // Скорость машины

    [Header("Анимация")]
    public Animator anim;         // Ссылка на компонент Animator

    private int currentPointIndex = 0; // Индекс текущей точки

    void Update()
    {
        // Если точек нет — ничего не делаем
        if (waypoints.Length == 0)
        {
            UpdateAnimation(Vector2.zero, 0); // Сообщаем аниматору, что стоим
            return;
        }

        // Определяем текущую цель
        Transform target = waypoints[currentPointIndex];

        // 1. Вычисляем направление к цели
        Vector2 direction = (target.position - transform.position).normalized;

        // 2. Передаем направление и текущую скорость в аниматор
        UpdateAnimation(direction, speed);

        // 3. Двигаем машину к цели
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 4. Проверяем, доехали ли мы
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= waypoints.Length)
            {
                currentPointIndex = 0;
            }
        }
    }

    // Метод для управления параметрами аниматора
    void UpdateAnimation(Vector2 dir, float currentSpeed)
    {
        if (anim == null) return;

        // Передаем скорость для контроля анимации стоянки (Idle)
        anim.SetFloat("Speed", currentSpeed);

        if (currentSpeed > 0.01f)
        {
            // Используем порог, чтобы определить основное направление
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                // Едем по горизонтали: жестко задаем 1 или -1
                float h = dir.x > 0 ? 1f : -1f;
                anim.SetFloat("Horizontal", h);
                anim.SetFloat("Vertical", 0f);
            }
            else
            {
                // Едем по вертикали: жестко задаем 1 или -1
                float v = dir.y > 0 ? 1f : -1f;
                anim.SetFloat("Horizontal", 0f);
                anim.SetFloat("Vertical", v);
            }
        }
    }
}
