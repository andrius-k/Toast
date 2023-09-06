using GlobalToast;
using GlobalToast.Animation;

namespace ToastSample
{
    public partial class ViewController1 : UIViewController
    {
        private const string ShortMessage = "Hi, this is a very important message!";
        private const string LongMessage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
        private const string ToastTitle = "This is a message!";

        private Stack<Toast> _presentedToasts = new Stack<Toast>();

        private double _duration = ToastDuration.Regular;
        private ToastPosition _position = ToastPosition.Bottom;
        private string _message = ShortMessage;
        private bool _showShadow = true;
        private bool _blockTouches;
        private bool _autoDismiss = true;
        private bool _progressIndicator;
        private ToastAnimator _animator = new FadeAnimator();
        private UIViewController _parentController;

        public ViewController1(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Toast Sample";

            dismissButton.Clicked += (sender, e) =>
            {
                if (_presentedToasts.Count != 0)
                    _presentedToasts.Pop().Dismiss();
            };

            tableView.Source = new SamplesTableViewSource(action =>
            {
                switch (action)
                {
                    case SampleAction.NavigationOpenController:
                        PerformSegue("navigateSegue", this);
                        break;
                    case SampleAction.SampleSingleMessage:
                        ShowSampleSingleLine();
                        break;
                    case SampleAction.SampleMessageWithTitle:
                        ShowSampleWithTitle();
                        break;
                    case SampleAction.SampleProgressIndicator:
                        ShowJustProgressIndicator();
                        break;
                }
            }, (action, isOn) =>
            {
                switch (action)
                {
                    case SampleAction.SettingsInsideController:
                        _parentController = isOn ? this : null;
                        break;
                    case SampleAction.SettingsLongDuration:
                        _duration = isOn ? ToastDuration.Long : ToastDuration.Regular;
                        break;
                    case SampleAction.SettingsLongMessage:
                        _message = isOn ? LongMessage : ShortMessage;
                        break;
                    case SampleAction.SettingsRemoveShadow:
                        _showShadow = !isOn;
                        break;
                    case SampleAction.SettingsScaleAnimation:
                        _animator = isOn ? (ToastAnimator)new ScaleAnimator() : new FadeAnimator();
                        break;
                    case SampleAction.SettingsBlockTouches:
                        _blockTouches = isOn;
                        break;
                    case SampleAction.SettingsAutoDismiss:
                        _autoDismiss = isOn;
                        break;
                    case SampleAction.SettingsProgressIndicator:
                        _progressIndicator = isOn;
                        break;
                }
            }, action =>
            {
                switch (action)
                {
                    case SampleAction.SettingsPossitionBottom:
                        _position = ToastPosition.Bottom;
                        break;
                    case SampleAction.SettingsPossitionTop:
                        _position = ToastPosition.Top;
                        break;
                    case SampleAction.SettingsPossitionCenter:
                        _position = ToastPosition.Center;
                        break;
                }
            });
        }

        private void ShowSampleSingleLine()
        {
            var toast = CreateToast().Show();
            _presentedToasts.Push(toast);
        }

        private void ShowSampleWithTitle()
        {
            var toast = CreateToast().SetTitle(ToastTitle).Show();
            _presentedToasts.Push(toast);
        }

        private void ShowJustProgressIndicator()
        {
            var toast = CreateToast().SetMessage(null).SetTitle(null).SetProgressIndicator(true).Show();
            _presentedToasts.Push(toast);
        }

        private Toast CreateToast()
        {
            return Toast.MakeToast(_message)
                        .SetPosition(_position)
                        .SetDuration(_duration)
                        .SetShowShadow(_showShadow)
                        .SetAnimator(_animator)
                        .SetParentController(_parentController)
                        .SetBlockTouches(_blockTouches)
                        .SetAutoDismiss(_autoDismiss)
                        .SetDismissCallback(() =>
                        {
                            // perform something amazing here.
                        })
                        .SetProgressIndicator(_progressIndicator)
                        .SetDismissButtonTitle("Dismiss");
        }
    }
}