using System;
using UIKit;
namespace GlobalToast
{
    public class ToastAppearance
    {
        public UIColor Color { get; set; } = UIColor.FromRGBA(0f, 0f, 0f, 0.85f);
        public UIColor MessageColor { get; set; } = UIColor.White;
        public UIColor TitleColor { get; set; } = UIColor.White;
        public UIColor DismissButtonColor { get; set; } = UIColor.FromRGB(255, 75, 75);
        public UIFont MessageFont = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);
        public UIFont TitleFont = UIFont.SystemFontOfSize(16f, UIFontWeight.Medium);
        public UIFont DismissButtonFont = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);
        public UITextAlignment MessageTextAlignment = UITextAlignment.Left;
        public nfloat CornerRadius { get; set; } = 5;
    }
}
