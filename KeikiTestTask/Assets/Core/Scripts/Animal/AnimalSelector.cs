using UnityEngine;
using UnityEngine.Events;

namespace TailAttach.Animals
{
    public class AnimalSelector : MonoBehaviour
    {
        [SerializeField] private Animal[] animals = default;
        [SerializeField] private SoundEvent failSound = default;
        [SerializeField] private SoundEvent successSound = default;
        [SerializeField] private AudioSource audioSource = default;
        private int currentIndex = 0;
        public UnityEvent onAnimalChangeStart = new UnityEvent();
        public UnityEvent onAnimalChangeComplete = new UnityEvent();
        public UnityEvent onWrongTail = new UnityEvent();
        public UnityEvent onFinish = new UnityEvent();
        public Animal CurrentAnimal
        {
            get
            {
                if (currentIndex < animals.Length)
                {
                    return animals[currentIndex];
                }
                return null;
            }
        }
        private void Awake()
        {
            currentIndex = 0;
            CurrentAnimal.Appear(NextAnimal);
            foreach (var item in animals)
            {
                item.ResetSadness();
            }
        }
        public void Initialize()
        {
            currentIndex = 0;
            CurrentAnimal.Appear(NextAnimal);
            CurrentAnimal.SayTask();
            foreach (var item in animals)
            {
                item.ResetSadness();
            }
        }
        public void HandleTailSelect(Animal animal, Sprite sprite)
        {
            if (CurrentAnimal == animal)
            {
                onAnimalChangeStart?.Invoke();
                CurrentAnimal.Accept();
                SuccessSound();
                CurrentAnimal.HideHint();
            }
            else
            {
                onWrongTail?.Invoke();
                CurrentAnimal.AttachFalseTail(sprite);
                FailSound();
            }
        }
        public void FailSound() => failSound.Play(audioSource);
        public void SuccessSound() => successSound.Play(audioSource);
        public void NextAnimal()
        {
            currentIndex++;
            onAnimalChangeComplete?.Invoke();
            if (currentIndex == animals.Length)
            {
                Finish();
                return;
            }
            CurrentAnimal.Appear(NextAnimal);
            CurrentAnimal.SayTask();
        }
        void Finish()
        {
            onFinish.Invoke();
        }

    }
}