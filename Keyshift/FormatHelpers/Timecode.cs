using System;

namespace Keyshift.FormatHelpers
{
    public static class Timecode
    {
        /// <summary>
        /// Formats a frame number into a Timecode string.
        /// </summary>
        /// <param name="position">The frame's number</param>
        /// <param name="fps">The base FPS</param>
        /// <returns>A Timecode string formatted as: HH:mm:ss.ff</returns>
        public static string FramesToTimecode(int position, float fps = 30)
        {
            int roundFps = (int)Math.Round(fps);
            int frm, second, minute, hour;
            frm = position % roundFps;
            second = (int)Math.Floor((double)position / roundFps);
            minute = (int)Math.Floor((double)second / 60);
            hour = (int)Math.Floor((double)minute / 60);
            return $"{hour:D2}:{minute:D2}:{second:D2}.{frm:D2}";

        }
    }
}