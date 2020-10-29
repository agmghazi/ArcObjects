using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace createRowInTable
{
    public class createRowInTable : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public createRowInTable()
        {
        }

        protected override void OnClick()
        {
            IMxDocument pMDoc = ArcMap.Application.Document as IMxDocument;
            IFeatureLayer pFlayer = pMDoc.ActiveView.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDs = pFlayer.FeatureClass as IDataset;

            IFeatureWorkspace pFWs = pDs.Workspace as IFeatureWorkspace;
            ITable pTable = pFWs.OpenTable("Tables");

            IWorkspaceEdit pWsE = pFWs as IWorkspaceEdit;
            pWsE.StartEditing(true);
            pWsE.StartEditOperation();

            IRow pRow = pTable.CreateRow();
            pRow.Value[pRow.Fields.FindField("Name")] = "Ahmed Gamal";
            pRow.Value[pRow.Fields.FindField("Rank")] = 5;
            pRow.Store();


            pWsE.StopEditOperation();
            pWsE.StopEditing(true);

            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
