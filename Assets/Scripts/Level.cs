using System;

public class Level {

    private Level() { }

    private int current_count;
    private int destination_score;

    public static Level CreateDefaultLevel() {
        Level level = new Level();
        level.destination_score = 5;
        level.current_count = 0;
        return level;
    }

    public bool IsLeveUp(int score) {
        if (score > destination_score) {
            destination_score += 5;
            current_count++;
            return true;
        } else return false;
    }

    public override string ToString() {
        return "" + current_count;
    }
}