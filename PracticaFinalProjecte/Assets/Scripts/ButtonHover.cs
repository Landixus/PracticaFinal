
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Vector3 cachedScale;

    public float tamanyFinal = 1.1f;
    public float step = 0.09f;

    void Start()
    {

        cachedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tamanyFinal < 1)
        {
            StartCoroutine("Shrink");
        } else {
            StartCoroutine("Enlarge");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        transform.localScale = cachedScale;
    }

    IEnumerator Enlarge()
    {
        for (float f = 1f; f <= tamanyFinal; f += step)
        {
            transform.localScale = new Vector3(f, f, f);
            yield return null;
        }
    }
    IEnumerator Shrink()
    {
        Debug.Log(tamanyFinal);
        for (float f = 1f; f >= tamanyFinal; f -= step)
        {
            transform.localScale = new Vector3(f, f, f);
            yield return null;
        }
    }

}
