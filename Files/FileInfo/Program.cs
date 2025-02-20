// Application Programming .NET Programming with C# by Abdullahi Tijjani
// Working with file information


// Make sure the example file exists
const string filename = "TestFile.txt";
// 1: WriteAllText overwrites a file with the given content
if (!File.Exists(filename)) {
File.WriteAllText(filename, "This is a text file. ");
}
// 3: AppendAllText adds text to an existing file
File.AppendAllText(filename, "This is Application programming. ");
// 4: A FileStream can be opened and written to until closed
using (StreamWriter sw = File.AppendText(filename)) {
sw.WriteLine("Aisha");
sw.WriteLine("Nabila");
sw.WriteLine("Ishaq");
}
// 2: ReadAllText reads all the content from a file
string content;
content = File.ReadAllText(filename);
Console.WriteLine(content);
// TODO: Get some information about the file


// TODO: We can also get general information using a FileInfo 


// TODO: File information can also be manipulated

