namespace Domain.Extension.Internal
{
    using System;

    internal static class RCL
    {
        public static string Wrap(string text, ConsoleColor color)
            => $"{color.getValue()}{text}{ConsoleColor.White.getValue()}";

        internal static string getValue(this ConsoleColor c)
        {
            switch (c)
            {
                case ConsoleColor.Black:
                    return Terminal.Black;
                case ConsoleColor.DarkBlue:
                    return Terminal.DarkBlue;
                case ConsoleColor.DarkGreen:
                    return Terminal.DarkGreen;
                case ConsoleColor.DarkCyan:
                    return Terminal.DarkCyan;
                case ConsoleColor.DarkRed:
                    return Terminal.DarkRed;
                case ConsoleColor.DarkMagenta:
                    return Terminal.DarkMagenta;
                case ConsoleColor.DarkYellow:
                    return Terminal.DarkYellow;
                case ConsoleColor.Gray:
                    return Terminal.Gray;
                case ConsoleColor.DarkGray:
                    return Terminal.DarkGray;
                case ConsoleColor.Blue:
                    return Terminal.Blue;
                case ConsoleColor.Green:
                    return Terminal.Green;
                case ConsoleColor.Cyan:
                    return Terminal.Cyan;
                case ConsoleColor.Red:
                    return Terminal.Red;
                case ConsoleColor.Magenta:
                    return Terminal.Magenta;
                case ConsoleColor.Yellow:
                    return Terminal.Yellow;
                case ConsoleColor.White:
                    return Terminal.White;
                default:
                    throw new ArgumentOutOfRangeException(nameof(c), c, null);
            }
        }
    }
}