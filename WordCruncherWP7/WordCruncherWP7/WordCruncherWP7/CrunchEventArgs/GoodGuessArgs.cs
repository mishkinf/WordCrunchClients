﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WordCruncherWP7.Messages;

namespace WordCruncherWP7.CrunchEventArgs
{
    public class GoodGuessArgs : EventArgs
    {
        public GoodGuessResponseMessage Message;

        public GoodGuessArgs(GoodGuessResponseMessage message)
        {
            this.Message = message;
        }
    }
}
