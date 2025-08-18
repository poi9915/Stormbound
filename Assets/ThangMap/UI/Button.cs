using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    Vector3 baseScale;

    void Awake() => baseScale = transform.localScale;

    public void OnPointerEnter(PointerEventData e) => transform.localScale = baseScale * 1.06f;
    public void OnPointerExit(PointerEventData e)  => transform.localScale = baseScale;
    public void OnPointerDown(PointerEventData e)  => transform.localScale = baseScale * 0.96f;
    public void OnPointerUp(PointerEventData e)    => transform.localScale = baseScale * 1.06f;
}
