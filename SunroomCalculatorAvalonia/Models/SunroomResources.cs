using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia;
using SunroomCalculatorAvalonia.Views;

namespace SunroomCalculatorAvalonia.Models
{
    public class SunroomResources
    {
        public UserControl SunroomVM, ScenarioVM, FloorPlanVM, PanelVM, EndCutVM, InputDefaultVM, InterfaceVM;
        public Bitmap SunroomDefault, SunroomGable, SunroomStudio;
        public Bitmap SunroomFloorPlan, SunroomOverhang, SunroomPlumCut, SunroomPlumCutTop, SunroomSquareCut;
        static WindowIcon _icon = new(AppContext.BaseDirectory+Path.Combine("Assets", "SunroomIcon.ico"));

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
            SunroomVM = new SunroomView();
            ScenarioVM = new ScenarioView();
            FloorPlanVM = new FloorPlanView();
            PanelVM = new PanelView();
            EndCutVM = new EndCutView();
            InterfaceVM = new InterfaceView();
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
        public static void SunroomMessageBox(string title, string message)
        {
            var msBoxStandardWindow = MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBox.Avalonia.DTO.MessageBoxStandardParams{
                    ContentTitle = title,
                    ContentMessage = message,
                    Icon = MessageBox.Avalonia.Enums.Icon.Error,
                    WindowIcon = _icon,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                });
            msBoxStandardWindow.Show();
        }
    }
}