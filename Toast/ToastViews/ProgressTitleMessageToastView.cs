using UIKit;
using GlobalToast.Extensions;
namespace GlobalToast.ToastViews
{
    public class ProgressTitleMessageToastView : BaseToastView
    {
        public UIActivityIndicatorView ActivityIndicator { get; set; }
        public UILabel TitleLabel { get; set; }
        public UILabel MessageLabel { get; set; }

        public ProgressTitleMessageToastView(Toast toast) : base(toast)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            ActivityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            ActivityIndicator.StartAnimating();
            ActivityIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(ActivityIndicator);

            TitleLabel = new UILabel();
            TitleLabel.Text = Toast.Title;
            TitleLabel.Font = Toast.Appearance.TitleFont;
            TitleLabel.TextColor = Toast.Appearance.TitleColor;
            TitleLabel.Lines = 0;
            TitleLabel.AdjustsFontSizeToFitWidth = true;
            TitleLabel.MinimumFontSize = 10f;
            TitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(TitleLabel);

            MessageLabel = new UILabel();
            MessageLabel.Text = Toast.Message;
            MessageLabel.Font = Toast.Appearance.MessageFont;
            MessageLabel.TextColor = Toast.Appearance.MessageColor;
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

            ActivityIndicator.SafeLeadingAnchor().ConstraintEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            ActivityIndicator.SafeCenterYAnchor().ConstraintEqualTo(this.SafeCenterYAnchor()).Active = true;
            ActivityIndicator.SafeBottomAnchor().ConstraintLessThanOrEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;
            ActivityIndicator.SafeTopAnchor().ConstraintGreaterThanOrEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;

            TitleLabel.SafeTrailingAnchor().ConstraintEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            TitleLabel.SafeLeadingAnchor().ConstraintEqualTo(ActivityIndicator.SafeTrailingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            TitleLabel.SafeTopAnchor().ConstraintEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;
            TitleLabel.SafeBottomAnchor().ConstraintEqualTo(MessageLabel.SafeTopAnchor(), -Toast.Layout.Spacing).Active = true;

            MessageLabel.SafeLeadingAnchor().ConstraintEqualTo(ActivityIndicator.SafeTrailingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            MessageLabel.SafeTrailingAnchor().ConstraintEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            MessageLabel.SafeBottomAnchor().ConstraintEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;
        }
    }
}
