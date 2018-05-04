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
        /// <summary>
        /// UISwitch with on or off values will be displayed
        /// </summary>
        public TableItem(string title, SampleAction action, bool isOn)
        {
            Title = title;
            Action = action;
            SelectedAction = isOn ? 0 : -1;
            ItemType = TableItemType.Toggle;
        }

        /// <summary>
        /// UISegmentedControll with each action will be displayed
        /// </summary>
        public TableItem(string title, params (SampleAction, string)[] actionsAndTitles) : this (title, 0, actionsAndTitles)
        {
        }

        /// <summary>
        /// UISegmentedControll with each action will be displayed
        /// </summary>
        public TableItem(string title, int selectedAction, params (SampleAction, string)[] actionsAndTitles)
        {
            Title = title;
            ActionsAndTitles = actionsAndTitles;
            SelectedAction = selectedAction;
            ItemType = TableItemType.MultipleActions;
        }

        /// <summary>
        /// Item will be clickable and no accessory will be displayed
        /// </summary>
        public TableItem(string title, SampleAction action)
        {
            Title = title;
            Action = action;
            ItemType = TableItemType.Click;
        }

        public string Title { get; set; }
        public SampleAction Action { get; set; }
        public (SampleAction, string)[] ActionsAndTitles { get; set; }
        public int SelectedAction = -1;
        public TableItemType ItemType { get; set; }

        /// <summary>
        /// Determines whether accessory switch is turned on or off when ItemType is Toggle
        /// </summary>
        public bool ToggleIsOn
        {
            get
            {
                if(ItemType == TableItemType.Toggle)
                    return SelectedAction == -1 ? false : true;
                
                return false;
            }
            set
            {
                if (ItemType == TableItemType.Toggle)
                    SelectedAction = value ? 0 : -1;
            }
        }
    }

    public enum SampleAction
    {
        None,
        SettingsInsideController,
        SettingsLongDuration,
        SettingsPossitionTop,
        SettingsPossitionBottom,
        SettingsPossitionCenter,
        SettingsLongMessage,
        SettingsRemoveShadow,
        SettingsScaleAnimation,
        SettingsBlockTouches,
        SettingsAutoDismiss,
        SampleSingleMessage,
        SampleMessageWithTitle,
        NavigationOpenController,
    }

    public enum TableItemType
    {
        Click,
        Toggle,
        MultipleActions
    }
}
