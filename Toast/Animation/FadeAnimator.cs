using System;
using GlobalToast.ToastViews;
using UIKit;

namespace GlobalToast.Animation
{
    /// <summary>
    /// Simple fade in/out toast animator
    /// </summary>
    public class FadeAnimator : ToastAnimator
    {
        public override double AnimationDuration => 0.3;

        public override void AnimateShow(BaseToastView toastView)
        {
            toastView.Alpha = 0;
            UIView.Animate(AnimationDuration, 0, UIViewAnimationOptions.CurveEaseOut, () =>
            {
                toastView.Alpha = 1;
            }, null);
        }

        public override void AnimateHide(BaseToastView toastView, Action completion)
        {
            toastView.Alpha = 1;
            UIView.Animate(AnimationDuration, 0, UIViewAnimationOptions.CurveEaseIn, () =>
            {
                toastView.Alpha = 0;
            }, completion);
        }
    }
}
