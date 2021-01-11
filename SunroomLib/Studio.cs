using System;

namespace SunroomLib
{
    public class Studio : Sunroom, IStudio
    {
        private double _pitch, _attachedHeight, _maxHeight, _unpitchedWallHeight, _soffitHeight, _dripEdge, 
            _pitchedWallHeight;

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

        public double UnpitchedWallHeight
        {
            get => _unpitchedWallHeight;
            set => _unpitchedWallHeight = value;
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

        public double PitchedWallHeight
        {
            get => _pitchedWallHeight;
            set => _pitchedWallHeight = value;
        }

        public Studio(double aWall, double bWall, double cWall, double overhang, double thickness, string endCut) : 
            base(aWall, bWall, cWall, overhang, thickness, endCut)
        {
            UnpitchedWallHeight = base.BWall;
            PitchedWallHeight = Math.Max(AWall, CWall);
        }

        private void CalculateRoofPanels(double soffitWall)
        {}

        public void WallHeightPitch(double pitch, double soffitWallHeight)
        {
            throw new System.NotImplementedException();
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