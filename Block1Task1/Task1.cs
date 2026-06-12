public class DeliveryCalculator
{
    private const decimal BASE_PRICE = 10;
    private const decimal DISTANCE_PRICE = 0.5m;
    private const decimal WEIGHT_PRICE = 2m;
    private const decimal FRAGILE_MODIFIER = 1.5m;

    public decimal CalculateDeliveryCost(double distance, double weight, bool isFragile, string deliveryType)
    {
        ValidateInput(distance, weight, deliveryType);
        
        decimal baseCost = CalculateBaseCost(distance, weight);
        decimal modifier = CalculateModifier(isFragile, deliveryType);
        decimal result = baseCost * modifier;
        
        return ApplyProgressiveDiscount(result);
    }

    private void ValidateInput(double distance, double weight, string deliveryType)
    {
        if (distance <= 0)
        {
            throw new ArgumentException("Дистанция должна быть больше нуля");
        }
                    
        if (weight <= 0)
        {
            throw new ArgumentException("Вес должен быть больше нуля");
        }

        if (deliveryType != "Standard" && deliveryType != "Express" && deliveryType != "Overnight")
        {
            throw new ArgumentException($"Неизвестный тип доставки. Допустимые значения: 'Standard', 'Express', 'Overnight'");
        }
    }

    private decimal CalculateBaseCost(double distance, double weight)
    {
        decimal distancePrice = (decimal)(distance * (double)DISTANCE_PRICE);
        decimal weightPrice = (decimal)(weight * (double)WEIGHT_PRICE);
        
        return BASE_PRICE + distancePrice + weightPrice;
    }

    private decimal CalculateModifier(bool isFragile, string deliveryType)
    {
        decimal deliveryTypeModifier = 1;

        switch (deliveryType)
        {
            case "Standard":
                deliveryTypeModifier = 1;
                break;

            case "Express":
                deliveryTypeModifier = 1.5m;
                break;

            case "Overnight":
                deliveryTypeModifier = 2;
                break;

            default:
                deliveryTypeModifier = 1;
                break;
        }

        decimal fragileModifier = 1;
        if (isFragile)
        {
            fragileModifier = 1.5m;
        }

        return deliveryTypeModifier * fragileModifier;
    }

    private decimal ApplyProgressiveDiscount(decimal cost)
    {
        if (cost <= 500) 
            return cost;
        
        if (cost <= 1000) 
            return 500 + (cost - 500) * 0.9m;
        
        if (cost <= 2000) 
            return 500 + 500 * 0.9m + (cost - 1000) * 0.8m;
        
        return 500 + 500 * 0.9m + 1000 * 0.8m + (cost - 2000) * 0.7m;
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
