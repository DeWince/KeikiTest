using System.Collections;
using System;
using UnityEngine;
using TailAttach.Animals;
using UnityEngine.EventSystems;

namespace TailAttach.Interaction
{
    public class InteractionController : MonoBehaviour, IPointerClickHandler
    {
        private const float InteractionAwaitTime = 7f;
        [SerializeField] private AnimalSelector animalSelector = default;
        [SerializeField] private TailButton[] tailButtons = default;
        private TailButton selectedButton = default;
        private Timer interactionTimer = new Timer(InteractionAwaitTime);
        private bool locked = false;

        public AnimalSelector AnimalSelector => animalSelector;

        public void Awake()
        {
            ForEachButton((button) => button.Initialize(this));
            AnimalSelector.onAnimalChangeStart.AddListener(LockAll);
            AnimalSelector.onAnimalChangeComplete.AddListener(UnlockAll);
            AnimalSelector.onWrongTail.AddListener(WrongTail);
        }
        public void Initialize()
        {
            StopInteractionAwait();
            StartInteractionAwait();
        }
        public void SelectTail(TailButton tailButton)
        {
            selectedButton = tailButton;
            AnimalSelector.HandleTailSelect(tailButton.CorrespondingAnimal, tailButton.Sprite);
        }
        public void DeselectButton()
        {
            selectedButton.DeselectButton();
        }
        public void LockAll()
        {
            locked = true;
            ForEachButton((button) => button.LockButton());
            StopInteractionAwait();
        }

        public void UnlockAll()
        {
            DeselectButton();
            locked = false;
            ForEachButton((button) => button.UnlockButton());
            StartInteractionAwait();
        }
        public void WrongTail()
        {
            DeselectButton();
            interactionTimer.TimeLeft = InteractionAwaitTime;
        }

        public void ForEachButton(Action<TailButton> tailAction)
        {
            foreach (var button in tailButtons)
            {
                tailAction?.Invoke(button);
            }
        }
        public TailButton FindCorrectButton()
        {
            return Array.Find(tailButtons, (x) => x.CorrespondingAnimal == AnimalSelector.CurrentAnimal);
        }

        private void StartInteractionAwait()
        {
            StopAllCoroutines();
            StartCoroutine(CountdownRoutine());
        }
        private void StopInteractionAwait()
        {
            StopAllCoroutines();
            AnimalSelector.CurrentAnimal.HideHint();
        }
        private IEnumerator CountdownRoutine()
        {
            if (locked)
            {
                yield break;
            }
            interactionTimer.TimeLeft = InteractionAwaitTime;
            while (!interactionTimer.Ready)
            {
                interactionTimer.TimeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
                if (interactionTimer.Ready)
                {
                    if (AnimalSelector.CurrentAnimal)
                    {
                        AnimalSelector.CurrentAnimal.ShowHint();
                    }
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector3.forward, 100);
            if (hit.collider)
            {
                Animal animal = hit.collider.GetComponent<Animal>();
                if (animal)
                {
                    animal.ReactOnClick();
                    
                }
            }
            animalSelector.FailSound();
        }
    }
}