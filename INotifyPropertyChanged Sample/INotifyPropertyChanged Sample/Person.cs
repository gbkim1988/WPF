using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotifyPropertyChanged_Sample
{
    class Person : INotifyPropertyChanged
    {
        private string name;

        // event Declare
        public event PropertyChangedEventHandler PropertyChanged;

        public Person()
        {

        }

        public Person(string Value)
        {
            this.name = Value;

        }

        public string PersonName
        {
            get { return name; }
            set { name = value;  OnPropertyChanged("PersonName"); }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
