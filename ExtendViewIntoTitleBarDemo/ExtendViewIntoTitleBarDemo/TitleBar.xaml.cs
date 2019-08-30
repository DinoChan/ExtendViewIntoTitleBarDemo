using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace ExtendViewIntoTitleBarDemo
{
    public sealed partial class TitleBar : UserControl
    {
        private UISettings _uiSettings;
        private AccessibilitySettings _accessibilitySettings;
        private CoreApplicationViewTitleBar _coreTitleBar;

        public TitleBar()
        {
            this.InitializeComponent();
            _uiSettings = new UISettings();
            _accessibilitySettings = new AccessibilitySettings();
            _coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

            _coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(BackgroundElement);

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
