using System.Collections.Generic;

namespace SunroomLib
{
    public interface ICathedral:ISunroom
    {
        void WallHeightPitch(List<double> pitch, List<double> soffitWallHeight);
        void WallHeightAttachedHeight(List<double> soffitWallHeight, double peak);
        void MaxHeightPitch(List<double> pitch, double maxH);
        void SoffitHeightAttachedHeight(List<double> soffitHeight, double peak);
        void SoffitHeightPitch(List<double> pitch, List<double> soffitHeight);
        void DripEdgeAttachedHeight(double dripEdge, double peak);
        void DripEdgePitch(double dripEdge, List<double> pitch);
    }
}