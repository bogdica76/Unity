using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaymakGames_Server
{
    class Text
    {
        public static void WriteInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[INFO]" + text);
            Console.ResetColor();
        }
        public static void WriteDebug(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[Debug]" + text);
            Console.ResetColor();
        }
        public static void WriteLog(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[Log]" + text);
            Console.ResetColor();
        }
    }
}
