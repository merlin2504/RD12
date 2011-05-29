using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;
using Microsoft.Practices.Prism.Commands;
using PACT.COMMON;
using System.Windows.Input;

namespace PACT.MODEL
{
    public class RibbonGenerator
    {
        private ObservableCollection<TabData> _tabDataCollection;
        //private ObservableCollection<ControlData> _controlDataCollection;
        //private ObservableCollection<GroupData> _groupDataCollection;
        //private ObservableCollection<TabData> _RibbonControlData;

        public ObservableCollection<TabData> GetControls()
        {
            {
                TabData td; 
                GroupData GrpData;
                _tabDataCollection = new ObservableCollection<TabData>();

                Uri smallImage = new Uri("/PACT.VIEW;component/Images/SmallIcon.png", UriKind.Relative);
                Uri largeImage = new Uri("/PACT.VIEW;component/Images/LargeIcon.png", UriKind.Relative);

                #region Inventory Tab
                td = new TabData("Inventory");
                GrpData = new GroupData("Products");
                GrpData.ControlDataCollection.Add(new ButtonData()
                {
                    Label = "Products",
                    LargeImage = largeImage,
                    ToolTipTitle = "ToolTip Title",
                    ToolTipDescription = "ToolTip Description",
                    ToolTipImage = smallImage,
                    //Command = RibbonViewModel.cmdLoadPage,
                    Tag = "2000"

                });

                GrpData.ControlDataCollection.Add(new ButtonData()
                {
                    Label = "New",
                    SmallImage = smallImage,
                    ToolTipTitle = "ToolTip Title",
                    ToolTipDescription = "ToolTip Description",
                    ToolTipImage = smallImage,
                    //Command = RibbonViewModel.cmdLoadPage,
                    Tag = "2001"
                });

                td.GroupDataCollection.Add(GrpData);

                GrpData = new GroupData("Sales");
                MenuButtonData mb = new MenuButtonData()
                {

                    Label = "Sales",
                    LargeImage = largeImage,
                    ToolTipTitle = "ToolTip Title",
                    ToolTipDescription = "ToolTip Description",
                    ToolTipImage = smallImage,
                    //Command = RibbonViewModel.cmdLoadPage,
                    Tag = "2100"
                };
                MenuItemData BtnMenu = new MenuItemData();
                BtnMenu.Label = "Hyderabad Sales Invoice";
                BtnMenu.SmallImage = smallImage;
                BtnMenu.KeyTip = "H";
                mb.ControlDataCollection.Add(BtnMenu);

                BtnMenu = new MenuItemData();
                BtnMenu.Label = "Chennai Sales Invoice";
                BtnMenu.SmallImage = smallImage;
                mb.ControlDataCollection.Add(BtnMenu);


                GrpData.ControlDataCollection.Add(mb);

                td.GroupDataCollection.Add(GrpData);

                td.GroupDataCollection.Add(new GroupData("Purchase")
                {
                    LargeImage = largeImage,
                    SmallImage = smallImage
                });

                _tabDataCollection.Insert(0, td);
                #endregion

                #region Home Tab
                td = new TabData("Home");

                td.GroupDataCollection.Add(new GroupData("DashBoard")
                {
                    LargeImage = largeImage,
                    SmallImage = smallImage
                });

                td.GroupDataCollection.Add(new GroupData("Sales DashBoard")
                {
                    LargeImage = largeImage,
                    SmallImage = smallImage
                });

                _tabDataCollection.Insert(0, td);
                #endregion

                #region Accounting Tab
                //Accounts Tab
                td = new TabData("Accounting");
                GrpData = new GroupData("Chart Of Accounts");
                GrpData.ControlDataCollection.Add(new ButtonData()
                {
                    Label = "Accounts",
                    LargeImage = largeImage,
                    ToolTipTitle = "ToolTip Title",
                    ToolTipDescription = "ToolTip Description",
                    ToolTipImage = smallImage,
                   // Command = RibbonViewModel.cmdLoadPage,
                    Tag = "1"

                });
                GrpData.ControlDataCollection.Add(new ButtonData()
                {
                    Label = "Depreciation",
                    SmallImage = smallImage,
                    ToolTipTitle = "ToolTip Title",
                    ToolTipDescription = "ToolTip Description",
                    ToolTipImage = smallImage,
                    //Command = RibbonViewModel.cmdLoadPage,
                    Tag = "4"
                });
                td.GroupDataCollection.Add(GrpData);

                td.GroupDataCollection.Add(new GroupData("Receipts")
                {
                    LargeImage = largeImage,
                    SmallImage = smallImage
                });

                td.GroupDataCollection.Add(new GroupData("Payments")
                {
                    LargeImage = largeImage,
                    SmallImage = smallImage
                });

                _tabDataCollection.Insert(0, td);
                #endregion

                

                return _tabDataCollection;
            }
           
        }
    }
}
