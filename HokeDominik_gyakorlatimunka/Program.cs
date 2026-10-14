using System.Text;

namespace HokeDominik_gyakorlatimunka;

class Program
{
    static void Main(string[] args)
    {
        var data = File.ReadAllLines("games_dataset.csv", new UTF8Encoding()).Skip(1).Select(x => x.Split(";")).Select(x =>
            new Game
            {
                name = x[0],
                type = x[1],
                platform = x[2],
                releaseDate = int.Parse(x[3]),
                score = double.Parse(x[4])
            }).ToList();

        // Listázd ki azokat a játékokat, amelyek értékelése legalább 9 pont!
        data.Where(x => x.score >= 9.0).ToList().ForEach(Console.WriteLine);

        // Add vissza azokat a Nintendo Switch játékokat, amelyek 2015 után jelentek meg és legalább 8-as az értékelésük.
        data.Where(x => x.platform.ToLower().Contains("nintendo") && x.releaseDate > 2015 && x.score >= 8.0).ToList().ForEach(Console.WriteLine);
        
        // Rendezd a játékokat kiadási év szerint csökkenő sorrendbe.
        data.OrderByDescending(x => x.releaseDate).ToList().ForEach(Console.WriteLine);
        
        // Írd ki a 10 legmagasabb értékelésű játék nevét és értékelését.
        data.OrderByDescending(x => x.score).Take(10).ToList().ForEach(Console.WriteLine);
        
        // Csoportosítsd a játékokat platform szerint, és számold meg, hány játék van egy-egy platformon.
        data.GroupBy(x => x.platform).Distinct().Select(x => new
        {
            platform = x.Key,
            has = data.Count(y => y.platform == x.Key)
        }).ToList().ForEach(x => Console.WriteLine($"{x.platform} {x.has}"));
        
        // Melyik játék típusból van a legtöbb? (pl. Sports, Puzzle, stb.)
        Console.WriteLine(data.GroupBy(x => x.type).Select(x => new
        {
            type = x.Key,
            count = data.Count(y => y.type == x.Key)
        }).OrderByDescending(x => x.count).First().type);
        
        // Számold ki, mi a játékok átlagos felhasználói értékelése.
        Console.WriteLine(data.Average(x => x.score));
        
        // Melyik platformon a legmagasabb az átlagos értékelés?
        Console.WriteLine(data.GroupBy(x => x.platform).Select(x => new
        {
            platform = x.Key,
            avgScore = data.Where(y => y.platform == x.Key).Average(y => y.score)
        }).OrderByDescending(x => x.avgScore).First().platform);
        
        // Melyik játék a legmagasabb értékelésű? Melyik a legalacsonyabb?
        var sc = data.OrderByDescending(x => x.score);
        Console.WriteLine($"{sc.First().name} | {sc.Last().name}");
        
        // Sorold fel, milyen különböző játéktípusok találhatók az adathalmazban!
        data.GroupBy(x => x.type).Distinct().Select(x => new
        {
            name = x.Key
        }).ToList().ForEach(x => Console.WriteLine(x.name));

        // Készíts egy listát, amely csak a játék nevét és kiadási évét tartalmazza.
        var lista = data.Select(x => new
        {
            name = x.name,
            releaseDate = x.releaseDate
        }).ToList();
        lista.ForEach(x => Console.WriteLine($"{x.name} | {x.releaseDate}"));
        
        // Listázd az összes „Action” típusú játék nevét, évét és értékelését, értékelés szerint csökkenő sorrendben.
        data.Where(x => x.type.ToLower() == "action").OrderByDescending(x => x.score).ToList().ForEach(x => Console.WriteLine($"{x.name} | {x.releaseDate} | {x.score}"));
        
        // Csoportosítsd a játékokat típus szerint, és számold ki mindegyik típus átlagos értékelését.
        data.GroupBy(x => x.type).Select(x => new
        {
            type = x.Key, 
            avgScore = data.Where(y => y.type == x.Key).Average(y => y.score)
        }).ToList().ForEach(x => Console.WriteLine($"{x.type} | {x.avgScore}"));
        
        // Ird ki a 2005 előtt megjelent játékok nevét és platformját.
        data.Where(x => x.releaseDate < 2005).ToList().ForEach(x => Console.WriteLine($"{x.name} | {x.releaseDate}"));
        
        // Írd ki platformonként a legmagasabb értékelésű játék nevét és értékelését (tipikusan group by + orderby descending + FirstOrDefault() kombináció).
        data.GroupBy(x => x.platform).Select(x => new
        {
            gameName = data.Where(y => y.platform == x.Key).OrderByDescending(y => y.score).First().name,
            score = data.Where(y => y.platform == x.Key).OrderByDescending(y => y.score).First().score
        }).ToList().ForEach(x => Console.WriteLine($"{x.gameName} | {x.score}"));
        
    } 
}