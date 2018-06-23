using System;
using GlobalToast.ToastViews;
using System.Timers;
namespace GlobalToast
{
    public class ToastTimer
    {
        private Timer _timer;
        private BaseToastView _toastView;
        private double _duration;
        private Action _completion;

        public ToastTimer(BaseToastView toastView, double duration, Action completion)
        {
            _toastView = toastView;
            _duration = duration;
            _completion = completion;

            _timer = new Timer(_duration);
            _timer.AutoReset = false;
        }

        public void Start()
        {
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Elapsed -= Timer_Elapsed;
            _timer.Dispose();

            _completion.Invoke();
        }
    }
}
