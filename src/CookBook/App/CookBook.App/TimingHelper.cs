using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CookBook.App;

public class TimingHelper
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();
        private static Action<string>? logAction;

        public static void Start(Action<string>? logCall = null)
        {
            logAction ??= logCall;
            stopwatch.Restart();
        }

        public static void Stop()
        {
            stopwatch.Stop();
        }

        public static void Log(string? message = null, [CallerMemberName] string? from = null, [CallerFilePath] string? fromFile = null)
        {
            logAction?.Invoke($"~LOG~ Elapsed: {stopwatch.ElapsedMilliseconds}ms. - ({from}) | [{fromFile.Split('\\').Last()}] - Msg: {message}");
        }
    }