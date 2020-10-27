using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace createPolygonFeature
{
    public class createPolygonFeature : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public createPolygonFeature()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseDown(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;

            IRubberBand pRabber = new RubberPolygon();

            //get polygon from screen
            IPolygon pPolygon = pRabber.TrackNew(pMxDoc.ActiveView.ScreenDisplay, null) as IPolygon;
           
            //get cordinate from hard coded
            //IPolygon pPolygon ;

            //IPoint pPoint1 = new Point();
            //pPoint1.X = -120.730273;
            //pPoint1.Y = 224.928212;

            //IPoint pPoint2 = new Point();
            //pPoint2.X = -25.280158;
            //pPoint2.Y = 27942068.023;

            //IPoint pPoint3 = new Point();
            //pPoint3.X = -117.895121;
            //pPoint3.Y = 150.269211;



            //IPointCollection pPointCollection = new Polygon();
            //pPointCollection.AddPoint(pPoint1);
            //pPointCollection.AddPoint(pPoint2);
            //pPointCollection.AddPoint(pPoint3);
            //pPointCollection.AddPoint(pPoint1);

            //pPolygon = pPointCollection as IPolygon;


            ////fix when draw wrong draw
            //IArea pArea = pPolygon as IArea;
            //if (pArea.Area < 0)
            //{
            //    pPolygon.ReverseOrientation();
            //}


            IFeatureLayer pFlayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDS = pFlayer.FeatureClass as IDataset;
            IWorkspaceEdit pWSE = pDS.Workspace as IWorkspaceEdit;

            pWSE.StartEditing(false);
            pWSE.StartEditOperation();

            IFeature pFeature = pFlayer.FeatureClass.CreateFeature();
            pFeature.Shape = pPolygon;
            pFeature.Store();

            pWSE.StopEditOperation();
            pWSE.StopEditing(true);

            pMxDoc.ActiveView.Refresh();
        }
    }

}
