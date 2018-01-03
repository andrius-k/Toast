using System;
using Foundation;
using UIKit;
namespace ToastSample
{
    public class SamplesTableViewSource : UITableViewSource
    {
        private const string _cellId = "sampleCell";
        private Action<SampleAction> _itemClicked;
        private Action<SampleAction, bool> _settingChanged;

        private TableSection[] _data =
        {
            new TableSection
            {
                Title = "Settings",
                Items = new TableItem[]
                {
                    new TableItem("Inside Controller", SampleAction.SettingsInsideController, false),
                    new TableItem("Long Duration", SampleAction.SettingsLongDuration, false),
                    new TableItem("Position Top", SampleAction.SettingsPossitionTop, false),
                    new TableItem("Long Message", SampleAction.SettingsLongMessage, false),
                    new TableItem("Remove Shadow", SampleAction.SettingsRemoveShadow, false),
                    new TableItem("Scale Animation", SampleAction.SettingsScaleAnimation, false),
                    new TableItem("Block Touches", SampleAction.SettingsBlockTouches, false),
                    new TableItem("Auto Dismiss", SampleAction.SettingsAutoDismiss, true)
                }
            },
            new TableSection
            {
                Title = "Toast Samples",
                Items = new TableItem[]
                {
                    new TableItem("Single Message", SampleAction.SampleSingleMessage),
                    new TableItem("Message with Title", SampleAction.SampleMessageWithTitle)
                }
            },
            new TableSection
            {
                Title = "Navigation",
                Items = new TableItem[]
                {
                    new TableItem("Open Another Controller", SampleAction.NavigationOpenController)
                }
            },
        };

        public SamplesTableViewSource(Action<SampleAction> itemClicked, Action<SampleAction, bool> settingChanged)
        {
            _itemClicked = itemClicked;
            _settingChanged = settingChanged;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return _data[section].Title;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return _data.Length;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _data[section].Items.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(_cellId);
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, _cellId);

            var item = _data[indexPath.Section].Items[indexPath.Row];

            cell.TextLabel.Text = item.Title;
            cell.SelectionStyle = UITableViewCellSelectionStyle.Default;

            if(item.IsOn != null)
            {
                var toggle = new UISwitch();
                toggle.On = item.IsOn.Value;
                toggle.ValueChanged += (sender, e) => 
                {
                    item.IsOn = toggle.On;
                    _settingChanged?.Invoke(item.Action, item.IsOn.Value);
                };
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                cell.AccessoryView = toggle;
            }

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            _itemClicked?.Invoke(_data[indexPath.Section].Items[indexPath.Row].Action);
        }
    }
}
