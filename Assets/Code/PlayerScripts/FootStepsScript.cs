using UnityEngine;

public class FootStepsScript : MonoBehaviour
{
    public GameObject footstep;

    void Update()
    {
        // Короткая проверка: нажата ли любая из клавиш?
        bool keyIsPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        footstep.SetActive(keyIsPressed);
    }
}
