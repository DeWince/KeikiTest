using TailAttach.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace TailAttach
{
    public class HintPointer : Hint
    {
        [SerializeField] private InteractionController interactionController = default;
        [SerializeField] private float fadeTime = 0.3f;
        [SerializeField] private float scaleFactor = 1.2f;
        [SerializeField] private float scaleTime = 1f;
        [SerializeField] private AnimationCurve curve = new AnimationCurve();
        [SerializeField] private Image image = default;
        public override void Show()
        {
            transform.position = interactionController.FindCorrectButton().transform.position;
            Appear();
        }
        public override void Hide()
        {
            Disappear();
        }
        private void Appear()
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(image.ColorTween(new Color(1, 1, 1, 0), Color.white, fadeTime));
            StartCoroutine(transform.ScaleTween(Vector3.one, Vector3.one * scaleFactor, scaleTime, curve, true));
        }
        private void Disappear()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }
            var routine = StartCoroutine(image.ColorTween(image.color, new Color(1, 1, 1, 0), fadeTime));
            StartCoroutine(routine.TweenCallback(() => gameObject.SetActive(false)));
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}