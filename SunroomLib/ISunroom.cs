using System.Collections.Generic;

namespace SunroomLib
{
    public interface ISunroom
    {
        Dictionary<string, object> CalculateRoofPanels(double soffitWall, Dictionary<string, object> 
            panelLengthDict);
    }
}