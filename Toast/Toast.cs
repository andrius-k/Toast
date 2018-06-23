using System;
using UIKit;
using GlobalToast.ToastViews;
using GlobalToast.Animation;
namespace GlobalToast
{
    public class Toast
    {
        public string Message { get; protected set; }
        public string Title { get; protected set; }
        public double Duration { get; protected set; } = ToastDuration.Regular;
        public ToastPosition Position { get; protected set; } = ToastPosition.Bottom;
        public bool ShowShadow { get; protected set; } = true;
        public bool ProgressIndicator { get; protected set; }
        public Func<Toast, BaseToastView> ToastViewFactory { get; protected set; }
        public UIViewController ParentController { get; protected set; }
        public bool BlockTouches { get; protected set; }
        public bool AutoDismiss { get; protected set; } = true;
        public string DismissButtonTitle { get; protected set; } = "Dismiss";

        public ToastAppearance Appearance { get; protected set; }
        private static ToastAppearance _globalAppearance = new ToastAppearance();
        public static ToastAppearance GlobalAppearance 
        {
            get => _globalAppearance;
            set => _globalAppearance = value ?? 
                throw new ArgumentException("GlobalAppearance can't be null");
        }

        public ToastLayout Layout { get; protected set; }
        private static ToastLayout _globalLayout = new ToastLayout();
        public static ToastLayout GlobalLayout
        {
            get => _globalLayout;
            set => _globalLayout = value ??
                throw new ArgumentException("GlobalLayout can't be null");
        }

        public ToastAnimator Animator { get; protected set; }
        private static ToastAnimator _globalAnimator = new FadeAnimator();
        public static ToastAnimator GlobalAnimator
        {
            get => _globalAnimator;
            set => _globalAnimator = value ??
                throw new ArgumentException("GlobalAnimator can't be null");
        }

        protected BaseToastView ToastView { get; set; }

        private Toast()
        {
        }

        /// <summary>
        /// Creates Toast object with provided title
        /// </summary>
        public static Toast MakeToast(string message)
        {
            var toast = new Toast
            {
                Message = message
            };
            return toast;
        }

        /// <summary>
        /// Helper method that creates a toast and immediately shows it.
        /// </summary>
        public static Toast ShowToast(string message)
        {
            var toast = new Toast
            {
                Message = message
            };
            toast.Show();

            return toast;
        }

        /// <summary>
        /// Helper method that creates a toast and immediately shows it.
        /// </summary>
        public static Toast ShowToast(string message, string title)
        {
            var toast = new Toast
            {
                Message = message,
                Title = title
            };
            toast.Show();

            return toast;
        }

        /// <summary>
        /// Sets the message of the toast
        /// </summary>
        public Toast SetMessage(string message)
        {
            Message = message;
            return this;
        }

        /// <summary>
        /// Sets the title of the toast
        /// </summary>
        public Toast SetTitle(string title)
        {
            Title = title;
            return this;
        }

        /// <summary>
        /// Sets the position of the toast
        /// </summary>
        public Toast SetPosition(ToastPosition position)
        {
            Position = position;
            return this;
        }

        /// <summary>
        /// Sets the duration of the toast
        /// </summary>
        public Toast SetDuration(double duration)
        {
            Duration = duration;
            return this;
        }

        /// <summary>
        /// Sets whether shadow should be drawn behind the frame of the toast
        /// </summary>
        public Toast SetShowShadow(bool show)
        {
            ShowShadow = show;
            return this;
        }

        /// <summary>
        /// Sets whether shadow should be drawn behind the frame of the toast
        /// </summary>
        public Toast SetProgressIndicator(bool progress)
        {
            ProgressIndicator = progress;
            return this;
        }

        /// <summary>
        /// Sets the toast view factory.
        /// View returned by this action will be used for presentation.
        /// </summary>
        public Toast SetToastViewFactory(Func<Toast, BaseToastView> toastViewFactory)
        {
            ToastViewFactory = toastViewFactory;
            return this;
        }

        /// <summary>
        /// Sets the appearance.
        /// </summary>
        public Toast SetAppearance(ToastAppearance appearance)
        {
            Appearance = appearance ?? throw new ArgumentNullException(nameof(appearance));
            return this;
        }

        /// <summary>
        /// Sets the layout.
        /// </summary>
        public Toast SetLayout(ToastLayout layout)
        {
            Layout = layout ?? throw new ArgumentNullException(nameof(layout));
            return this;
        }

        /// <summary>
        /// Sets the animator.
        /// </summary>
        public Toast SetAnimator(ToastAnimator animator)
        {
            Animator = animator ?? throw new ArgumentNullException(nameof(animator));
            return this;
        }

        /// <summary>
        /// Sets the parent controller. 
        /// Toast will be presented inside this controller instead of being added to KeyWindow.
        /// Layout guides will be taken into account when displaying toast.
        /// </summary>
        public Toast SetParentController(UIViewController controller)
        {
            ParentController = controller;
            return this;
        }

        /// <summary>
        /// If this is true touches on the toast will not be passed through to underlying layer.
        /// </summary>
        public Toast SetBlockTouches(bool blockTouches)
        {
            BlockTouches = blockTouches;
            return this;
        }

        /// <summary>
        /// If this is true toast will not be dismissed until user action is taken.
        /// Setting this to false will set <see cref="BlockTouches"/> to true so that the dismiss button is clickable.
        /// </summary>
        public Toast SetAutoDismiss(bool autoDismiss)
        {
            AutoDismiss = autoDismiss;
            if(autoDismiss == false)
                BlockTouches = true;
            return this;
        }

        /// <summary>
        /// Sets the dismiss button title.
        /// </summary>
        public Toast SetDismissButtonTitle(string title)
        {
            DismissButtonTitle = title;
            return this;
        }

        /// <summary>
        /// Presents the toast
        /// </summary>
        public Toast Show()
        {
            if (Appearance == null)
                Appearance = GlobalAppearance;

            if (Layout == null)
                Layout = GlobalLayout;

            if (Animator == null)
                Animator = GlobalAnimator;

            if (ProgressIndicator)
                AutoDismiss = false;
            
            ToastView = GetToastView();

            ToastView.ParentView.AddSubview(ToastView);
            ToastView.ParentView.BringSubviewToFront(ToastView);

            ToastView.Setup();

            if(AutoDismiss)
            {
                ToastView.AnimateShow();
                new ToastTimer(ToastView, Duration, () =>
                {
                    Remove();
                }).Start();
            }
            else
            {
                ToastView.AnimateShow();
                ToastView.DismissAction = () =>
                {
                    ToastView.DismissAction = null;
                    Remove();
                };
            }

            return this;
        }

        /// <summary>
        /// If <see cref="AutoDismiss"/> is false this method will dismiss 
        /// shown toast without user input.
        /// </summary>
        public void Dismiss()
        {
            if(AutoDismiss == false)
                ToastView?.DismissAction?.Invoke();
        }

        private void Remove()
        {
            ToastView.InvokeOnMainThread(() =>
            {
                ToastView.AnimateHide(() =>
                {
                    ToastView.RemoveFromSuperview();
                });
            });
        }

        /// <summary>
        /// Returns the view of the toast that will be presented
        /// </summary>
        private BaseToastView GetToastView()
        {
            if (ToastViewFactory != null)
                return ToastViewFactory(this);

            if (Title == null)
            {
                if (ProgressIndicator)
                    return new ProgressMessageToastView(this);
                else if (AutoDismiss)
                    return new MessageToastView(this);
                else
                    return new DismissibleMessageToastView(this);
            }
            else
            {
                if (ProgressIndicator)
                    return new ProgressTitleMessageToastView(this);
                else if (AutoDismiss)
                    return new TitleMessageToastView(this);
                else
                    return new DismissibleTitleMessageToastView(this);
            }
        }
    }
}
