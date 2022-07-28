namespace touhou_data_watcher.Game; 

public class TouhouData {
    private int gameDifficulty;
    private int gameStage;
    private int gameScore;

    internal TouhouData(int gameDifficulty = 0, int gameStage = 0, int gameScore = 0) {
        this.gameDifficulty = gameDifficulty;
        this.gameStage = gameStage;
        this.gameScore = gameScore;
    }

    internal void ResetCachedData() {
        gameDifficulty = 0;
        gameStage = 0;
        gameScore = 0;
    }
    
    internal int GetGameDifficulty() {
        return gameDifficulty;
    }

    internal void SetGameDifficulty(int value) {
        gameDifficulty = value;
    }

    internal int GetGameStage() {
        return gameStage;
    }

    internal void SetGameStage(int value) {
        gameStage = value;
    }

    internal int GetGameScore() {
        return gameScore;
    }

    internal void SetGameScore(int value) {
        gameScore = value;
    }
}