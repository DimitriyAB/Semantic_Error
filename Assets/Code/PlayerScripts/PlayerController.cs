using System.Collections.Generic; // Нужен для List
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator; 
    public float speed = 2f;
    public VectorValue pos;

    private Rigidbody2D rb;

    // --- Новая логика для отслеживания последнего ввода ---
    private List<Vector2> inputStack = new List<Vector2>();
    private Vector2 currentDirection = Vector2.zero;
    

    void Start()
    {
        transform.position = pos.initialValue;

        rb = GetComponent<Rigidbody2D>();
        // Если animator не назначен в инспекторе, пробуем найти его на объекте
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        // Можно добавить проверку, если Animator все равно не найден
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name + "!");
        }
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        HandleInput();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        // --- Заменяем Input.GetAxis на новую логику ---
        // Добавляем направление при нажатии
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) AddDirection(Vector2.up);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) AddDirection(Vector2.down);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) AddDirection(Vector2.left);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) AddDirection(Vector2.right);

        // Удаляем направление при отпускании
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) RemoveDirection(Vector2.up);
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) RemoveDirection(Vector2.down);
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) RemoveDirection(Vector2.left);
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) RemoveDirection(Vector2.right);

        // Определяем текущее направление: это всегда последний элемент в стеке
        if (inputStack.Count > 0)
        {
            currentDirection = inputStack[inputStack.Count - 1];
        }
        else
        {
            currentDirection = Vector2.zero;
        }
        // --- Конец новой логики ввода ---
    }
    void AddDirection(Vector2 dir)
    {
        // Добавляем, только если такого направления еще нет в списке
        if (!inputStack.Contains(dir))
        {
            inputStack.Add(dir);
        }
    }

    void RemoveDirection(Vector2 dir)
    {
        if (inputStack.Contains(dir))
        {
            inputStack.Remove(dir);
        }
    }

    void UpdateAnimation()
    {
        // Обновляем параметры аниматора на основе текущего направления
        // Если персонаж движется (currentDirection не ноль)
        if (currentDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", currentDirection.x);
            animator.SetFloat("Vertical", currentDirection.y);
            // Используем sqrMagnitude для скорости, чтобы избежать квадратного корня
            // Она просто показывает, движемся ли мы вообще (больше 0)
            animator.SetFloat("Speed", currentDirection.sqrMagnitude);
        }
        else // Если персонаж стоит (currentDirection == 0)
        {
            
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
                animator.SetFloat("Speed", 0);
            
            
        }



        // --- Дополнительно: Поворот спрайта ---
        // Если вам нужен поворот спрайта (как в предыдущем примере)
        // и вы не используете FlipX, раскомментируйте этот блок.
        // Убедитесь, что у персонажа нет дочерних объектов, которые тоже будут переворачиваться.
        /*
        if (currentDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (currentDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        */
    }

    public void ResetInput()
    {
        inputStack.Clear();
        currentDirection = Vector2.zero;

        // Также обнуляем анимацию, чтобы персонаж не "бежал" на месте после паузы
        if (animator != null)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
    }


    void Move()
    {
        // Используем Rigidbody2D для движения, это лучше для физики
        // currentDirection.normalized гарантирует, что скорость будет постоянной,
        // даже если движемся по диагонали (что тут исключено, но для хорошей практики)
        rb.MovePosition(rb.position + currentDirection.normalized * speed * Time.fixedDeltaTime);
    }
}

