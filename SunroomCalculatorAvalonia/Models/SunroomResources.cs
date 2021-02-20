using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SunroomCalculatorAvalonia.Views;

namespace SunroomCalculatorAvalonia.Models
{
    public class SunroomResources
    {
        public UserControl Input1VM, Input2VM, Input3VM, Input4VM, Input5VM, InputDefaultVM;
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
        public SunroomResources()
        {
            Input1VM = new Input1View();
            Input2VM = new Input2View();
            Input3VM = new Input3View();
            Input4VM = new Input4View();
            Input5VM = new Input5View();
            InputDefaultVM = new InputDefaultView();
            
            SunroomDefault = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "porch-1046156_640.jpg"));
            SunroomGable = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Sunroom_Gable.jpg"));
            SunroomStudio = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Sunroom_Studio.jpg"));
            
            SunroomFloorPlan = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Sunroom-floorplan.png"));
            SunroomOverhang = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Sunroom-overhang.png"));
            SunroomPlumCut = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Sunroom-plumcut.png"));
            SunroomPlumCutTop = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Sunroom-plumcuttop.png"));
            SunroomSquareCut = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Sunroom-squarecut.png"));
            
            GableDripEdgeAttached = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Gable-dripedge_attachedheight.png"));
            GableDripEdgePitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Gable-dripedge_pitch.png"));
            GableMaxPitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Gable-maxheight_pitch.png"));
            GableSoffitAttached = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Gable-soffitheight_attachedheight.png"));
            GableSoffitPitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Gable-soffitheight_pitch.png"));
            GableWallAttached = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Gable-wallheight_attachedheight.png"));
            GableWallPitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Gable-wallheight_pitch.png"));
            
            StudioDripEdgeAttached = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Studio-dripedge_attachedheight.png"));
            StudioDripEdgePitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Studio-dripedge_pitch.png"));
            StudioMaxPitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Studio-maxheight_pitch.png"));
            StudioSoffitAttached = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Studio-soffitheight_attachedheight.png"));
            StudioSoffitPitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Studio-soffitheight_pitch.png"));
            StudioWallAttached = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Studio-wallheight_attachedheight.png"));
            StudioWallPitch = new Bitmap(AppContext.BaseDirectory+Path.Combine("Assets", "Studio-wallheight_pitch.png"));
        }
    }
}