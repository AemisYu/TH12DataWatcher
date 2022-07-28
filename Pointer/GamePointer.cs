namespace touhou_data_watcher;

public class GamePointer {
    public static int FromPointerAddress(GamePointerAddress gamePointerAddress) => (int) gamePointerAddress;
}

public enum GamePointerAddress {
    Character = 0x004B0C90,
    SubCharacter = 0x004B0C94,
    Difficulty = 0x004B0CA8,
    Stage = 0x004B0CB0,
    StageFrames = 0x004B0CBC,
    GameState = 0x004B0CB8,
    GameStateFrames = 0x004B0CC0,
    MenuPointer = 0x004B4530, // sub menu selection at 0x34, in sub menu at 0xB4
    EnemyState = 0x004B43B8, // boss flag at 0x1594
    BgmStage = 0x004D3658, // currently playing outside of stage, or stage theme in-stage
    BgmBoss = 0x004D3758, // boss theme
    PracticeFlag = 0x004B0CE0, // 16 when practicing, 0 otherwise
    ReplayFlag = 0x004CE8B0, // 2 when watching replay, 1 otherwise
    Life = 0x004B0C98,
    Bomb = 0x004B0CA0,
    Score = 0x004B0C44,
    GameOver = 0x004B0CC4,
}