public class DeliveryCalculator 
{
    public decimal CalculateDeliveryCost(double distance, double weight, bool isFragile, string deliveryType)
    {
        decimal result = 0;
        decimal basePrice = 10;
        decimal distancePrice = (decimal)(distance * 0.5);
        decimal weightPrice = (decimal)(weight * 2);
        decimal fragileModifyer = 1;
        decimal deliveryTypeModifyer = 1;

        if (distance <= 0) throw new ArgumentException("Дистанция должна быть больше нуля");
        if (weight <= 0) throw new ArgumentException("Вес должен быть больше нуля");
        switch (deliveryType)
        {
            case "Standard": deliveryTypeModifyer = 1; break;
            case "Express": deliveryTypeModifyer = 1.5m; break;
            case "Overnight": deliveryTypeModifyer = 2; break;
            default:
                throw new ArgumentException($"Неизвестный тип доставки. Допустимые значения: 'Standard', 'Express', 'Overnight'");
        }

        if (isFragile) fragileModifyer = 1.5m;

        result = (basePrice + distancePrice + weightPrice) * fragileModifyer * deliveryTypeModifyer;

        if (result <= 500) return result;
        if (result <= 1000) return 500 + (result - 500) * 0.9m;
        if (result <= 2000) return 500 + 500 * 0.9m + (result - 1000) * 0.8m;
        return 500 + 500 * 0.9m + 1000 * 0.8m + (result - 2000) * 0.7m;
    }
}

class Task1
{
    static void Main()
    {
        var calculator = new DeliveryCalculator();

        try
        {
            decimal cost1 = calculator.CalculateDeliveryCost(1000, 50, false, "Standard");
            Console.WriteLine($"Стоимость доставки (Standard): {cost1} у.е.");

            decimal cost2 = calculator.CalculateDeliveryCost(800, 30, true, "Express");
            Console.WriteLine($"Стоимость доставки (Express, хрупкий): {cost2} у.е.");

            decimal cost3 = calculator.CalculateDeliveryCost(1500, 100, false, "Overnight");
            Console.WriteLine($"Стоимость доставки (Overnight): {cost3} у.е.");

            decimal cost4 = calculator.CalculateDeliveryCost(2000, 150, true, "Overnight");
            Console.WriteLine($"Стоимость доставки (Overnight, хрупкий): {cost4} у.е.");

            decimal cost5 = calculator.CalculateDeliveryCost(-5, 10, false, "Standard");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
