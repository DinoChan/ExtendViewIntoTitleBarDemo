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


       private void SetTitleBarVisibility()
        {
          LayoutRoot.Visibility = m_coreTitleBar->IsVisible || IsAlwaysOnTopMode ? ::Visibility::Visible : ::Visibility::Collapsed;
        }

        void TitleBar::SetTitleBarPadding()
        {
            double leftAddition = 0;
            double rightAddition = 0;

            if (this->FlowDirection == ::FlowDirection::LeftToRight)
            {
                leftAddition = m_coreTitleBar->SystemOverlayLeftInset;
                rightAddition = m_coreTitleBar->SystemOverlayRightInset;
            }
            else
            {
                leftAddition = m_coreTitleBar->SystemOverlayRightInset;
                rightAddition = m_coreTitleBar->SystemOverlayLeftInset;
            }

            this->LayoutRoot->Padding = Thickness(leftAddition, 0, rightAddition, 0);
        }

        void TitleBar::ColorValuesChanged(_In_ UISettings ^ /*sender*/, _In_ Object ^ /*e*/)
        {
            Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler([this]() { SetTitleBarControlColors(); }));
        }

        void TitleBar::SetTitleBarControlColors()
        {
            auto applicationView = ApplicationView::GetForCurrentView();
            if (applicationView == nullptr)
            {
                return;
            }

            auto applicationTitleBar = applicationView->TitleBar;
            if (applicationTitleBar == nullptr)
            {
                return;
            }

            if (m_accessibilitySettings->HighContrast)
            {
                // Reset to use default colors.
                applicationTitleBar->ButtonBackgroundColor = nullptr;
                applicationTitleBar->ButtonForegroundColor = nullptr;
                applicationTitleBar->ButtonInactiveBackgroundColor = nullptr;
                applicationTitleBar->ButtonInactiveForegroundColor = nullptr;
                applicationTitleBar->ButtonHoverBackgroundColor = nullptr;
                applicationTitleBar->ButtonHoverForegroundColor = nullptr;
                applicationTitleBar->ButtonPressedBackgroundColor = nullptr;
                applicationTitleBar->ButtonPressedForegroundColor = nullptr;
            }
            else
            {
                Color bgColor = Colors::Transparent;
                Color fgColor = safe_cast < SolidColorBrush ^> (Application::Current->Resources->Lookup("SystemControlPageTextBaseHighBrush"))->Color;
                Color inactivefgColor =
                    safe_cast < SolidColorBrush ^> (Application::Current->Resources->Lookup("SystemControlForegroundChromeDisabledLowBrush"))->Color;
                Color hoverbgColor = safe_cast < SolidColorBrush ^> (Application::Current->Resources->Lookup("SystemControlBackgroundListLowBrush"))->Color;
                Color hoverfgColor = safe_cast < SolidColorBrush ^> (Application::Current->Resources->Lookup("SystemControlForegroundBaseHighBrush"))->Color;
                Color pressedbgColor = safe_cast < SolidColorBrush ^> (Application::Current->Resources->Lookup("SystemControlBackgroundListMediumBrush"))->Color;
                Color pressedfgCoolor = safe_cast < SolidColorBrush ^> (Application::Current->Resources->Lookup("SystemControlForegroundBaseHighBrush"))->Color;
                applicationTitleBar->ButtonBackgroundColor = bgColor;
                applicationTitleBar->ButtonForegroundColor = fgColor;
                applicationTitleBar->ButtonInactiveBackgroundColor = bgColor;
                applicationTitleBar->ButtonInactiveForegroundColor = inactivefgColor;
                applicationTitleBar->ButtonHoverBackgroundColor = hoverbgColor;
                applicationTitleBar->ButtonHoverForegroundColor = hoverfgColor;
                applicationTitleBar->ButtonPressedBackgroundColor = pressedbgColor;
                applicationTitleBar->ButtonPressedForegroundColor = pressedfgCoolor;
            }
        }

        void TitleBar::OnHighContrastChanged(_In_ AccessibilitySettings ^ /*sender*/, _In_ Object ^ /*args*/)
        {
            Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler([this]() {
                SetTitleBarControlColors();
                SetTitleBarVisibility();
            }));
        }

        void TitleBar::OnWindowActivated(_In_ Object ^ /*sender*/, _In_ WindowActivatedEventArgs ^ e)
        {
            VisualStateManager::GoToState(
                this, e->WindowActivationState == CoreWindowActivationState::Deactivated ? WindowNotFocused->Name : WindowFocused->Name, false);
        }

    }



}
