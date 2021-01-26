namespace SunroomLib
{
    public interface IStudio
    {
        void WallHeightPitch(double pitch, double soffitWallHeight);
        void WallHeightAttachedHeight(double soffitWallHeight, double attachedHeight);
        void MaxHeightPitch(double pitch, double maxHeight);
        void SoffitHeightAttachedHeight(double soffitHeight, double attachedHeight);
        void SoffitHeightPitch(double pitch, double soffitHeight);
        void DripEdgeAttachedHeight(double dripEdge, double attachedHeight);
        void DripEdgePitch(double dripEdge, double pitch);
    }
}