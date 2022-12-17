using UnityEngine;

class RoadValueGenerator
{
    private int _countOfMultipliedBlocks = 0;
    private int _residualValue = 0;

    public RoadValueGenerator()
    {
        _countOfMultipliedBlocks = 0;
        _residualValue = 0;
    }

    internal void GetValues(out int value, out int decreasedValue, out int multiplier)
    {
        value = Random.Range(10, 50);
        decreasedValue = (int)(value * Random.Range(0.5f, 0.9f));

        if (Random.Range(0, 2) == 1 && _countOfMultipliedBlocks < 2 && _residualValue <= 50)
        {
            multiplier = CountMultiplier(_residualValue);
            _countOfMultipliedBlocks += 1;
            _residualValue += value * multiplier - decreasedValue;
        }
        else
        {
            multiplier = 0;
            _residualValue += value - decreasedValue;
        }
    }

    private int CountMultiplier(int value)
    {
        int multiplier = 5;

        for (int i = 5; i > 0; i--)
            if (multiplier * value >= 100)
                multiplier--;
        return multiplier;
    }
}
