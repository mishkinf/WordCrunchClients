﻿#pragma checksum "C:\Users\Mishkin\Documents\Visual Studio 2010\Projects\WordCrunchClients\WordCruncherWP7\WordCruncherWP7\WordCruncherWP7\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "934AD027451364B97A6352C9C85B38B9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WordCruncherWP7 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Button playButton;
        
        internal System.Windows.Controls.Button optionsButton;
        
        internal System.Windows.Controls.Button highScoreButton;
        
        internal System.Windows.Controls.Button textConnectionButton;
        
        internal System.Windows.Controls.TextBlock connectionResult;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WordCruncherWP7;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.playButton = ((System.Windows.Controls.Button)(this.FindName("playButton")));
            this.optionsButton = ((System.Windows.Controls.Button)(this.FindName("optionsButton")));
            this.highScoreButton = ((System.Windows.Controls.Button)(this.FindName("highScoreButton")));
            this.textConnectionButton = ((System.Windows.Controls.Button)(this.FindName("textConnectionButton")));
            this.connectionResult = ((System.Windows.Controls.TextBlock)(this.FindName("connectionResult")));
        }
    }
}

