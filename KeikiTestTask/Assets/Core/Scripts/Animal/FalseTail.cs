using System.Collections;
using UnityEngine;

namespace TailAttach.Animals
{
    public class FalseTail : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 0.2f;
        [SerializeField] private SpriteRenderer sr = default;

        private Transform target;

        public void AttachFalseTail(Transform target, Sprite sprite, System.Action callback)
        {
            sr.sprite = sprite;
            this.target = target;
            ReplicateTargetTransform();
            Appear();
            void Appear()
            {
                gameObject.SetActive(true);
                StopAllCoroutines();
                var routine = StartCoroutine(sr.ColorTween(new Color(1, 1, 1, 0), Color.white, fadeTime));
                StartCoroutine(routine.TweenCallback(Attach));
            }
            void Attach()
            {
                transform.position = target.position;
                StartCoroutine(FollowRoutine());
                callback?.Invoke();
            }
        }

        private void ReplicateTargetTransform()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        private IEnumerator FollowRoutine()
        {
            while (isActiveAndEnabled)
            {
                ReplicateTargetTransform();
                yield return new WaitForEndOfFrame();
            }
        }

        public void Disappear()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }
            StopAllCoroutines();
            var routine = StartCoroutine(sr.ColorTween(sr.color, new Color(1, 1, 1, 0), fadeTime));
            StartCoroutine(routine.TweenCallback(Deactivate));
            void Deactivate()
            {
                gameObject.SetActive(false);
            }

        }
    }
}