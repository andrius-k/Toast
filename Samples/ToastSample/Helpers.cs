using System;
namespace ToastSample
{
    public class TableSection
    {
        public string Title { get; set; }
        public TableItem[] Items { get; set; }
    }

    public class TableItem 
    {
        public TableItem(string title, SampleAction action, bool? isOn = null)
        {
            Title = title;
            Action = action;
            IsOn = isOn;
        }

        public string Title { get; set; }
        public SampleAction Action { get; set; }
        public bool? IsOn { get; set; }
    }

    public enum SampleAction
    {
        None,
        SettingsInsideController,
        SettingsLongDuration,
        SettingsPossitionTop,
        SettingsLongMessage,
        SettingsRemoveShadow,
        SettingsScaleAnimation,
        SettingsBlockTouches,
        SettingsAutoDismiss,
        SampleSingleMessage,
        SampleMessageWithTitle,
        NavigationOpenController,
    }
}
