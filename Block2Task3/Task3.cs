public class SmartContainer
{
    public string Id { get; }
    public double VolumeCapacity { get; }
    public double CurrentVolume { get; private set; }
    public enum Statuses
    {
        Empty,
        PartiallyFilled,
        Full
    }

    public Statuses Status
    {
        get
        {
            if (CurrentVolume == 0)
                return Statuses.Empty;
            if (CurrentVolume == VolumeCapacity)
                return Statuses.Full;
            else
                return Statuses.PartiallyFilled;
        }
    }

    public SmartContainer(string id, double volumeCapacity)
    {
        Id = id;
        VolumeCapacity = volumeCapacity;
        CurrentVolume = 0;
    }

    public void LoadCargo(double volume)
    {
        if (CurrentVolume + volume > VolumeCapacity)
            throw new InvalidOperationException($"Нельзя загрузить объем больше, чем объём контейнера: {VolumeCapacity}м³");

        CurrentVolume += volume;
    }

    public void UnloadCargo(double volume)
    {
        if (volume > CurrentVolume)
            throw new InvalidOperationException($"Нельзя выгрузить больше, чем есть в контейнере: {CurrentVolume}м³");

        CurrentVolume -= volume;
    }
}

class Task3
{
    static void Main()
    {
        try
        {
            var container = new SmartContainer("ABC-123", 100);

            Console.WriteLine($"Контейнер {container.Id} создан");
            Console.WriteLine($"Вместимость: {container.VolumeCapacity}м³");
            Console.WriteLine($"Статус: {container.Status}\n");

            container.LoadCargo(30);
            Console.WriteLine($"Загружено 30м³. Текущий объём: {container.CurrentVolume}м³");
            Console.WriteLine($"Статус: {container.Status}\n");

            container.LoadCargo(70);
            Console.WriteLine($"Загружено 70м³. Текущий объём: {container.CurrentVolume}м³");
            Console.WriteLine($"Статус: {container.Status}\n");

            container.UnloadCargo(100);
            Console.WriteLine($"Разгружено 100м³. Текущий объём: {container.CurrentVolume}м³");
            Console.WriteLine($"Статус: {container.Status}\n");

            container.LoadCargo(999);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}