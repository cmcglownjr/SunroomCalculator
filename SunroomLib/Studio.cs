using System;
using System.Data;

namespace SunroomLib
{
    /// <summary>
    /// Class <c>Studio</c> Models a studio style sunroom.
    /// </summary>
    public class Studio : Sunroom, IStudio
    {
        private double _pitch, _attachedHeight, _maxHeight, _soffitWallLength, _soffitWallHeight, _soffitHeight, 
            _dripEdge, _pitchedWallLength, _roofArea;

        public double Pitch // The roof pitch
        {
            get => _pitch;
            set
            {
                if (value < Math.Tan(4.0 / 12.0))
                {
                    throw new DataException($"The pitch is less than 4/12 and is considered too low.");
                }
                else if (value > Math.Tan(9.0 / 12.0))
                {
                    throw new DataException($"The pitch is greater than 9/12 and is considered too steep.");
                }
                else
                {
                    _pitch = value;
                }
            }
        }

        public double AttachedHeight // The height where the bottom of the panels attach to the existing structure
        {
            get => _attachedHeight;
            set => _attachedHeight = value;
        }

        public double MaxHeight // The maximum height of the sunroom with panels
        {
            get => _maxHeight;
            set => _maxHeight = value;
        }
        
        public double SoffitWallLength // This will be set to B Wall since it isn't angled
        {
            get => _soffitWallLength;
            set => _soffitWallLength = value;
        }
        public double PitchedWallLength // This will be set to A or C Wall because they are angled
        {
            get => _pitchedWallLength;
            set => _pitchedWallLength = value;
        }
        public double SoffitWallHeight // The height of B Wall
        {
            get => _soffitWallHeight;
            set => _soffitWallHeight = value;
        }

        public double SoffitHeight // The distance from the ground to the soffit
        {
            get => _soffitHeight;
            set => _soffitHeight = value;
        }

        public double DripEdge // The distance from the ground to the drip edge
        {
            get => _dripEdge;
            set => _dripEdge = value;
        }
        
        public double RoofArea // The area of the roof with panels
        {
            get => _roofArea;
            set => _roofArea = value;
        }

        public Studio(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut, 
            string panelWidth) : base(aWall, bWall, cWall, overhang, thickness, endCut, panelWidth)
        {
            SoffitWallLength = BWall;
            PitchedWallLength = Math.Max(AWall, CWall);
        }

        protected override void CalculateRoofPanels()
        {
            double roofWidth = SoffitWallLength + SideOverhang * 2;
            double panelWidth = Utilities.StandardPanelWidths[PanelWidth];
            double roofPanelNumber = Math.Ceiling(roofWidth / panelWidth);
            if ((roofPanelNumber * panelWidth - SoffitWallLength) / 2 < SideOverhang)
            {
                // The overhang from the calculated number of panels is too low
                SideOverhang = (roofPanelNumber * panelWidth - SoffitWallLength) / 2;
            }

            if ((roofPanelNumber * panelWidth - SoffitWallLength) / 2 > (panelWidth / 2))
            {
                // The calculated overhang exceeds max allowed
                SideOverhang = (roofPanelNumber * panelWidth - SoffitWallLength) / 2;
            }
            if (PanelCut) // Checks to see if panel lengths were cut in half
            {
                RoofArea = RoofPanelLength * 2 * roofPanelNumber * panelWidth;
            }
            else
            {
                RoofArea = RoofPanelLength * roofPanelNumber * panelWidth;
            }
        }

        protected override void CalculateSunroom()
        {
            CalculatePanelLength(Pitch, PitchedWallLength);
            CalculateRoofPanels();
        }

        public void WallHeightPitch(double pitch, double soffitWallHeight)
        {
            Pitch = pitch;
            SoffitWallHeight = soffitWallHeight;
            SoffitHeight = SoffitWallLength - Overhang * Math.Tan(Pitch);
            AttachedHeight = SoffitHeight + PitchedWallLength * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = CalculateDripEdge(SoffitHeight, Pitch);
            CalculateSunroom();
        }

        public void WallHeightAttachedHeight(double soffitWallHeight, double peak)
        {
            SoffitWallHeight = soffitWallHeight;
            AttachedHeight = peak;
            Pitch = Math.Atan((AttachedHeight - SoffitWallHeight) / PitchedWallLength);
            SoffitHeight = SoffitWallHeight - Overhang * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = CalculateDripEdge(SoffitHeight, Pitch);
            CalculateSunroom();
        }

        public void MaxHeightPitch(double pitch, double maxHeight)
        {
            Pitch = pitch;
            MaxHeight = maxHeight;
            SoffitWallHeight = MaxHeight - PitchedWallLength * Math.Tan(Pitch) - Utilities.Angled(Pitch, Thickness);
            SoffitHeight = SoffitWallHeight - Overhang * Math.Tan(Pitch);
            AttachedHeight = MaxHeight - Utilities.Angled(Pitch, Thickness);
            DripEdge = CalculateDripEdge(SoffitHeight, Pitch);
            CalculateSunroom();
        }

        public void SoffitHeightAttachedHeight(double soffitHeight, double peak)
        {
            SoffitHeight = soffitHeight;
            AttachedHeight = peak;
            Pitch = Math.Atan((AttachedHeight - SoffitHeight) / (PitchedWallLength + Overhang));
            SoffitWallHeight = SoffitHeight + Overhang * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = CalculateDripEdge(SoffitHeight, Pitch);
            CalculateSunroom();
        }

        public void SoffitHeightPitch(double pitch, double soffitHeight)
        {
            Pitch = pitch;
            SoffitHeight = soffitHeight;
            SoffitWallHeight = SoffitHeight + Overhang * Math.Tan(Pitch);
            AttachedHeight = SoffitWallHeight + PitchedWallLength * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = CalculateDripEdge(SoffitHeight, Pitch);
            CalculateSunroom();
        }

        public void DripEdgeAttachedHeight(double dripEdge, double peak)
        {
            AttachedHeight = peak;
            DripEdge = dripEdge;
            double tolerance = 0.01;
            double diff = 100;
            double incr = 0.1;
            double ratioPitch = 0.0;
            double oldRatio, dripEstimate;
            while (diff > tolerance)
            {
                oldRatio = ratioPitch;
                ratioPitch += incr;
                Pitch = Math.Atan2(ratioPitch, 12.0);
                dripEstimate = Utilities.EstimateDripFromAttached(AttachedHeight, Pitch, PitchedWallLength,
                    Overhang, Thickness, AWall, BWall, CWall, Endcut, PanelWidth);
                diff = Math.Abs(DripEdge - dripEstimate);
                if (ratioPitch > 12.0){break;}

                if (dripEstimate < DripEdge)
                {
                    ratioPitch = oldRatio;
                    incr /= 2;
                }
            }

            SoffitWallHeight = AttachedHeight - PitchedWallLength * Math.Tan(Pitch);
            SoffitHeight = SoffitWallHeight - Overhang * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            CalculateSunroom();
        }

        public void DripEdgePitch(double dripEdge, double pitch)
        {
            Pitch = pitch;
            DripEdge = dripEdge;
            SoffitHeight = DripEdge - Utilities.Angled(Pitch, Thickness);
            SoffitWallHeight = SoffitHeight + Overhang * Math.Tan(Pitch);
            AttachedHeight = SoffitWallHeight + PitchedWallLength * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            CalculateSunroom();
        }
    }
}