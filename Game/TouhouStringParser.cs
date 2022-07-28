namespace touhou_data_watcher.Game; 

public static class TouhouStringParser {
    internal static string ParseDifficultyName(int difficultyValue) {
        string diff;
        
        switch (difficultyValue) {
            case 0: {
                diff = "Easy";
                break;
            }
            case 1: {
                diff = "Normal";
                break;
            }
            case 2: {
                diff = "Hard";
                break;
            }
            case 3: {
                diff = "Lunatic";
                break;
            }
            case 4: {
                diff = "Extra";
                break;
            }
            default: {
                diff = "Unknown";
                break;
            }
        }

        return diff.ToUpper();
    }
}