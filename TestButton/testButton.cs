using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TestButton
{
    public class testButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public testButton()
        {
        }

        protected override void OnClick()
        {
            MessageBox.Show("test button");
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
