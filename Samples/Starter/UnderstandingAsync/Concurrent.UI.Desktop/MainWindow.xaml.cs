using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TaskAwait.Library;
using TaskAwait.Shared;

namespace Concurrent.UI
{
    public partial class MainWindow : Window
    {
        PersonReader reader = new PersonReader();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FetchWithTaskButton_Click(object sender, RoutedEventArgs e)
        {
            FetchWithTaskButton.IsEnabled = false;
            ClearListBox();

            Task<List<Person>> peopleTask = reader.GetPeopleAsync();

            List<Person> people = new List<Person>();

            // Get the List<Person> result from the async method here

            foreach (var person in people)
                PersonListBox.Items.Add(person);

            FetchWithTaskButton.IsEnabled = true;

            #region continuation
            //peopleTask.ContinueWith(task =>
            //    {
            //        try
            //        {
            //            List<Person> people = task.Result;
            //            foreach (var person in people)
            //                PersonListBox.Items.Add(person);
            //        }
            //        finally
            //        {
            //            FetchWithTaskButton.IsEnabled = true;
            //        }
            //    }
            //        //, TaskScheduler.FromCurrentSynchronizationContext()
            //    );
            #endregion
        }

        private void FetchWithAwaitButton_Click(object sender, RoutedEventArgs e)
        {
            FetchWithAwaitButton.IsEnabled = false;
            ClearListBox();

            var people = new List<Person>();

            // Get the List<Person> result from the async method here

            foreach (var person in people)
                PersonListBox.Items.Add(person);

            FetchWithAwaitButton.IsEnabled = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Cancellation has not been implemented
            // See "Completed" code for an example.
        }

        private void ClearListBox()
        {
            PersonListBox.Items.Clear();
        }
    }
}
