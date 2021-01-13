using System;

namespace SunroomLib
{
    public class Studio : Sunroom, IStudio
    {
        private double _pitch, _attachedHeight, _maxHeight, _soffitWallLength, _soffitWallHeight, _soffitHeight, 
            _dripEdge, _pitchedWallLength, _roofArea;

        public double Pitch
        {
            get => _pitch;
            set => _pitch = value;
        }

        public double AttachedHeight
        {
            get => _attachedHeight;
            set => _attachedHeight = value;
        }

        public double MaxHeight
        {
            get => _maxHeight;
            set => _maxHeight = value;
        }

        public double SoffitWallLength
        {
            get => _soffitWallLength;
            set => _soffitWallLength = value;
        }

        public double SoffitWallHeight
        {
            get => _soffitWallHeight;
            set => _soffitWallHeight = value;
        }

        public double SoffitHeight
        {
            get => _soffitHeight;
            set => _soffitHeight = value;
        }

        public double DripEdge
        {
            get => _dripEdge;
            set => _dripEdge = value;
        }

        public double PitchedWallLength
        {
            get => _pitchedWallLength;
            set => _pitchedWallLength = value;
        }

        public double RoofArea => _roofArea;

        public Studio(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut, 
            string panelWidth) : base(aWall, bWall, cWall, overhang, thickness, endCut, panelWidth)
        {
            SoffitWallLength = BWall;
            PitchedWallLength = Math.Max(AWall, CWall);
        }

        private void CalculateRoofPanels()
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
                _roofArea = RoofPanelLength * 2 * roofPanelNumber * panelWidth;
            }
            else
            {
                _roofArea = RoofPanelLength * roofPanelNumber * panelWidth;
            }
        }

        protected override void CalculateSunroom()
        {
            CalculatePanelLength(_pitch, _pitchedWallLength);
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
            throw new System.NotImplementedException();
        }

        public void MaxHeightPitch(double pitch, double maxH)
        {
            throw new System.NotImplementedException();
        }

        public void SoffitHeightAttachedHeight(double soffitHeight, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void SoffitHeightPitch(double pitch, double soffitHeight)
        {
            throw new System.NotImplementedException();
        }

        public void DripEdgeAttachedHeight(double dripEdge, double peak)
        {
            throw new System.NotImplementedException();
        }

        public void DripEdgePitch(double dripEdge, double pitch)
        {
            throw new System.NotImplementedException();
        }
    }
}