using System;
using System.Collections.Generic;
using System.Text;

namespace BulkDL {
    public static class LogUtil {

        public static void LogRed(dynamic args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(args);
            Console.ResetColor();
        }
        public static void LogWhite(dynamic args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(args);
            Console.ResetColor();
        }

        public static void LogYellow(dynamic args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(args);
            Console.ResetColor();
        }

        public static void LogMagenta(dynamic args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(args);
            Console.ResetColor();
        }

        public static void LogGreen(dynamic args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(args);
            Console.ResetColor();
        }

        public static void LogCyan(dynamic args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(args);
            Console.ResetColor();
        }

    }
}
