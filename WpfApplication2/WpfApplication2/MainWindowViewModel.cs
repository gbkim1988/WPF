using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApplication2.Models;

namespace WpfApplication2
{
    class MainWindowViewModel
    {


        public MainWindowViewModel()
        {
        }

        private ICommand textBoxButtonCmdWithParameter;

        public ICommand TextBoxButtonCmdWithParameter
        {
            get
            {
                return this.textBoxButtonCmdWithParameter ?? (this.textBoxButtonCmdWithParameter = new SimpleCommand
                {
                    CanExecuteDelegate = x => true,
                    ExecuteDelegate = async x =>
                    {
                        if (x is String)
                        {
                            // 웹 리퀘스트를 전송하는 코드를 추가 

                            await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync("TextBox Button with parameter was clicked!",
                                                                                                  string.Format("Parameter: {0}", x));
                        }
                    }
                });
            }
        }
    }
}
