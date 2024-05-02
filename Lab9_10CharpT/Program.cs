using System.Collections;

Console.WriteLine("Lab 9 (variant 7)");
Console.Write("Enter the number of task (1 - 4): ");
int choice = Int32.Parse(Console.ReadLine());
switch (choice){
    case 1:{
        Task1 lab8task1 = new Task1();
        lab8task1.Run();
    }break;
    case 2:{
        Task2 lab8task2 = new Task2();
        lab8task2.Run();
    }break;
    case 3:{
        Task3_1 lab8task3_1 = new Task3_1();
        lab8task3_1.Run();
        Task3_2 lab8task3_2 = new Task3_2();
        lab8task3_2.Run();
    }break;
    case 4:{
        Task4 lab8task4 = new Task4();
        lab8task4.Run();
    }break;
}
class Task1{
    public void Run(){
        string postfixExpression = "10 2 + 8 4 - *";
        string prefixExpression = ConvertToPrefixExpression(postfixExpression);
        Console.WriteLine("Prefix expression: " + prefixExpression);
    }

    public static string ConvertToPrefixExpression(string expression){
            Stack<string> stack = new Stack<string>();
            string[] tokens = expression.Split(' ');

            foreach (string token in tokens){
                // Якщо токен є операндом (число), поміщаємо його у стек
                if (IsOperand(token)){
                    stack.Push(token);
                }
                // Якщо токен є оператором (знак), видаляємо два верхніх операнди зі стеку, об'єднуємо їх з оператором і поміщаємо результат у стек
                else if (IsOperator(token)){
                    string operand2 = stack.Pop();
                    string operand1 = stack.Pop();
                    string newExpression = token + " " + operand1 + " " + operand2;
                    stack.Push(newExpression);
                }
            }

            // Результатом є останній елемент у стеку
            return stack.Peek();
        }

        public static bool IsOperand(string token){
            return !IsOperator(token);
        }

        public static bool IsOperator(string token){
            return token == "+" || token == "-" || token == "*" || token == "/";
        }
}

class Task2{
    class Worker{
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        public override string ToString(){
            return $"{LastName}, {FirstName}, {MiddleName}, {Gender}, {Age}, {Salary}";
        }
    }

    public void Run(){
        List<Worker> workers = new List<Worker>();
        string file = "C:\\Users\\User\\github-classroom\\csharplab9-KaliuzhnaMariia\\Lab9_10CharpT\\workers.txt";
        using (StreamReader sr = new StreamReader(file)){
            while (!sr.EndOfStream){
                string[] data = sr.ReadLine().Split(',');
                if (data.Length == 6){
                    Worker work = new Worker{
                        LastName = data[0],
                        FirstName = data[1],
                        MiddleName = data[2],
                        Gender = data[3],
                        Age = int.Parse(data[4]),
                        Salary = double.Parse(data[5])
                    };
                    workers.Add(work);
                }
                else{
                    Console.WriteLine($"Incorrect data format for the worker: {string.Join(",", data)}");
                }
            }
        }

        Queue<Worker> lowSalaryWorkers = new Queue<Worker>();
        Queue<Worker> highSalaryWorkers = new Queue<Worker>();
        foreach (Worker work in workers){
            if (work.Salary < 10000)
                lowSalaryWorkers.Enqueue(work);
            else
                highSalaryWorkers.Enqueue(work);
        }

        Console.WriteLine("Workers with low salary (< 10000):");
        while (lowSalaryWorkers.Count > 0){
            Console.WriteLine(lowSalaryWorkers.Dequeue());
        }

        Console.WriteLine("\nEmployees with high salary ()>= 10000):");
        while (highSalaryWorkers.Count > 0){
            Console.WriteLine(highSalaryWorkers.Dequeue());
        }
    }
}

class Task3_1{
    public void Run(){
        string postfixExpression = "10 2 + 8 4 - *";
        string prefixExpression = ConvertToPrefixExpression(postfixExpression);
        Console.WriteLine("Prefix expression: " + prefixExpression);
    }

    public static string ConvertToPrefixExpression(string expression){
        ArrayList stack = new ArrayList();
        string[] tokens = expression.Split(' ');

        foreach (string token in tokens){
            if (IsOperand(token)){
                stack.Add(token);
            }
            else if (IsOperator(token)){
                string operand2 = (string)stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                string operand1 = (string)stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                string newExpression = token + " " + operand1 + " " + operand2;
                stack.Add(newExpression);
            }
        }

        return (string)stack[stack.Count - 1];
    }

    public static bool IsOperand(string token){
        return !IsOperator(token);
    }

    public static bool IsOperator(string token){
        return token == "+" || token == "-" || token == "*" || token == "/";
    }
}

class Task3_2{
    class Worker : IEnumerable, IComparable, ICloneable{
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        public override string ToString(){
            return $"{LastName}, {FirstName}, {MiddleName}, {Gender}, {Age}, {Salary}";
        }

        public int CompareTo(object obj){
            if (obj == null) return 1;
            Worker otherWork = obj as Worker;
            if (otherWork != null)
                return this.LastName.CompareTo(otherWork.LastName);
            else
                throw new ArgumentException("Object is not an Worker");
        }

        public object Clone(){
            return new Worker{
                LastName = this.LastName,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                Gender = this.Gender,
                Age = this.Age,
                Salary = this.Salary
            };
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public void Run(){
        ArrayList workers = new ArrayList();
        string file = "C:\\Users\\User\\github-classroom\\csharplab9-KaliuzhnaMariia\\Lab9_10CharpT\\workers.txt";
        using (System.IO.StreamReader sr = new System.IO.StreamReader(file)){
            while (!sr.EndOfStream){
                string[] data = sr.ReadLine().Split(',');
                if (data.Length == 6){
                    Worker work = new Worker{
                        LastName = data[0],
                        FirstName = data[1],
                        MiddleName = data[2],
                        Gender = data[3],
                        Age = int.Parse(data[4]),
                        Salary = double.Parse(data[5])
                    };
                    workers.Add(work);
                }
                else{
                    Console.WriteLine($"Incorrect data format for the worker: {string.Join(",", data)}");
                }
            }
        }
        workers.Sort();
        Queue lowSalaryWorkers = new Queue();
        Queue highSalaryWorkers = new Queue();
        foreach (Worker work in workers){
            if (work.Salary < 10000)
                lowSalaryWorkers.Enqueue(work.Clone());
            else
                highSalaryWorkers.Enqueue(work.Clone());
        }

        Console.WriteLine("Workers with low salary (< 10000):");
        while (lowSalaryWorkers.Count > 0){
            Console.WriteLine(lowSalaryWorkers.Dequeue());
        }

        Console.WriteLine("\nWorkers with high salary (>= 10000):");
        while (highSalaryWorkers.Count > 0){
            Console.WriteLine(highSalaryWorkers.Dequeue());
        }
    }     
}

class Task4{
    class Song{
    public string Title { get; set; }
    public string Artist { get; set; }

    public Song(string title, string artist){
        Title = title;
        Artist = artist;
    }

    public override string ToString(){
        return $"{Title} by {Artist}";
    }

    public override bool Equals(object obj){
        if (obj == null || GetType() != obj.GetType()){
            return false;
        }

        Song other = (Song)obj;
        return Title == other.Title && Artist == other.Artist;
    }

    public override int GetHashCode(){
        return Title.GetHashCode() ^ Artist.GetHashCode();
    }
}

    class MusicDisc : IEnumerable<Song>{
    public string Title { get; set; }
    private List<Song> songs = new List<Song>();

    public MusicDisc(string title){
            Title = title;
    }

    public void AddSong(Song song){
        songs.Add(song);
    }

    public void RemoveSong(Song song){
        songs.Remove(song);
    }

    public void Display(){
        Console.WriteLine($"Music disc: {Title}");
        foreach (Song song in songs){
            Console.WriteLine(song);
        }
    }

    public IEnumerator<Song> GetEnumerator(){
        return songs.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator(){
        return GetEnumerator();
    }
}

    class MusicCatalog{
    private Hashtable discs = new Hashtable();

    public void AddDisc(string title){
        discs[title] = new MusicDisc(title);
    }

    public void RemoveDisc(string title){
        discs.Remove(title);
    }

    public void AddSongToDisc(string discTitle, Song song){
        if (discs.ContainsKey(discTitle)){
            ((MusicDisc)discs[discTitle]).AddSong(song);
        }
        else{
            Console.WriteLine("Disc not found");
        }
    }

    public void RemoveSongFromDisc(string discTitle, Song song){
        if (discs.ContainsKey(discTitle)){
            ((MusicDisc)discs[discTitle]).RemoveSong(song);
        }
        else{
            Console.WriteLine("Disc not found");
        }
    }

    public void DisplayCatalog(){
        foreach (MusicDisc disc in discs.Values){
            disc.Display();
        }
    }

    public void SearchByArtist(string artist){
        Console.WriteLine($"Result for '{artist}': ");
        foreach (MusicDisc disc in discs.Values){
            foreach (Song song in disc){
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase)){
                    Console.WriteLine($"Disc: {disc.Title}, \tSong: {song.Title}");
                }
            }
        }
    }
}
    public void Run(){
        MusicCatalog catalog = new MusicCatalog();

        catalog.AddDisc("Hits of 90s");
        catalog.AddDisc("Best Rock");

        catalog.AddSongToDisc("Hits of 90s", new Song("Iris", "Goo Goo Dolls"));
        catalog.AddSongToDisc("Hits of 90s", new Song("Smells Like Teen Spirit", "Nirvana"));
        catalog.AddSongToDisc("Best Rock", new Song("Come as You Are", "Nirvana"));
        catalog.AddSongToDisc("Best Rock", new Song("In Bloom", "Nirvana"));

        catalog.DisplayCatalog();

        Console.WriteLine("\n");
        catalog.SearchByArtist("Nirvana");

        Console.WriteLine("\n");
        catalog.SearchByArtist("Goo Goo Dolls");

        Console.WriteLine("\nRemove song:");
        catalog.RemoveSongFromDisc("Best Rock", new Song("Come as You Are", "Nirvana"));
        catalog.DisplayCatalog();

        catalog.RemoveDisc("Best Rock");

        Console.WriteLine("\nRemove catalog: ");
        catalog.DisplayCatalog();
    }
}
