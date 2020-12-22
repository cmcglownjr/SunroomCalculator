using System.Collections.Generic;

namespace SunroomLib
{
    public interface ICathedral:ISunroom
    {
        void wall_height_pitch(List<double> pitch, List<double> soffitWallHeight);
        void wall_height_peak_height(List<double> soffitWallHeight, double peak);
        void max_height_pitch(List<double> pitch, double maxH);
        void soffit_height_peak_height(List<double> soffitHeight, double peak);
        void soffit_height_pitch(List<double> pitch, List<double> soffitHeight);
        void drip_edge_peak_height(double dripEdge, double peak);
        void drip_edge_pitch(double dripEdge, List<double> pitch);
    }
}