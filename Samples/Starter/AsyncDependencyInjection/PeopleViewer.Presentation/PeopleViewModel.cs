using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PeopleViewer.Presentation
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        protected readonly IPersonReader Reader;

        private IEnumerable<Person> _people;
        public IEnumerable<Person> People
        {
            get { return _people; }
            set
            {
                if (_people == value)
                    return;
                _people = value;
                RaisePropertyChanged("People");
            }
        }

        public PeopleViewModel(IPersonReader reader)
        {
            // Guard Clause
            //if (reader == null)
            //    throw new ArgumentNullException("'reader' parameter cannot be null");
            //Reader = reader;

            // Null Conditional Operator
            Reader = reader ?? throw new ArgumentNullException("'reader' parameter cannot be null");
        }

        public async Task RefreshPeople()
        {
            People = await Reader.GetPeopleAsync();
        }

        public void ClearPeople()
        {
            People = new List<Person>();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
