namespace MusicApp;

public class Program
{
    public static void Main(string[] args)
    {
        List<Song> songs = new List<Song>();
        StreamReader sr = null;
        try
        {
            sr = new StreamReader(@"E:\szinkák\1.házi\Task1\Input\music.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] parts = line.Split(';');

                string artist = parts[0].Trim();

                parts.ToList().ForEach(item => songs.Add(new Song(artist, item.Trim())));
              
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the file: {ex.Message}");
        }
        finally
        {
            if (sr != null)
            {
                sr.Close();
            }
        }
        songs.ForEach(song => Console.WriteLine(song));
    }
}
