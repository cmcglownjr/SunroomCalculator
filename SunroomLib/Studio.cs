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
            _dripEdge, _pitchedWallLength, _roofArea, _sideOverhang;
        public bool PanelCut;
        public int NumPanelCuts, RoofPanelLength, PanelType;
        public double Pitch // The roof pitch
        {
            get => _pitch;
            private set
            {
                if (value < Math.Atan(4.0 / 12.0))
                {
                    throw new DataException($"The pitch is less than 4/12 and is considered too low.");
                }
                if (value > Math.Atan(9.0 / 12.0))
                {
                    throw new DataException($"The pitch is greater than 9/12 and is considered too steep.");
                }
                _pitch = value;
            }
        }

        public double AttachedHeight // The height where the bottom of the panels attach to the existing structure
        {
            get => _attachedHeight;
            private set => _attachedHeight = value;
        }

        public double MaxHeight // The maximum height of the sunroom with panels
        {
            get => _maxHeight;
            private set => _maxHeight = value;
        }
        
        public double SoffitWallLength // This will be set to B Wall since it isn't angled
        {
            get => _soffitWallLength;
            private set => _soffitWallLength = value;
        }
        public double PitchedWallLength // This will be set to A or C Wall because they are angled
        {
            get => _pitchedWallLength;
            private set => _pitchedWallLength = value;
        }
        public double SoffitWallHeight // The height of B Wall
        {
            get => _soffitWallHeight;
            private set => _soffitWallHeight = value;
        }

        public double SoffitHeight // The distance from the ground to the soffit
        {
            get => _soffitHeight;
            private set => _soffitHeight = value;
        }

        public double DripEdge // The distance from the ground to the drip edge
        {
            get => _dripEdge;
            private set => _dripEdge = value;
        }
        
        public double RoofArea // The area of the roof with panels
        {
            get => _roofArea;
            private set => _roofArea = value;
        }
        public double SideOverhang
        {
            get => _sideOverhang;
            private set => _sideOverhang = value;

        }

        public Studio(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut, 
            string panelWidth) : base(aWall, bWall, cWall, overhang, thickness, endCut, panelWidth)
        {
            SoffitWallLength = BWall;
            PitchedWallLength = Math.Max(AWall, CWall);
            if (overhang > Utilities.StandardPanelWidths[panelWidth] / 2)
            {
                SideOverhang = Utilities.StandardPanelWidths[panelWidth] / 2;
            }
            else
            {
                SideOverhang = overhang;
            }
        }
        protected override void CalculatePanelLength()
        {
            double panelLength;
            if (Endcut == "SquareCut")
            {
                panelLength = (PitchedWallLength + Overhang) / Math.Cos(Pitch);
            }
            else
            {
                var panelBottom = (PitchedWallLength + Overhang) / Math.Cos(Pitch);
                var panelTop = (PitchedWallLength + Overhang + Thickness * Math.Sin(Pitch)) / Math.Cos(Pitch);
                panelLength = Math.Max(panelBottom, panelTop);
            }
            RoofPanelLength = Convert.ToInt32(Math.Ceiling(panelLength / 12) * 12);
            while (RoofPanelLength > 192)
            {
                // Cut panel lengths in half because the lengths exceed allowed threshold
                PanelCut = true;
                RoofPanelLength /= 2;
                NumPanelCuts += 1;
            }

            foreach (var panelStandard in Utilities.StandardPanelLengths.Keys)
            {
                if (RoofPanelLength <= panelStandard)
                {
                    PanelType = panelStandard;
                    break;
                }
            }
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
            CalculatePanelLength();
            CalculateRoofPanels();
        }

        public void WallHeightPitch(double pitch, double soffitWallHeight)
        {
            Pitch = pitch;
            SoffitWallHeight = soffitWallHeight;
            SoffitHeight = SoffitWallLength - Overhang * Math.Tan(Pitch);
            AttachedHeight = SoffitHeight + PitchedWallLength * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = Utilities.CalculateDripEdge(SoffitHeight, Pitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void WallHeightAttachedHeight(double soffitWallHeight, double peak)
        {
            SoffitWallHeight = soffitWallHeight;
            AttachedHeight = peak;
            Pitch = Math.Atan((AttachedHeight - SoffitWallHeight) / PitchedWallLength);
            SoffitHeight = SoffitWallHeight - Overhang * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = Utilities.CalculateDripEdge(SoffitHeight, Pitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void MaxHeightPitch(double pitch, double maxHeight)
        {
            Pitch = pitch;
            MaxHeight = maxHeight;
            SoffitWallHeight = MaxHeight - PitchedWallLength * Math.Tan(Pitch) - Utilities.Angled(Pitch, Thickness);
            SoffitHeight = SoffitWallHeight - Overhang * Math.Tan(Pitch);
            AttachedHeight = MaxHeight - Utilities.Angled(Pitch, Thickness);
            DripEdge = Utilities.CalculateDripEdge(SoffitHeight, Pitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void SoffitHeightAttachedHeight(double soffitHeight, double peak)
        {
            SoffitHeight = soffitHeight;
            AttachedHeight = peak;
            Pitch = Math.Atan((AttachedHeight - SoffitHeight) / (PitchedWallLength + Overhang));
            SoffitWallHeight = SoffitHeight + Overhang * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = Utilities.CalculateDripEdge(SoffitHeight, Pitch, Thickness, Endcut);
            CalculateSunroom();
        }

        public void SoffitHeightPitch(double pitch, double soffitHeight)
        {
            Pitch = pitch;
            SoffitHeight = soffitHeight;
            SoffitWallHeight = SoffitHeight + Overhang * Math.Tan(Pitch);
            AttachedHeight = SoffitWallHeight + PitchedWallLength * Math.Tan(Pitch);
            MaxHeight = AttachedHeight + Utilities.Angled(Pitch, Thickness);
            DripEdge = Utilities.CalculateDripEdge(SoffitHeight, Pitch, Thickness, Endcut);
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
                    Overhang, Thickness, Endcut);
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