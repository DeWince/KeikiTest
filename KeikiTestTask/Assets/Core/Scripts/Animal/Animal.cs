using UnityEngine;
using Spine.Unity;
using UnityEngine.EventSystems;

namespace TailAttach.Animals
{
    public class Animal : MonoBehaviour
    {
        [SpineSlot]
        [SerializeField] private string tailSlotName = "Tail";
        [SpineAttachment]
        [SerializeField] private string attachment = "attachmentName";

        [SerializeField] private Hint animalHint = default;
        [SerializeField] private Transform attachmentPoint = default;
        [SerializeField] private FalseTail falseTail = default;
        [SerializeField] private SkeletonAnimation skeletonAnimation = default;
        [SerializeField] private SoundEvent taskSound = default;
        [SerializeField] private AudioSource audioSource = default;
        [SerializeField] private AnimationReferenceAsset idleAnimationRef = default;
        [SerializeField] private AnimationReferenceAsset joyAnimationRef = default;
        [SerializeField] private AnimationReferenceAsset refuseAnimationRef = default;
        [SerializeField] private AnimationReferenceAsset sadAnimationRef = default;
        [SerializeField] private AnimationReferenceAsset tapAnimationRef = default;
        private System.Action disappearCallback;
        private int sadnessCounter = 0;
        public void Appear(System.Action disappearCallback = null)
        {
            this.disappearCallback = disappearCallback;
            gameObject.SetActive(true);          
            skeletonAnimation.AnimationState.AddAnimation(0, idleAnimationRef.Animation, true, 0);
            HideTail();
        }
        public void SayTask()
        {
            taskSound.Play(audioSource);
        }
        private void Disappear()
        {
            gameObject.SetActive(false);
            disappearCallback?.Invoke();
        }
        public void ResetSadness()
        {
            sadnessCounter = 0;
        }
        void HideTail()
        {
            var slot = skeletonAnimation.Skeleton.FindSlot(tailSlotName);
            slot.Attachment = null;
        }
        public void AttachFalseTail(Sprite sprite)
        {
            falseTail.AttachFalseTail(attachmentPoint, sprite, Refuse);
            sadnessCounter++;
            void Refuse()
            {
                if (sadnessCounter>2)
                {
                    skeletonAnimation.PlayAnimation(sadAnimationRef,false);
                    skeletonAnimation.QueueAnimation(idleAnimationRef, true).SetStartAction(falseTail.Disappear);
                }
                else
                {
                    skeletonAnimation.PlayAnimation(refuseAnimationRef, true);
                    skeletonAnimation.QueueAnimation(idleAnimationRef, true, 1).SetStartAction(falseTail.Disappear);
                }

                
            }
        }
        public void Accept()
        {
            falseTail.Disappear();
            skeletonAnimation.PlayAnimation(joyAnimationRef).SetCallbackAction(Disappear);
            skeletonAnimation.QueueAnimation(idleAnimationRef, true);
            skeletonAnimation.Skeleton.SetAttachment(tailSlotName, attachment);
        }
        public void ReactOnClick()
        {
            skeletonAnimation.PlayAnimation(tapAnimationRef);
            skeletonAnimation.QueueAnimation(idleAnimationRef, true);
        }
        public void ShowHint()
        {
            animalHint.Show();
        }
        public void HideHint()
        {
            animalHint.Hide();
        }
    }
}