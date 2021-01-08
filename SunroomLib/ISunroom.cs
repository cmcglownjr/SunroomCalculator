using System.Collections.Generic;

namespace SunroomLib
{
    public interface ISunroom
    {
        void CalculatePanelLength(double pitch, double pitchedWall);
        // Dictionary<string, object> CalculateRoofPanels(double soffitWall, Dictionary<string, object> 
        //     panelLengthDict);
    }
}