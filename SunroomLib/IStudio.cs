namespace SunroomLib
{
    public interface IStudio : ISunroom
    {
        void wall_height_pitch(double pitch, double soffitWallHeight);
        void wall_height_peak_height(double soffitWallHeight, double peak);
        void max_height_pitch(double pitch, double maxH);
        void soffit_height_peak_height(double soffitHeight, double peak);
        void soffit_height_pitch(double pitch, double soffitHeight);
        void drip_edge_peak_height(double dripEdge, double peak);
        void drip_edge_pitch(double dripEdge, double pitch);
    }
}