using System.Collections.Generic;

namespace SunroomLib
{
    public interface IGabled
    {
        void WallHeightPitch(List<double> pitch, List<double> soffitWallHeight);
        void WallHeightAttachedHeight(List<double> soffitWallHeight, double attachedHeight);
        void MaxHeightPitch(List<double> pitch, double maxHeight);
        void SoffitHeightAttachedHeight(List<double> soffitHeight, double attachedHeight);
        void SoffitHeightPitch(List<double> pitch, List<double> soffitHeight);
        void DripEdgeAttachedHeight(double dripEdge, double attachedHeight);
        void DripEdgePitch(double dripEdge, List<double> pitch);
    }
}