using System;
using System.Collections.Generic;
using System.IO;
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
using System.Reflection;

namespace PickerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int week = 9;
        private const int year = 2018;
        private bool header;

        public MainWindow()
        {
            InitializeComponent();
            header = false;

            DirectoryInfo di = new DirectoryInfo(@"T:\pub\analytics101\");
            foreach (DirectoryInfo subdir in di.GetDirectories())
            {
                foreach (FileInfo file in subdir.GetFiles())
                {
                    if (file.Name == "FootballObjects.dll")
                    {
                        WritePredictions(file.FullName);
                    }
                }
            }
        }

        private void WritePredictions(string AssemblyPath)
        {
            PredictWrapper pw = new PredictWrapper(AssemblyPath, 2018, week);
            Dictionary<string, string> predictions = pw.GetPredictions(week);

            int i = 1;
            if (!header)
            {
                DataGridTextColumn col1 = new DataGridTextColumn();
                col1.Header = "Contestant";
                col1.Binding = new Binding("Contestant");
                PickerGrid.Columns.Add(col1);

                foreach (string key in predictions.Keys)
                {
                    DataGridTemplateColumn col = new DataGridTemplateColumn();
                    col.Header = key;
                    col.Width = new DataGridLength(80);

                    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Image));
                    Binding b = new Binding("p" + i);
                    b.Mode = BindingMode.TwoWay;
                    factory.SetValue(Image.SourceProperty, b);
                    DataTemplate cellTemplate = new DataTemplate();
                    cellTemplate.VisualTree = factory;
                    col.CellTemplate = cellTemplate;

                    PickerGrid.Columns.Add(col);
                    i++;
                }            

                header = true;
            }

            Prediction p = new Prediction
            {
                Contestant = pw.PredictorName
            };

            i = 1;
            foreach (DataGridColumn c in PickerGrid.Columns)
            {
                string key = c.Header.ToString();
                if (predictions.ContainsKey(key))
                {
                    PropertyInfo pi = p.GetType().GetProperty("p" + i);
                    BitmapImage icon = new BitmapImage(new Uri(@"..\..\icons\" + predictions[key] + ".gif", UriKind.Relative));
                    pi.SetValue(p, icon);
                    i++;
                }
            }
            
            PickerGrid.Items.Add(p);
        }

        class Prediction
        {
            public string Contestant { get; set; }
            public ImageSource p1 { get; set; }
            public ImageSource p2 { get; set; }
            public ImageSource p3 { get; set; }
            public ImageSource p4 { get; set; }
            public ImageSource p5 { get; set; }
            public ImageSource p6 { get; set; }
            public ImageSource p7 { get; set; }
            public ImageSource p8 { get; set; }
            public ImageSource p9 { get; set; }
            public ImageSource p10 { get; set; }
            public ImageSource p11 { get; set; }
            public ImageSource p12 { get; set; }
            public ImageSource p13 { get; set; }
            public ImageSource p14 { get; set; }
            public ImageSource p15 { get; set; }
            public ImageSource p16 { get; set; }
        }
    }
}
