using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Windows.Forms;

namespace WorkWithValueDomain
{
    public class WorkWithValueDomain : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public WorkWithValueDomain()
        {
        }

        protected override void OnClick()
        {
            IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactory();

            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(@"D:\Applications\ArcObjects\TestButton\GeoDP\GeoDB.gdb", ArcMap.Application.hWnd);

            IWorkspaceDomains pWorkspaceDomains = pWorkspace as IWorkspaceDomains;
            ICodedValueDomain pDomain = pWorkspaceDomains.DomainByName["Resturants"] as ICodedValueDomain;

            for (int i = 0; i < pDomain.CodeCount; i++)
            {
                MessageBox.Show("Code: " + pDomain.Value[i] + Environment.NewLine + "Description: " + pDomain.Name[i]);

            }
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
