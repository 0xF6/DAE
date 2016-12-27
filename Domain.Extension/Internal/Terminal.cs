using System.Linq;

namespace Domain.Extension.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Wrap the management console terminal
    /// </summary>
    internal static class Terminal
    {
        public const string Key = "§";
        public static string Cost(char c) => $"§{c}";
        public static string Cost(this string c) => $"§{c}";
        private static TextWriter Out => Console.Out;
        private static TextReader In => Console.In;
        static Terminal()
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
            }
            catch
            {
            }
            listOfRCL = new List<string>
            {
                "§0", // Black
                "§1", // DarkBlue
                "§2", // DarkGreen
                "§3", // DarkCyan
                "§4", // DarkRed
                "§5", // DarkMagenta
                "§6", // DarkYellow
                "§7", // DarkGray
                "§8", // Gray
                "§9", // Blue
                "§a", // Green
                "§b", // Cyan
                "§c", // Red
                "§d", // Magenta
                "§e", // Yellow
                "§f" // White
            };
        }
        public const string Black = "§0";
        public const string DarkBlue = "§1";
        public const string DarkGreen = "§2";
        public const string DarkCyan = "§3";
        public const string DarkRed = "§4";
        public const string DarkMagenta = "§5";
        public const string DarkYellow = "§6";
        public const string DarkGray = "§7";
        public const string Gray = "§8";
        public const string Blue = "§9";
        public const string Green = "§a";
        public const string Cyan = "§b";
        public const string Red = "§c";
        public const string Magenta = "§d";
        public const string Yellow = "§e";
        public const string White = "§f";
        public static readonly List<string> listOfRCL;
        private static readonly string defHeader = "§cEngine§c";
        private static string header = "";
        public static bool isUseRCL = true;
        public static bool isUseColor = true;
        /// <summary>
        /// Writes a new line, with the support of rcl, without symbol of the end-line
        /// </summary>
        /// <param name="s">String Rcl</param>
        public static void Write(string s, bool isUseHeader = false)
        {
            if (isUseHeader)
            {
                if (header == "")
                {
                    Out.Write("<");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Out.Write(defHeader.SReplaceRcl());
                    Console.ForegroundColor = ConsoleColor.White;
                    Out.Write(">: ");
                }
                if (header != "" && isUseRCL)
                {
                    Out.Write("<");
                    ParseAndWrite(header);
                    Out.Write(">: ");
                }
            }
            if (isUseRCL)
                ParseAndWrite(s);
            else
                Out.Write(s);
        }
        /// <summary>
        /// Writes a new line, with the support of rcl
        /// </summary>
        /// <param name="s">String Rcl</param>
        public static void WriteLine(string s, bool isUseHeader = false)
        {
            if (isUseHeader)
            {
                if (header == "")
                {
                    Out.Write("<");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Out.Write(defHeader.SReplaceRcl());
                    Console.ForegroundColor = ConsoleColor.White;
                    Out.Write(">: ");
                }
                if (header != "" && isUseRCL)
                {
                    Out.Write($"<");
                    ParseAndWrite(header);
                    Out.Write($">: ");
                }
            }
            if (isUseRCL)
                ParseAndWrite($"{s}{Environment.NewLine}");
            else
                Out.Write($"{s}{Environment.NewLine}");
        }
        private static string getHTime()
            => $"[{RCL.Wrap(DateTime.Now.ToString("yyyy-MM-dd HH':'mm':'ss"), ConsoleColor.Gray)}]";
        private static void ParseAndWrite(string str)
        {
            lock (Out)
            {
                str = listOfRCL.Aggregate(str, (current, y) => current.Replace(y, $"+{y}\0"));
                char[] chars = str.ToCharArray();
                for (int i = 0; i != chars.Length; i++)
                {
                    if (i > chars.Length - 1)
                        break;
                    if (chars[i] == '+')
                    {
                        if (chars.Length > i + 1)
                        {
                            if (chars[i + 1] == '§')
                            {
                                if (chars.Length > i + 2)
                                {
                                    if (chars[i + 2] == '0')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '1')
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '2')
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '3')
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '4')
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '5')
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '6')
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '7')
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '8')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == '9')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == 'a')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == 'b')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == 'c')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == 'd')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == 'e')
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        i = i + 3;
                                    }
                                    else if (chars[i + 2] == 'f')
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        i = i + 3;
                                    }
                                    else
                                        Out.Write(chars[i]);
                                }
                                else
                                    Out.Write(chars[i]);
                            }
                            else
                                Out.Write(chars[i]);
                        }
                        else
                            Out.Write(chars[i]);
                    }
                    else
                        Out.Write(chars[i]);
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static string SReplaceRcl(this string s, string to = "") => listOfRCL.Aggregate(s, (current, y) => current.Replace(y, to));
        private static void VReplaceRcl(this string s, string to = "") => s = listOfRCL.Aggregate(s, (current, y) => current.Replace(y, to));
    }
}
