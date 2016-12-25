ValueConverters

https://www.wpftutorial.net/ValueConverters.html

상호 화환이 안되는 타입을 가진 두개의 프로퍼티의 데이터를 바인딩할때, 두 프로퍼티
간의 코드가 필요로하다. 이 코드는 source 에서 target 타입으로 값을 정/역 방향으로
변환한다.

이 코드 조각을 ValueConverter 라고 부른다. value converter 는 클래스이며,
IValueConverter 를 구현하는 클래스이다. IValueConverter 는 Convert 와
ConvertBack 메서드를 가진다.

ValueConverter 구현하기

WPF 는 몇가지 Converter 를 제공한다 하지만, 이내 곧 여러분 자신의 converter 가 필요
할 것이다. 이를 위해 클래스를 추가하자 그리고 이를 [SourceType]To[TargetType]converter라고
부르자. value converter 를 이름 짓는 보편적인 방법이다.


예시

public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        // Do the conversion from bool to visibility
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        // Do the conversion from visibility to bool
    }
}

XAML 에서 ValueConverter 사용하기

https://www.wpftutorial.net/ValueConverters.html


첫째, converter 의 namespace 와 XAML namespace 를 맵핑하는 것
둘째, {StaticResource converter}

예시
<Window x:Class="VirtualControlDemo.Window1"
    ...
    xmlns:l="clr-namespace:VirtualControlDemo"
    ...>
    <Window.Resources>
        <l:BoolToVisibilityConverter x:Key="converter" />
    </Window.Resources>
    <Grid>
        <Button Visibility="{Binding HasFunction,
            Converter={StaticResource converter}}" />
    </Grid>
</Window>
