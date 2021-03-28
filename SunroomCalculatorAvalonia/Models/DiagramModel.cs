using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace SunroomCalculatorAvalonia.Models
{
    public class DiagramModel
    {
        public static WindowIcon? SunroomIcon;
        public Bitmap SunroomDefault, SunroomGable, SunroomStudio;
        public Bitmap SunroomFloorPlan, SunroomOverhang, SunroomPlumCut, SunroomPlumCutTop, SunroomSquareCut;
        public Bitmap GableDripEdgeAttached,
            GableDripEdgePitch,
            GableMaxPitch,
            GableSoffitAttached,
            GableSoffitPitch,
            GableWallAttached,
            GableWallPitch;
        public Bitmap StudioDripEdgeAttached,
            StudioDripEdgePitch,
            StudioMaxPitch,
            StudioSoffitAttached,
            StudioSoffitPitch,
            StudioWallAttached,
            StudioWallPitch;

        public DiagramModel()
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            SunroomIcon = new(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/SunroomIcon.ico")));
            // Sunroom Styles
            SunroomDefault =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/porch-1046156_640.jpg")));
            SunroomGable =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Sunroom_Gable.jpg")));
            SunroomStudio =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Sunroom_Studio.jpg")));
            // Common
            SunroomFloorPlan =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Sunroom-floorplan.png")));
            SunroomOverhang =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Sunroom-overhang.png")));
            SunroomPlumCut =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Sunroom-plumcut.png")));
            SunroomPlumCutTop =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Sunroom-plumcuttop.png")));
            SunroomSquareCut =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Sunroom-squarecut.png")));
            // Gable
            GableDripEdgeAttached =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Gable-dripedge_attachedheight.png")));
            GableDripEdgePitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Gable-dripedge_pitch.png")));
            GableMaxPitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Gable-maxheight_pitch.png")));
            GableSoffitAttached =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Gable-soffitheight_attachedheight.png")));
            GableSoffitPitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Gable-soffitheight_pitch.png")));
            GableWallAttached =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Gable-wallheight_attachedheight.png")));
            GableWallPitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Gable-wallheight_pitch.png")));
            // Studio
            StudioDripEdgeAttached =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Studio-dripedge_attachedheight.png")));
            StudioDripEdgePitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Studio-dripedge_pitch.png")));
            StudioMaxPitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Studio-maxheight_pitch.png")));
            StudioSoffitAttached =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Studio-soffitheight_attachedheight.png")));
            StudioSoffitPitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Studio-soffitheight_pitch.png")));
            StudioWallAttached =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Studio-wallheight_attachedheight.png")));
            StudioWallPitch =
                new Bitmap(assets.Open(new Uri("avares://SunroomCalculatorAvalonia/Assets/Studio-wallheight_pitch.png")));
        }
    }
}