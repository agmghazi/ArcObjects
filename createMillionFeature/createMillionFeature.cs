using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;

namespace createMillionFeature
{
    public class createMillionFeature : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public createMillionFeature()
        {
        }

        protected override void OnClick()
        {
            //time before running
            DateTime fDate = DateTime.Now;

            int feature = 5000;
            IMxDocument pmxDoc = ArcMap.Application.Document as IMxDocument;
            IFeatureLayer pFlayer = pmxDoc.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDs = pFlayer.FeatureClass as IDataset;
            IWorkspaceEdit pWorkspace = pDs.Workspace as IWorkspaceEdit;

            pWorkspace.StartEditing(false);
            pWorkspace.StartEditOperation();

            IFeatureBuffer pBuffer = pFlayer.FeatureClass.CreateFeatureBuffer();
            IFeatureCursor pFcursor = pFlayer.FeatureClass.Insert(true);
            Random r = new Random();
            for (int i = 0; i < feature; i++)
            {
                IPoint pPoint = new Point();
                pPoint.X = -78.115341 + r.NextDouble() * 20000;
                pPoint.Y = 133.077697 + r.NextDouble() * 20000;

                pBuffer.Shape = pPoint;
                pBuffer.Value[pBuffer.Fields.FindField("Name")] = "myfeature" + i.ToString()+ " "+"Finish";
                pFcursor.InsertFeature(pBuffer);
                if (i % 1000 == 0)
                {
                    pFcursor.Flush();
                }
            }

            pFcursor.Flush();
            pWorkspace.StopEditOperation();
            pWorkspace.StopEditing(true);

            //time after complete;
            double timeinsecond = (DateTime.Now - fDate).TotalSeconds;
            MessageBox.Show("Time to create " + feature + ":" + timeinsecond + "second");

            pmxDoc.ActiveView.Refresh();


        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
