using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace CreatePointFeature
{
    public class CreatePointFeature : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public CreatePointFeature()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
           
            //get point cordinate from map point
           // IPoint pPoint = pMxDoc.ActivatedView.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y);


            //get point from hard coded
            IPoint pPoint = new Point();
            pPoint.X = -126.165462;
            pPoint.Y = 136.960535;

            IFeatureLayer pFLayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;

            IWorkspaceEdit pWorkspaceEdit = ((IDataset)(pFLayer.FeatureClass)).Workspace as IWorkspaceEdit;

            pWorkspaceEdit.StartEditing(false);

            pWorkspaceEdit.StartEditOperation();

            IFeature pFeature = pFLayer.FeatureClass.CreateFeature();
            pFeature.Shape = pPoint;
            pFeature.Store();

            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);


            //for refresh mxd doc
            pMxDoc.ActivatedView.Refresh();
        }
    }

}
