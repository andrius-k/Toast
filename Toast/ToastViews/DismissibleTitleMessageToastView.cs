using System;
using UIKit;
using GlobalToast.Extensions;
namespace GlobalToast.ToastViews
{
    public class DismissibleTitleMessageToastView : TitleMessageToastView
    {
        public UIButton DismissButton { get; set; }

        public DismissibleTitleMessageToastView(Toast toast) : base(toast)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            DismissButton = new UIButton(UIButtonType.System);
            DismissButton.TitleLabel.Font = Toast.Appearance.DismissButtonFont;
            DismissButton.SetTitleColor(Toast.Appearance.DismissButtonColor, UIControlState.Normal);
            DismissButton.SetTitle("Dismiss", UIControlState.Normal);
            DismissButton.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(DismissButton);
        }

        protected override void ConstrainChildren()
        {
            TitleLabel.SafeTrailingAnchor().ConstraintLessThanOrEqualTo(DismissButton.SafeLeadingAnchor(), -Toast.Layout.Spacing).Active = true;
            TitleLabel.SafeLeadingAnchor().ConstraintGreaterThanOrEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            TitleLabel.SafeTopAnchor().ConstraintEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;
            TitleLabel.SafeBottomAnchor().ConstraintEqualTo(MessageLabel.SafeTopAnchor(), -Toast.Layout.Spacing).Active = true;

            MessageLabel.SafeLeadingAnchor().ConstraintGreaterThanOrEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            MessageLabel.SafeTrailingAnchor().ConstraintLessThanOrEqualTo(DismissButton.SafeLeadingAnchor(), -Toast.Layout.Spacing).Active = true;
            MessageLabel.SafeBottomAnchor().ConstraintEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;

            DismissButton.SafeTrailingAnchor().ConstraintLessThanOrEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            DismissButton.SafeCenterYAnchor().ConstraintEqualTo(this.SafeCenterYAnchor()).Active = true;
            DismissButton.SetContentCompressionResistancePriority(
                MessageLabel.ContentCompressionResistancePriority(UILayoutConstraintAxis.Horizontal) + 10,
                UILayoutConstraintAxis.Horizontal);
            DismissButton.TouchUpInside += DismissButton_TouchUpInside;
        }

        private void DismissButton_TouchUpInside(object sender, EventArgs e)
        {
            Dismiss();
        }

        protected virtual void Dismiss()
        {
            DismissAction?.Invoke();
        }

        public override void RemoveFromSuperview()
        {
            base.RemoveFromSuperview();

            if (DismissButton != null)
                DismissButton.TouchUpInside -= DismissButton_TouchUpInside;
        }
    }
}
