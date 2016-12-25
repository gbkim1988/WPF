https://msdn.microsoft.com/ko-kr/magazine/mt149362?author=stephen+cleary

stephen cleary --

WPF MMVM image
https://www.codeproject.com/KB/WPF/813345/MVVM_image.png

MVVM 구조는 다음의 요소들을 가진다.
View, View's Code Behind, ViewModel, Model

View 와 ViewModel 은 Notification, Data Binding, Commands 라고 명명된
통신을 통해서 데이터를 교환한다.  

클래스의 이름을 짓기 위해 아래의 규칙을 사용한다.
- View 는 접미사로 View 의 이름 뒤에 붙인다. (e.g.: StudentListView)
- ViewModel 은 ViewModel 의 이름뒤에 붙인다. (e.g.: StudentListViewModel)
- Model 은 Model 의 이름 뒤에 붙인다. (e.g.: StudentModel)

MVVM 에서 commands 를 어떻게 사용하는지 확인한다.

1. WPF 프로젝트를 생성, MainWindow 의 이름을 MainWindowView 로 명명한다.
2. MainWindowViewModel 클래스를 생성한다.
3. View 에게 ViewModel 이 누구인지를 알려준다. 이는 View 의 Data Context 에서 설정
* Data Context 는 xaml 파일 내부에 정의하는 Tag Element 이다. ViewModel 을 식별하
는데 도움을준다.

MainWindowView.xaml 코드 샘플

<Window x:Class="WpfExample.MainWindowView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="350" Width="525"
        xmlns:local="clr-namespace:WpfExample">

    <Window.DataContext>
        <local:MainWindowViewModel/>
    </Window.DataContext>

    <Grid>
    </Grid>
</Window>

!! UserControl 과 같은 개체에도 DataContext 속성이 존재하는가?
 -> 테스트해보기

 위의 코드에서 local 은 WpfExample 의 alias 이다. 이것은 framework 로 하여금
 MainWindowViewModel 이 어디에 존재하는지 알려준다.

 이제, MVVM 패턴에 따라 MainWindowView 는 ViewModel 이 MainWindowViewModel 임을
 알고 있다.

 간단한 바인딩을 통해 이를 검증하자.

 View 에 Button 을 추가하고 ViewModel 을 통해 Button Content 를 설정해보자

코드 샘플

MainWindowView.xaml.cs

<Window x:Class=" WpfMvvmExample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:local="clr-namespace:WpfMvvmExample"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="350" Width="525">

    <Window.DataContext>
        <local:MainWindowViewModel/>
    </Window.DataContext>

    <Grid>
        <Button Width="100"
        Height="100" Content="{Binding ButtonContent}"/>
    </Grid>
</Window>

ViewModel

namespace WpfExample
{
    class MainWindowViewModel
    {
        public string ButtonContent
        {
            get
            {
                return "Click Me";
            }
        }
    }
}

위의 코드는 View 로 하여금 Content를 ViewModel 의  ButtonContent Property 로부터
가져오도록 알려준다.

ICommand Iterface로 이동한다.

WPF 의 Commands 를 이용하여 Button 에 기능을 부여해보자

Commands 는 MVVM 아키텍처에 따라 model 을 업데이트하는 메커니즘을 view 에게 제공
한다. ICommand 인터페이스를 살펴보자

bool CanExecute(object parameter)
void Execute(object parameter)
event EventHandler CanExecuteChanged;

우리는 간단한 어플리케이션을 만들어 볼것이다. 이 어플리케이션은 Hi 메시지를 버튼클릭시
보여준다. 또다른 버튼을 추가해본다 단, toggle 속성을 부여한다.
RelayCommand 라는 클래스를 생성한다. ICommand 인터페이스를 구현하며(Implement) 이
클래스는 ICommand 의 개선된 코드이며 각각의 클래스의 boilerplate code를 추출한다. (
    즉, 불필요한 코드를 한곳에 모아서 관리한다는 의미이다.
)

Boilerplate code : 컴퓨터 프로그래밍에서 boilerplate code 또는 boilerplate 란 코드의
섹션을 의미하며 이 코드의 섹션은 많은 장소에 변경이 없거나 매우 적은 변경이 가해진뒤에
포함되는 코드섹션이다.

Async Programming : Patterns for Asynchronous MVVM Applications: Commands

https://msdn.microsoft.com/en-us/magazine/dn630647.aspx

MVVM Pattern 과 async, await 을 결합하는 두번째 article 이다.

지난 시간에 data 를 asynchronous operation 과 바인딩하는 방법을 보였다.

first article : msdn.microsoft.com/magazine/dn605875

해당 application 을 작성하는 중에 다양한 단순화가 있다.

단순히 asynchronous commands 에만 초점을 맞추었다.

첫째, command execute parameter 를 사용하지 않았다. 이는 실무에서 이를 사용하지
않기 때문이다.
둘째, ICommand.CanExecuteChanged 를 구현하지 않았다. MVVM 플래폼에서 메모리 누수를
일으킨다. 코드를 간결하게 하기 위해 WPF 자체의 CommandManager를 CanExecuteChanged를
구현하는데 사용하였다.

간단한 Serivce Layer 를 사용하였다. single static method 이다. 해당 service layer는
cacellation 을 지원한다.


Figure 3 The Service Layer
public static class MyService
{
  // bit.ly/1fCnbJ2
  public static async Task<int> DownloadAndCountBytesAsync(string url,
    CancellationToken token = new CancellationToken())
  {
    await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
    var client = new HttpClient();
    using (var response = await client.GetAsync(url, token).ConfigureAwait(false))
    {
      var data = await
        response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
      return data.Length;
    }
  }
}

* Asynchronous Commands
시작하기 전에 ICommand 인터페이스를 살펴보자

public interface ICommand
{
  event EventHandler CanExecuteChanged;
  bool CanExecute(object parameter);
  void Execute(object parameter);
}

CanExecuteChagned 와 파라미터를 부시하고 asynchronous command 가 어떻게 이 인터페이스와
동작할지를 생각해보자.

CanExecute 메서드는 반드시 synchronous 이어야 한다. asynchronous 가 될 수 있는 멤버
는 Execute 이다.

Execute 메서드는 synchronous 구현을 위해 설계되었다.
"Best Practices in Asynchronous Programming" 에 따르면 async void 메서드는
반드시 피해야한다. event handler 가 아니라면, 혹은 논리적으로 event handler 와
동일하다면 말이다. ICommand.Execute 는 논리적으로 event handler 이다. 따라서
async void 가 될 수 있다. (msdn.microsoft.com/magazine/jj991977)

하지만, async void 메서드내의 코드를 최소화시키고 async Task 메서드
를 노출시키는 것이 최선이다. 이 practice 는 코드를 좀더 테스틑 가능하게 만든다.
이러한 견지에서 필자는 다음의 asynchronous command interface 를 제안한다.

asynchronous Command Interface 제안

public interface IAsyncCommand : ICommand
{
  Task ExecuteAsync(object parameter);
}

Figure 4 Base Type for Asynchronous Commands
public abstract class AsyncCommandBase : IAsyncCommand
{
  public abstract bool CanExecute(object parameter);
  public abstract Task ExecuteAsync(object parameter);
  public async void Execute(object parameter)
  {
    await ExecuteAsync(parameter);
  }
  public event EventHandler CanExecuteChanged
  {
    add { CommandManager.RequerySuggested += value; }
    remove { CommandManager.RequerySuggested -= value; }
  }
  protected void RaiseCanExecuteChanged()
  {
    CommandManager.InvalidateRequerySuggested();
  }
}

Base Class 는 두가지를 고려한다. CanExecuteChanged 구현을 CommandManger로
펀트한다. 그리고 async void ICommand.Execute 메서드를 구현한다. 이는
ExecuteAsync 메서드를 호출하면서 이루어진다. 이 메서드는 결과를 기다린다.
이는 어떠한 예외가 UI 쓰레드에 적절히 전달되었는지 확인하기 위해서다. (await
키워드를 ExecuteAsync 메서드에 사용한 이유), Thread 는 시간과의 싸움이구나

이러한 기반작업 위에 효율적인 asynchronous command 를 개발할 준비가 되었다.
표준 delegate 타입은 action 이다. 비동기적으로 동등한 것은 Func<Task> 이다.
Figure 5는 delegate-based AsyncCommand 를 보여준다.


Figure 5 The First Attempt at an Asynchronous Command
public class AsyncCommand : AsyncCommandBase
{
  private readonly Func<Task> _command;
  public AsyncCommand(Func<Task> command)
  {
    _command = command;
  }
  public override bool CanExecute(object parameter)
  {
    return true;
  }
  public override Task ExecuteAsync(object parameter)
  {
    return _command();
  }
}

여기서 UI 는 URL 을 위한 textbox  와 HTTP request 를 전송하는 button 이다.
그리고 label 은 result 를 위해서 배치하였다.

<Grid>
  <TextBox Text="{Binding Url}" />
  <Button Command="{Binding CountUrlBytesCommand}"
      Content="Go" />
  <TextBlock Text="{Binding ByteCount}" />
</Grid>


Figure 6 The First MainWindowViewModel
public sealed class MainWindowViewModel : INotifyPropertyChanged
{
  public MainWindowViewModel()
  {
    Url = "http://www.example.com/";
    CountUrlBytesCommand = new AsyncCommand(async () =>
    {
      ByteCount = await MyService.DownloadAndCountBytesAsync(Url);
    });
  }
  public string Url { get; set; } // Raises PropertyChanged
  public IAsyncCommand CountUrlBytesCommand { get; private set; }
  public int ByteCount { get; private set; } // Raises PropertyChanged
}

위의 어플리케이션을 시작한다면 여러분은 4가지 경우의 이상현상을 목격하게된다.
첫째, label 은 항상 result를 보여준다. 심지어 button 을 클릭하기 전에도 말이다.
둘째, busy indicator 가 없다. 이러한 indicator 는 operation 이 진행중임을 알려주는
요소이다.
셋째, HTTP 요청이 실패할 경우, exception 이 UI main loop 에 전달되는 점이다. 이는
application 의 fault를 초래한다.
넷째, 사용자가 여러개의 요청을 전송하는 경우, 그에 대한 결과를 구별하지 못한다.
이는 먼저 보낸 요청이 나중의 요청의 결과를 덮어씌울 수 있음을 의미한다.


1. How will the UI display errors?
2. How should the UI look while the operation is in progress?
3. How is the user restricted while the operation is in progress?
4. Does the user have any additional commands available while
the operaiton is in progress?
5. If the user can start multiple operations, how does the UI
provide completion or error details for each one?

Handling Asynchronous Command

대부분의 문제는 result 가 어떻게 핸들되는냐와 관련되어 있다.
정말로 필요한 것은 Task<T> 를 wraping 하는 어떠한 종류의 type 이고
어떠한 data-binding 기능을 제공하는 것이다. 그결과로 application 은 좀더 우아하게
반응할 수 있다. NotifyTaskCompletion<T> 타입은 거의 완벽하게 이를 해결한다.
필자는 이 타입에 하나의 멤버를 추가하려고한다. 이 멤버는 AsyncCommand 로직의
몇몇을 단순화한다. TaskCompletion 속성 이 바로 그것이다. 해당 속성은 opeation 의
종결을 표현하며 exception을 전파하지 않는다. 아래의 코드가 그것이다.

public NotifyTaskCompletion(Task<TResult> task)
{
  Task = task;
  if (!task.IsCompleted)
    TaskCompletion = WatchTaskAsync(task);
}
public Task TaskCompletion { get; private set; }

Figure 7 The Second Attempt at an Asynchronous Command
public class AsyncCommand<TResult> : AsyncCommandBase, INotifyPropertyChanged
{
  private readonly Func<Task<TResult>> _command;
  private NotifyTaskCompletion<TResult> _execution;
  public AsyncCommand(Func<Task<TResult>> command)
  {
    _command = command;
  }
  public override bool CanExecute(object parameter)
  {
    return true;
  }
  public override Task ExecuteAsync(object parameter)
  {
    Execution = new NotifyTaskCompletion<TResult>(_command());
    return Execution.TaskCompletion;
  }
  // Raises PropertyChanged
  public NotifyTaskCompletion<TResult> Execution { get; private set; }
}

AsyncCommand 클래스는 NotifyTaskCompletion 을 사용하여 실제 작업을
표현한다. XAML 은 데이터를 직접 작업 결과와 에러 메시지로 바인딩한다.
