using UnityEngine;
using UnityEngine.UI;
using TailAttach.Animals;

namespace TailAttach.Interaction
{
    /// <summary>
    /// Should be initialized with InteractionController
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class TailButton : MonoBehaviour
    {
        [SerializeField] private ScaleTweener scaleTweener = default;
        [SerializeField] private Animal correspondingAnimal = default;
        [SerializeField] private float selectScale = 1.3f;
        [SerializeField] private float scaleTime = 0.3f;
        [SerializeField] private Material selectMaterial = default;
        [SerializeField] private Material normalMaterial = default;
        private Button button = default;
        private InteractionController interactionController;
        public Animal CorrespondingAnimal => correspondingAnimal;
        public Sprite Sprite => button.image.sprite;
        public void Initialize(InteractionController interactionController)
        {
            this.interactionController = interactionController;
            button = GetComponent<Button>();
            button.onClick.AddListener(SelectButton);
        }

        public void SelectButton()
        {
            if (interactionController)
            {
                button.image.material = selectMaterial;
                interactionController.SelectTail(this);
                if (interactionController.FindCorrectButton() == this)
                {
                    StopBlinking();
                    StartCoroutine(transform.ScaleTween(transform.localScale, Vector3.one * selectScale, scaleTime));
                }

            }
            else
            {
                Debug.LogWarning(name + " on " + gameObject.name + " Should be initialized with InteractionController");
            }

        }
        public void DeselectButton()
        {
            StartCoroutine(transform.ScaleTween(transform.localScale, Vector3.one, scaleTime));
            button.image.material = normalMaterial;
        }

        public void StartBlinking()
        {
            if (scaleTweener)
                scaleTweener.StartScalingPingPong();
        }
        public void StopBlinking()
        {
            if (scaleTweener)
                scaleTweener.StopScalingImediate();
        }
  
        public void LockButton()
        {
            button.interactable = false;
        }
        public void UnlockButton()
        {
            button.interactable = true;
        }
    }
}