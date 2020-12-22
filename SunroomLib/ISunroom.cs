using System.Collections.Generic;

namespace SunroomLib
{
    public interface ISunroom
    {
        Dictionary<string, object> calculate_roof_panels(double soffitWall, Dictionary<string, object> 
            panelLengthDict);
    }
}