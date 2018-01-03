using System;
using UIKit;
using GlobalToast.Extensions;
namespace GlobalToast.ToastViews
{
    public class TitleMessageToastView : BaseToastView
    {
        public UILabel TitleLabel { get; set; }
        public UILabel MessageLabel { get; set; }

        public TitleMessageToastView(Toast toast) : base(toast)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            TitleLabel = new UILabel();
            TitleLabel.Text = Toast.Title;
            TitleLabel.Font = Toast.Appearance.TitleFont;
            TitleLabel.TextColor = Toast.Appearance.TitleColor;
            TitleLabel.TextAlignment = UITextAlignment.Center;
            TitleLabel.Lines = 0;
            TitleLabel.AdjustsFontSizeToFitWidth = true;
            TitleLabel.MinimumFontSize = 10f;
            TitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(TitleLabel);

            MessageLabel = new UILabel();
            MessageLabel.Text = Toast.Message;
            MessageLabel.Font = Toast.Appearance.MessageFont;
            MessageLabel.TextColor = Toast.Appearance.MessageColor;
            MessageLabel.TextAlignment = UITextAlignment.Center;
            MessageLabel.Lines = 0;
            MessageLabel.AdjustsFontSizeToFitWidth = true;
            MessageLabel.MinimumFontSize = 10f;
            MessageLabel.TextAlignment = Toast.Appearance.MessageTextAlignment;
            MessageLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(MessageLabel);
        }

        protected override void ConstrainChildren()
        {
            base.ConstrainChildren();

            TitleLabel.SafeTrailingAnchor().ConstraintLessThanOrEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            TitleLabel.SafeLeadingAnchor().ConstraintGreaterThanOrEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            TitleLabel.SafeTopAnchor().ConstraintEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;
            TitleLabel.SafeBottomAnchor().ConstraintEqualTo(MessageLabel.SafeTopAnchor(), -Toast.Layout.Spacing).Active = true;

            MessageLabel.SafeLeadingAnchor().ConstraintGreaterThanOrEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            MessageLabel.SafeTrailingAnchor().ConstraintLessThanOrEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            MessageLabel.SafeBottomAnchor().ConstraintEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;
        }
    }
}
