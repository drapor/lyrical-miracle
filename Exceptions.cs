using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LyricalMiracle
{
    public class Exceptions
    {
        public void WriteException(Guid ID, Exception exception, string functionException, string classException, DateTime dateException)
        {
            MessageBox.Show(exception.ToString());
        }
    }
}
