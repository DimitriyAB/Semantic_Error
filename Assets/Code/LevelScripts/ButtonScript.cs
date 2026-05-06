using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Animator anim;
    public GameObject frame;
    public GameObject[] otherFrames;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Кто-то вошел в триггер: " + other.name);


        if (other.CompareTag("Player"))
        {
            // ПРОВЕРКА: Если аниматор уже уничтожен, ничего не делаем
            if (anim != null)
            {
                anim.SetTrigger("isTriggered");
            }

            if (frame != null)
            {
                frame.SetActive(true);
            }

            // Исправлено имя переменной в цикле (f вместо frame), 
            // чтобы не путать с публичной переменной выше
            foreach (GameObject f in otherFrames)
            {
                if (f != null)
                {
                    f.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ПРОВЕРКА: Важнейшее место, где обычно вылетает ошибка
            if (anim != null)
            {
                anim.SetTrigger("isTriggered");
            }
        }
    }
}
