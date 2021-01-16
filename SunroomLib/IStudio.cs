namespace SunroomLib
{
    public interface IStudio
    {
        void WallHeightPitch(double pitch, double soffitWallHeight);
        void WallHeightAttachedHeight(double soffitWallHeight, double peak);
        void MaxHeightPitch(double pitch, double maxHeight);
        void SoffitHeightAttachedHeight(double soffitHeight, double peak);
        void SoffitHeightPitch(double pitch, double soffitHeight);
        void DripEdgeAttachedHeight(double dripEdge, double peak);
        void DripEdgePitch(double dripEdge, double pitch);
    }
}