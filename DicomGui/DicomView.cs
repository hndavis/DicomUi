using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DicomGui
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DicomGui"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DicomGui;assembly=DicomGui"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:DicomView/>
    ///
    /// 
    /// </summary>
    /// 
    
    [TemplatePart(Name="MainBorder", Type=typeof(Border))]
    [TemplatePart(Name = "Body", Type=typeof(ContentControl))]
    public class DicomView : Control
    {



        static DicomView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (DicomView),
                new FrameworkPropertyMetadata(typeof (DicomView)));


            CommandManager.RegisterClassCommandBinding(typeof (DicomView),
                new CommandBinding(DicomView.CustomCommand, OnCustomCommand));
        }


        private Border MainBorder;
        private ContentControl Body;

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof (object), typeof (DicomView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));


        public static readonly ICommand CustomCommand = new RoutedUICommand("CustomCommand", "CustomCommand",
            typeof (DicomView),
            new InputGestureCollection(
                new InputGesture[]
                {
                    new KeyGesture(Key.Enter),
                    new MouseGesture(MouseAction.LeftClick)
                }));



        public object Content
        {
            get
            {
                return (object) GetValue(ContentProperty);
               
            }
            set { SetValue(ContentProperty, value); }
        }

        static void OnCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
            DicomView invoker = sender as DicomView;
            //Do whatever you need

        }


        static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DicomView invoker = sender as DicomView;
            //Do handle event
            if ( invoker == null )
            { throw new Exception(" Mouse Down has no event handler");}

            //Raise your event
            invoker.OnInvertCall();

            //Do Rest
        }

        public static readonly RoutedEvent InvertCallEvent = EventManager.RegisterRoutedEvent
            ("InvertCall", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (DicomView));

        public event RoutedEventHandler InvertCall
        {
            add {  AddHandler(InvertCallEvent,value);}
            remove {  RemoveHandler(InvertCallEvent, value);}
        }

        private void OnInvertCall()
        {
         
            RoutedEventArgs args = new RoutedEventArgs(InvertCallEvent);   
            RaiseEvent(args);
        }


    }
}
