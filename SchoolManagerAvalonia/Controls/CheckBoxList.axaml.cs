using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SchoolManagerViewModel;

namespace SchoolManagerAvalonia.Controls
{
    public partial class CheckBoxList : UserControl, IObserver<IEnumerable<CheckBoxListItem>>
    {
        public CheckBoxList()
        {
            InitializeComponent();

            // Subscribe to changes in the Objects property
            this.GetObservable(ObjectsProperty).Subscribe(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // Define the styled property for Objects
        public static readonly StyledProperty<IEnumerable<CheckBoxListItem>> ObjectsProperty =
            AvaloniaProperty.Register<CheckBoxList, IEnumerable<CheckBoxListItem>>(nameof(Objects));

        public IEnumerable<CheckBoxListItem> Objects
        {
            get => GetValue(ObjectsProperty);
            set => SetValue(ObjectsProperty, value);
        }

        // Define the styled property for AllSubjectsSelected
        public static readonly StyledProperty<bool> AllSubjectsSelectedProperty =
            AvaloniaProperty.Register<CheckBoxList, bool>(nameof(AllSubjectsSelected));

        public bool AllSubjectsSelected
        {
            get => GetValue(AllSubjectsSelectedProperty);
            set => SetValue(AllSubjectsSelectedProperty, value);
        }

        private void OnObjectsChanged(IEnumerable<CheckBoxListItem>? newObjects)
        {
            if (newObjects != null)
            {
                foreach (var item in newObjects)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CheckBoxListItem.IsChecked))
            {
                UpdateAllSubjectsSelectedState();
            }
        }

        private void UpdateAllSubjectsSelectedState()
        {
            AllSubjectsSelected = Objects?.All(item => item.IsChecked) ?? false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateAllSubjectsSelectedState();
        }

        // Implementation of IObserver<IEnumerable<CheckBoxListItem>>
        public void OnNext(IEnumerable<CheckBoxListItem>? value)
        {
            OnObjectsChanged(value);
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}
