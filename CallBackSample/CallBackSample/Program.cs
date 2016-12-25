using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CallBackSample
{
    public delegate bool CallBack(int hwnd, int lParam);

    class Program
    {
        [DllImport("user32")]
        public static extern int EnumWindows(CallBack x, int y);

        static void Main(string[] args)
        {
            CallBack myCallBack = new CallBack(CallBackSample.Program.Report);
            EnumWindows(myCallBack, 0);
            System.Console.Read();
            return;
        }

        public static bool Report(int hwnd, int lParam)
        {
            Console.Write("Window handle is ");
            Console.WriteLine(hwnd);
            return true;
        }
    }
}
