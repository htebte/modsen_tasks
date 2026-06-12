public class CapacityCalculator
{
    public int GetRequiredContainers(int[] boxWeights, int maxCapacity)
    {
        int result = 1;
        int currentWeight = 0;

        if (boxWeights == null || boxWeights.Length == 0)
        {
            return 0;
        }

        foreach (int weight in boxWeights)
        {
            if (weight > maxCapacity)
            {
                throw new ArgumentException("Вес коробки превышает максимальную ёмкость контейнера");
            }
        }

        for (int i = 0; i < boxWeights.Length; i++) {
            if (currentWeight + boxWeights[i] <= maxCapacity)
            {
                currentWeight += boxWeights[i];
            }
            else
            {
                result++;
                currentWeight = boxWeights[i];
            }
        }

        return result;
    }
}

class Task2
{
    static void Main()
    {
        var calculator = new CapacityCalculator();

        try
        {
            int test1 = calculator.GetRequiredContainers([3, 2, 5, 1, 4], 6);
            Console.WriteLine($"Тест 1: {test1}");

            int test2 = calculator.GetRequiredContainers([1, 1, 1, 1], 2);
            Console.WriteLine($"Тест 2: {test2}");

            int test3 = calculator.GetRequiredContainers([10, 5, 3], 8);
            Console.WriteLine($"Тест 3: {test3}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
