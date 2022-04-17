using System.Text.Json;
using Microsoft.Win32;

namespace AdminTools.Agent;

public class JokeService
{
    private static string _profileListPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList";
    private static string _profileGuidPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileGUID";

    private static string _hostname = Environment.MachineName;

    // Programming jokes borrowed from:
    // https://github.com/eklavyadev/karljoke/blob/main/source/jokes.json
    private readonly HashSet<Joke> _jokes = new()
    {
        new Joke("What's the best thing about a Boolean?", "Even if you're wrong, you're only off by a bit."),
        new Joke("What's the object-oriented way to become wealthy?", "Inheritance"),
        new Joke("Why did the programmer quit their job?", "Because they didn't get arrays."),
        new Joke("Why do programmers always mix up Halloween and Christmas?", "Because Oct 31 == Dec 25"),
        new Joke("How many programmers does it take to change a lightbulb?", "None that's a hardware problem"),
        new Joke(
            "If you put a million monkeys at a million keyboards, one of them will eventually write a Java program",
            "the rest of them will write Perl"),
        new Joke("['hip', 'hip']", "(hip hip array)"),
        new Joke("To understand what recursion is...", "You must first understand what recursion is"),
        new Joke("There are 10 types of people in this world...", "Those who understand binary and those who don't"),
        new Joke("Which song would an exception sing?", "Can't catch me - Avicii"),
        new Joke("Why do Java programmers wear glasses?", "Because they don't C#"),
        new Joke("How do you check if a webpage is HTML5?", "Try it out on Internet Explorer"),
        new Joke("A user interface is like a joke.", "If you have to explain it then it is not that good."),
        new Joke("I was gonna tell you a joke about UDP...", "...but you might not get it."),
        new Joke("The punchline often arrives before the set-up.", "Do you know the problem with UDP jokes?"),
        new Joke("Why do C# and Java developers keep breaking their keyboards?",
            "Because they use a strongly typed language."),
        new Joke("Knock-knock.", "A race condition. Who is there?"),
        new Joke("What's the best part about TCP jokes?", "I get to keep telling them until you get them."),
        new Joke("A programmer puts two glasses on their bedside table before going to sleep.",
            "A full one, in case they gets thirsty, and an empty one, in case they don’t."),
        new Joke("There are 10 kinds of people in this world.",
            "Those who understand binary, those who don't, and those who weren't expecting a base 3 joke."),
        new Joke("What did the router say to the doctor?", "It hurts when IP."),
        new Joke("An IPv6 packet is walking out of the house.", "He goes nowhere."),
        new Joke("3 SQL statements walk into a NoSQL bar. Soon, they walk out", "They couldn't find a table.")
    };

    private string _profGuid = "";
    private string _profList = "";

    private string? _rjoke;
    private string _username = "";

    public string GetJoke()
    {
        var joke = _jokes.ElementAt(
            Random.Shared.Next(_jokes.Count));
        return $"{joke.Setup}{Environment.NewLine}{joke.Punchline}";
    }

    public void CreateJson()
    {
        var rkpl = Registry.LocalMachine.OpenSubKey(_profileListPath);
        var rkpg = Registry.LocalMachine.OpenSubKey(_profileGuidPath);

        var jsonfile = "profile-" + _hostname + ".json";

        List<Profile> prList;
        List<string> proList;
        List<string> proGuid;
        prList = new List<Profile>();
        proList = new List<string>();
        proGuid = new List<string>();

        Regis.GetProfileList(rkpl, proList);
        Regis.GetProfileGuid(rkpg, proGuid);

        foreach (var vpg in proGuid)
        {
            var pGuid = vpg;
            _profGuid = pGuid;
        }

        foreach (var vpl in proList)
        {
            var pList = vpl;
            _profList = pList;
        }

        var prof = new Profile
        {
            RmtPc = _hostname,
            User = "null",
            ProfileList = _profList,
            ProfileGuid = _profGuid
        };

        prList.Add(prof);

        var json = JsonSerializer.Serialize(prList);

        File.WriteAllText(jsonfile, json);
    }

    private record Joke(string Setup, string Punchline);
}