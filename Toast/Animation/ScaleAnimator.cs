using System;
using GlobalToast.ToastViews;
using UIKit;

namespace GlobalToast.Animation
{
    /// <summary>
    /// Show animation will scale the toast up.
    /// Hide animation will fade toast out.
    /// </summary>
    public class ScaleAnimator : ToastAnimator
    {
        public override void AnimateShow(BaseToastView toastView)
        {
            toastView.Transform = CoreGraphics.CGAffineTransform.MakeScale(0.7f, 0.7f);
            toastView.ParentView.LayoutIfNeeded();

            toastView.Alpha = 0;
            UIView.AnimateNotify(AnimationDuration, 0, 0.65f, 1f, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                toastView.Alpha = 1;
                toastView.Transform = CoreGraphics.CGAffineTransform.MakeScale(1, 1);
                toastView.ParentView.LayoutIfNeeded();
            }, null);
        }

        public override void AnimateHide(BaseToastView toastView, Action completion)
        {
            toastView.Alpha = 1;
            UIView.Animate(AnimationDuration, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
            {
                toastView.Alpha = 0;
            }, completion);
        }
    }
}
