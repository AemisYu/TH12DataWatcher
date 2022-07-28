namespace touhou_data_watcher;

using System.Diagnostics;
using System.Runtime.InteropServices;
using Game;
using Util;

public class TouhouDataWatcher {
    private const int VersionMajor = 0;
    private const int VersionMinor = 0;
    private const int VersionRev = 1;
    
    private static string UFO_NAME = "th12";
    
    private IntPtr _ufoHandle;
    private bool _ufoProcessDetected;

    private TouhouData _touhouData;
    
    [DllImport("kernel32.dll")]
    static extern bool ReadProcessMemory(int hProcess, int lpBaseAddr, byte[] byteBuffer, int dwSize, ref int lpBytesRead);
    [DllImport("kernel32.dll")]
    static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    static void Main() {
        Console.CursorVisible = false;
        
        new TouhouDataWatcher();
    }

    private TouhouDataWatcher() {
        _touhouData = new TouhouData();
        
        startDisplay();
        
        new Thread(DetectGame).Start();
    }

    private void startDisplay() {
        
        Console.WriteLine("Touhou 12 Data Watcher - Version " + VersionMajor + "." + VersionMinor + "." + VersionRev);
        Console.WriteLine("This code is available on GitHub: https://github.com/veir1/TH12DataWatcher");
        Console.WriteLine("(c) 2022  dowemy");
        Console.WriteLine("This program is licensed under GNU GPL v3.0 and comes with ABSOLUTELY NO WARRANTY; for details see the LICENSE file.\n");
        Console.WriteLine("!! WARNING !! This software may trigger some anti-cheat systems, use it a your own risk!!\n");
    }
    
    private void DetectGame() {
        while (true) {
            if (!_ufoProcessDetected) {
                _touhouData.ResetCachedData();
                Console.SetCursorPosition(0, 7);
                Console.Write("Waiting for th12...");
                
                Process[] touhouGameProcesses = Process.GetProcessesByName(UFO_NAME);
                
                if (touhouGameProcesses.Length > 0) {
                    _ufoHandle = OpenProcess(0x0010, false, touhouGameProcesses.FirstOrDefault()!.Id);
                    _ufoProcessDetected = true;
                }
            } else {
                int bytes = 0;
                byte[] byteBuffer = new byte[4]; // Read first 4 bytes of the stack
                
                bool readLevel = ReadProcessMemory((int)_ufoHandle, GamePointer.FromPointerAddress(GamePointerAddress.Difficulty), byteBuffer, 2, ref bytes);
                if (readLevel) _touhouData.SetGameDifficulty(BitConverter.ToInt16(byteBuffer, 0));
                else _ufoProcessDetected = false;

                bool readStage = ReadProcessMemory((int)_ufoHandle, GamePointer.FromPointerAddress(GamePointerAddress.Stage), byteBuffer, 2, ref bytes);
                if (readStage) _touhouData.SetGameStage(BitConverter.ToInt16(byteBuffer, 0));
                else _ufoProcessDetected = false;
                
                bool readScore = ReadProcessMemory((int)_ufoHandle, GamePointer.FromPointerAddress(GamePointerAddress.Score), byteBuffer, 4, ref bytes); 
                if (readScore) _touhouData.SetGameScore(BitConverter.ToInt32(byteBuffer, 0) * 10);
                else _ufoProcessDetected = false;

                Console.SetCursorPosition(0, 7);
                Console.Write("Game Difficulty: " + TouhouStringParser.ParseDifficultyName(_touhouData.GetGameDifficulty()) + StringUtils.BlankField);
                Console.SetCursorPosition(0, 8);
                Console.Write("Game Stage: " + _touhouData.GetGameStage() + StringUtils.BlankField);
                Console.SetCursorPosition(0, 9);
                Console.Write("Game Score: " + _touhouData.GetGameScore() + StringUtils.BlankField);
            }
            
            Thread.Sleep(100); // Let the cpu rest
        }
    }
}