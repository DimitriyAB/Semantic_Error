using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Animator anim;
    public GameObject frame;
    public GameObject[] otherFrames;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(" то-то вошел в триггер: " + other.name);


        if (other.CompareTag("Player"))
        {
            if (anim != null)
            {
                // »спользуем триггер ѕќ ј«ј“№
                anim.SetTrigger("ShowTrigger");
            }

            if (frame != null) frame.SetActive(true);

            foreach (GameObject f in otherFrames)
            {
                if (f != null) f.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (anim != null)
            {
                // »спользуем триггер — –џ“№
                anim.SetTrigger("HideTrigger");
            }
        }
    }
}
