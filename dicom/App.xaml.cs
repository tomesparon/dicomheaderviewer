using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Linq;




namespace dicom
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window1 window1 = new Window1();

            if (e.Args.Length > 0 && System.IO.File.Exists(e.Args[0]))
            {
                string dicomFile = e.Args[0];
                window1.LoadDicomFile(dicomFile);
            }

            window1.Show();
        }
    }
}
