using System.Collections.Generic;

namespace SunroomLib
{
    public interface ISunroom
    {
        void CalculatePanelLength(double pitch, double pitchedWall);
        double CalculateDripEdge(double soffit, double pitch);
    }
}