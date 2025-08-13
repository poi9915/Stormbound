// using UnityEngine;
// using UnityEngine.EventSystems;

// public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
// {
//     Vector3 startScale;

//     void Start() => startScale = transform.localScale;

//     public void OnPointerEnter(PointerEventData eventData)
//     {
//         LeanTween.scale(gameObject, startScale * 1.1f, 0.1f).setEaseOutBack();
//     }

//     public void OnPointerExit(PointerEventData eventData)
//     {
//         LeanTween.scale(gameObject, startScale, 0.1f).setEaseInBack();
//     }
// }
