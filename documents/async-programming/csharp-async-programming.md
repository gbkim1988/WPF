Async Improves Responsiveness

https://msdn.microsoft.com/ko-kr/library/mt674882.aspx


The async method in .NET Framework 4.5 and Windows Runtime

------------------------------------------------------------------------
| Application area         | supporting APIs that cotain async methods  |
-------------------------------------------------------------------------
| Web Access               | HttpClient, SyndicationClient              |
-------------------------------------------------------------------------
| Working with files       | StorageFile, StreamWriter, StreamReader,   |
|                          | XmlReader                                  |
-------------------------------------------------------------------------
| Working with images      | MediaCapture, BitmapEncoder, BitmapDecoder |
-------------------------------------------------------------------------
| WCF Programming          } Synchronous and Asynchronous Operations    |
-------------------------------------------------------------------------

Async Methods Are Easier to Write

asyn 와 await 키워드는 async programming 의 핵심이다. 이러한 키워드를 이용하여
.NET Framework나 Windows Runtime 내의 자원을 사용할 수 있으며 이는 동기 메서드와
유사한 수준의 난이도를 가진다. 비동적인 메서드는 async 메서드로 나타낸다.

Task 는 Async method 의 Return Type 이 int 인 경우 Task<int> 로 표현한다.

이것은 비동적인 메서드에 사용되는 표현인 것으로 생각되고 내적인 이유가 있을 것으로
생각된다.

논리적인 흐름에 따라 Async Method 가 지원되는 .NET Framework 또는 Windows Runtime
메서드를 사용해야 할 것으로 생각된다. 그리고 각 API 의 비동기 메서드를 호출하여
호출을 할 시에 await 키워드를 사용하여 실제로 지연이 발생하는 영역을 구별하여
디버깅 시에 장점을 살릴 수 있다.

앞서 비동기 메서드를 호출한 뒤에 await 키워드르로 응답을 비동기적으로 처리한다고 언급
하였는데 대표적인 예로 HttpClient 의 API 목록 및 설명을 언급하고자 한다.

--------------------------------------------------------------------------
|   HttpClient Async Method  |           Description                     |
--------------------------------------------------------------------------
|   GetStringAsync           |
--------------------------------------------------------------------------
|   PostAsync                | Send a POST request with a cancellation   |
|                            | token as an asynchronous operation        |
--------------------------------------------------------------------------
|   이하 중략                 | 이하 중략                                 |
--------------------------------------------------------------------------

다음의 특징은 원문의 example 에서 async method 가 가지는 특징이다.

 - async 지시자를 포함하는 method
 - async 메서드의 이름은 접두사로 Async 를 붙인다.
 - 다음의 타입들 중에 하나를 반환 타입으로 지정한다.
   -> Task<TResult> => TResult 라는 타입의 피연산자는 return 을 통해 반환하는 경우
   -> Task => return 을 가지지 않거나, return 하되 피연산자는 존재하지 않는 경우
   -> void => async event handelr 작성 시 사용
 - 메서드는 적어도 하나의 await 을 가진다. await 키워드는 async 메서드가 더이상
   계속될 수 없는 지점을 표시하며 await 표현이 마킹하는 비동기 동작이 완료될 때 까지
   해당 지점에 머무른다. 그동안 method 는 중지되고 제어권은 async 메서드의 호출자에게
   돌아간다.

async 메서드에서 키워드와 타입을 제공하여 무엇을 원하는지를 지정해야한다. 컴파일러는
나머지 작업을 모두 처리해준다. 제어권이 await 지점에 복귀되었을때, 무엇이 발생해야하는
지를 추적하는 것 또한 컴파일러의 작업에 포함된다. loops 그리고 exception handler 와 같은
처리는 전통적인 비동기 코드에서 처리가 힘든 작업이다. async 메서드에서는 동기 작업과
동일하게 이를 처리할 수 있다.

원문의 example 에서 async 메서드의 동작을 정리

1. event handelr 가 호출되고 AccessTheWebAsync async 메서드를 await 한다.
2. AccessTheWebAsync 는 HttpClient 인스턴스를 생성하고 GetStringAsync 메서드를
호출하여 웹사이트의 문자열을 다운로드한다.
3. GetStringAsync 메서드는 제어을 호출자에 반환한다.
4.
