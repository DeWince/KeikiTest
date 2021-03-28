using TailAttach.Interaction;
using UnityEngine;

namespace TailAttach
{
    public class HintBlinker : Hint
    {
        [SerializeField] protected InteractionController interactionController = default;

        public override void Show()
        {
            interactionController.ForEachButton((button) => button.StartBlinking());
            interactionController.AnimalSelector.CurrentAnimal.SayTask();
        }
        public override void Hide()
        {
            interactionController.ForEachButton((button) => button.StopBlinking());
        }
    }
}