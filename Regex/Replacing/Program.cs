// LinkedIn Learning Course .NET Programming with C# by Joe Marini
// Example file for Replacing content with Regexes 
using System.Text.RegularExpressions;

string teststr1 = "The quick brown Fox jumps over the lazy Dog";

Regex CapWords = new Regex(@"[A-Z]\w+");

// TODO: Regular expressions can be used to replace content in strings
// in addition to just searching for content
string reslut = CapWords.Replace(teststr1, "*");
Console.WriteLine(teststr1);
Console.WriteLine(reslut);

// TODO: Replacement text can be generated on the fly using MatchEvaluator
// This is a delegate that computes the new value of the replacement
string Makeupper(Match m) {
    string s = m.ToString();
    if (m.Index == 0) {
        return s;
    }
    return s.ToUpper();
}

string upperstr = CapWords.Replace(teststr1, new MatchEvaluator(Makeupper));
Console.WriteLine(teststr1);
Console.WriteLine(upperstr);