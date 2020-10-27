using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace CreateLineFeature
{
    public class CreateLineFeature : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public CreateLineFeature()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;

            //get line from drawing
            IRubberBand pRubber = new RubberLine();
            //IPolyline pPloyline = pRubber.TrackNew(pMxDoc.ActivatedView.ScreenDisplay, null) as IPolyline;

            //get lines from hard coded
            IPolyline pPloyline;

            //create points
            IPoint pPoint1 = new Point();
            pPoint1.X= -92.675984 ;
            pPoint1.Y=  194.232396 ;

            IPoint pPoint2 = new Point();
            pPoint2.X=-78.115341 ;
            pPoint2.Y= 133.077697;

           IPoint pPoint3 = new Point();
            pPoint3.X=13.616707 ;
            pPoint3.Y = 0.090495;

            IPointCollection pPointCollection = new Polyline();
            pPointCollection.AddPoint(pPoint1);
            pPointCollection.AddPoint(pPoint2);
            pPointCollection.AddPoint(pPoint3);


            IFeatureLayer pFLayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;

            IDataset pDS = pFLayer.FeatureClass as IDataset;
            IWorkspaceEdit pWSE = pDS.Workspace as IWorkspaceEdit;

            pWSE.StartEditing(false);
            pWSE.StartEditOperation();

            IFeature pFeature = pFLayer.FeatureClass.CreateFeature();
            pFeature.Shape = pPointCollection as IGeometry;
            pFeature.Store();

            pWSE.StopEditOperation();
            pWSE.StopEditing(true);

            pMxDoc.ActivatedView.Refresh();
        }
    }

}
