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

namespace WordCruncherWP7.Messages
{
    public class GoodbyeMessage : iMessage
    {
        public static int TYPE_CODE = 0x0006;

        public String encode() { return ""; }

        public void fromJSON(string message)
        {

        }
    }
}
