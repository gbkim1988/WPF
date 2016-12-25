using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication3
{
    class RelayCommand : ICommand
    {
        // Action 은 무엇?
        private Action<object> execute;
        //  Predicate 는 무엇?
        private Predicate<object> canExecute;
        // EventHandler 는 무엇?
        private event EventHandler CanExecuteChangedIternal;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        // DefaultCanExecute 는 무엇?
        {

        }

        // 생성자 정의 
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentException("execute");
            if (canExecute == null)
                throw new ArgumentException("canExecute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            // CommandManager.RequerySuggested 는 button 을
            // 활성화 비활성화 하는 역할을한다. 즉, toggle 버튼화 
            // 시켜줌 
            // add 접근자 
            add
            {
                // += 연산자는 add 연산자
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedIternal += value;
            }
            // remove 접근자 
            remove
            {
                // -= 연산자는 remove 연산자 
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedIternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            // && 연산자는 앞의 조건이 true 일 경우 뒤의 조건이 실행된다. 
            // 만약, this.canExecute 가 null 이라면 False 를 리턴
            // 만약, this.canExecute 가 null 이 아니라면, 
            // this.canExecute(parameter)를 실행한다. 
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(object paremter)
        {
            this.execute(paremter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedIternal;

            if (handler != null)
                handler.Invoke(this, EventArgs.Empty);
        }

        public void Destroy()
        {
            // 아래의 문법은 특이하군 알아봐야겠다 
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }

        public static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }

}
