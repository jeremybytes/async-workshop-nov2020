using PeopleViewer.Presentation;
using PersonReader.CSV;
using PersonReader.Service;
using System;
using System.Windows;

namespace PeopleViewer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }

        private static void ComposeObjects()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "People.txt";
            var reader = new CSVReader(new CSVFilePath(path));
            //var readerUri = new ServiceReaderUri("http://localhost:9874");
            //var reader = new ServiceReader(readerUri);
            var viewModel = new PeopleViewModel(reader);
            Application.Current.MainWindow = new MainWindow(viewModel);
        }

        private static void AlternateComposeObjects()
        {
            Application.Current.MainWindow = 
                new MainWindow(
                    new PeopleViewModel(
                        new CSVReader(
                            new CSVFilePath(
                                AppDomain.CurrentDomain.BaseDirectory + "People.txt"))));
        }
    }
}
