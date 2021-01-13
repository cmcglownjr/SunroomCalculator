using System.Collections.Generic;

namespace SunroomLib
{
    public class Gabled : Sunroom, IGabled
    {
        public Gabled(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut, 
            string panelWidth):base(aWall, bWall, cWall, overhang, thickness, endCut, panelWidth){}
        public void WallHeightPitch(List<double> pitch, List<double> soffitWallHeight)
        {
            throw new System.NotImplementedException();
        }

        public void WallHeightAttachedHeight(List<double> soffitWallHeight, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void MaxHeightPitch(List<double> pitch, double maxHeight)
        {
            throw new System.NotImplementedException();
        }

        public void SoffitHeightAttachedHeight(List<double> soffitHeight, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void SoffitHeightPitch(List<double> pitch, List<double> soffitHeight)
        {
            throw new System.NotImplementedException();
        }

        public void DripEdgeAttachedHeight(double dripEdge, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void DripEdgePitch(double dripEdge, List<double> pitch)
        {
            throw new System.NotImplementedException();
        }
    }
}