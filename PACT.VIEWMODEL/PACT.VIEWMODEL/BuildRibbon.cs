//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Xml.Linq;
//using Microsoft.Windows.Controls.Ribbon;
//using System.Windows.Media.Imaging;
//using System.Windows;

//namespace PACT.MUKVIEWMODEL
//{
//    public class BuildRibbon
//    {
//        void buildmyrib()
//        {

//            var documentObject = XDocument.Load("strFileName");
//            IEnumerable<XElement> xRibbonTabList = documentObject.Descendants("RibbonTab");

//            // Get all SubMenuItems
//            foreach (XElement xRibbonTab in xRibbonTabList)
//            {
//                // Get the Main Menu Item name
//                string strRibbonTabName = xRibbonTab.Attribute("Name").Value;

//                // Create a RibbonTab
//                RibbonTab tab1 = new RibbonTab
//                {
//                    Header = strRibbonTabName,
//                    //Foreground = objFontBrushMainMenu,
//                };

//                // Get SubMenu for the current Menu
//                IEnumerable<XElement> xRibbinGroupList = xRibbonTab.Descendants("RibbonGroup");

//                foreach (XElement xRibbonGroup in xRibbinGroupList)
//                {
//                    string strGroupName = xRibbonGroup.Attribute("Name").Value;
//                    RibbonGroup group1 = new RibbonGroup
//                    {
//                        Header = strGroupName,
//                    };

//                    IEnumerable<XElement> xRibbinButtonList = xRibbonGroup.Descendants("RibbonButton");

//                    foreach (XElement xRibbonButton in xRibbinButtonList)
//                    {
//                        string strRibbonButtonName = xRibbonButton.Attribute("Name").Value;
//                        string strRibbonButtonCommand = xRibbonButton.Attribute("Command").Value;
//                        string strRibbonButtonImage = xRibbonButton.Attribute("Image").Value;

//                        BitmapImage bi = new BitmapImage();
//                        bi.BeginInit();
//                        bi.UriSource = new Uri(strRibbonButtonImage, UriKind.RelativeOrAbsolute);
//                        bi.EndInit();

//                        Style s = new Style(typeof(RibbonButton));
//                        s.Setters.Add(new Setter
//                        {
//                            Property = RibbonControlService.LabelProperty,
//                            Value = strRibbonButtonName

//                        });

//                        s.Setters.Add(new Setter
//                        {
//                            Property = RibbonControlService.LargeImageSourceProperty,
//                            Value = bi
//                        });

//                        // Create SubMenuItem
//                        RibbonButton objRibbonButton = new RibbonButton
//                        {
//                            //Foreground = objFontBrush,
//                            //Height = MENU_ITEM_HEIGHT,
//                            Command = (
//                            CommandSource.Instance).GetCommand(strRibbonButtonCommand),
//                            CommandParameter =
//                            new WorkSpaceCmdParameter
//                            {
//                                Parameter =
//                                new CallerInfo
//                                {
//                                    Name = strRibbonButtonName
//                                }
//                            },

//                            Style = s,
//                            FlowDirection =
//                            FlowDirection.LeftToRight
//                        };

//                        //Add Ribbon Buttons to Group
//                        group1.Items.Add(objRibbonButton);
//                    }

//                    // Add Ribbon Groups to Ribbon Tab
//                    tab1.Items.Add(group1);
//                }

//                // Add Ribbon Tabs to Main collection
//                this._RibbonControl.Add(tab1);
//            }
//        }
//    }
//}
