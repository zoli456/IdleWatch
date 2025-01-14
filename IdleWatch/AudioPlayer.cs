using System.Diagnostics;
using NAudio.Wave;

namespace IdleWatch;

internal class AudioPlayer
{
    internal static bool IsPlaying;
    internal static bool Looping;
    internal static MemoryStream AlarmSound = new(File.ReadAllBytes($"{Settings.path}\\Hangok\\alarm.mp3"));
    internal static float Volume;
    internal static Mp3FileReader mp3Reader;
    internal static WaveOutEvent waveOut;

    internal static async Task PlayAlarmLoop()
    {
        if (IsPlaying) return;
        Volume = 1;
        Looping = true;
        IsPlaying = true;
        Debug.WriteLine("IsPlaying!");
        while (Looping)
        {
            AlarmSound.Position = 0;
            mp3Reader = new Mp3FileReader(AlarmSound);
            waveOut = new WaveOutEvent();
            waveOut.Volume = (float)((decimal)Volume / 100);
            waveOut.Init(mp3Reader);
            waveOut.Play();
            while (waveOut.PlaybackState == PlaybackState.Playing && IsPlaying)
            {
                if (Volume < Settings.Hang_section["Hangerő"].IntValue)
                {
                    Volume += .1f;
                    waveOut.Volume = Volume / 100;
                    Debug.WriteLine(Volume);
                }

                await Task.Delay(25);
            }

            waveOut.Stop();
            waveOut.Dispose();
            await mp3Reader.DisposeAsync();
        }

        IsPlaying = false;
    }

    internal static void StopAlarmLoop()
    {
        IsPlaying = false;
        Looping = false;
    }

    internal static async Task PlayAlarmSingle()
    {
        Debug.WriteLine("IsPlaying!");
        AlarmSound.Position = 0;
        mp3Reader = new Mp3FileReader(AlarmSound);
        waveOut = new WaveOutEvent();
        waveOut.Volume = (float)((decimal)Settings.Hang_section["Hangerő"].IntValue / 100);
        waveOut.Init(mp3Reader);
        waveOut.Play();
        while (waveOut.PlaybackState == PlaybackState.Playing)
            await Task.Delay(1);
        waveOut.Stop();
        waveOut.Dispose();
        await mp3Reader.DisposeAsync();
    }
}