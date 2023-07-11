public static class TypeFruit
{
    public enum FruitType
    {
        Apple,
        Avocado,
        Bannana,
        Cherries,
        Lemon,
        Peach
    }
    public static string TypeToString(FruitType fruitType)
    {
        if(fruitType == FruitType.Apple)
        {
            return "Apples";
        }
        else if(fruitType == FruitType.Avocado)
        {
            return "Avocados";
        }
        else if(fruitType == FruitType.Bannana)
        {
            return "Bannanas";
        }
        else if(fruitType == FruitType.Cherries)
        {
            return "Cherries";
        }
        else if(fruitType == FruitType.Lemon)
        {
            return "Lemons";
        }
        else
        {
            return "Peaches";
        }
    }
}
