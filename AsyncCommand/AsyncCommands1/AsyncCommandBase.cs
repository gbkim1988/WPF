using System;
using System.Threading.Tasks;
using System.Windows.Input;

public abstract class AsyncCommandBase : IAsyncCommand
{
    // 명령을 현재 상태에서 실행할 수 있는지를 결정하는 메서드를 정의
    public abstract bool CanExecute(object parameter);

    public abstract Task ExecuteAsync(object parameter);

    // 명령이 호출될 때 호출될 메서드를 정의합니다.
    public async void Execute(object parameter)
    {
        await ExecuteAsync(parameter);
    }
    
    // 이벤트 
    // 명령을 실행해야 하는지 여부에 영향을 주는 변경이 발생할때 발생
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    protected void RaiseCanExecuteChanged()
    {
        // Forces the CommandManager to raise the CommandManager.RequerySuggested event.
        CommandManager.InvalidateRequerySuggested();
    }
}