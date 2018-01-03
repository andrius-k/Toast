using System;
using GlobalToast.ToastViews;
namespace GlobalToast.Animation
{
    /// <summary>
    /// Provides functionality for toast show and hide animations
    /// </summary>
    public abstract class ToastAnimator
    {
        public virtual double AnimationDuration
        {
            get => 0.4;
        }

        /// <summary>
        /// Start show animation
        /// </summary>
        public abstract void AnimateShow(BaseToastView toastView);

        /// <summary>
        /// Start hide animation. 
        /// Completion will be invoked when animation completes.
        /// </summary>
        public abstract void AnimateHide(BaseToastView toastView, Action completion);
    }
}
