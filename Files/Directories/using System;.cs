using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== Office File Organizer =====");

        // Get user details
        Console.Write("Enter your name: ");
        string studentName = Console.ReadLine();

        Console.Write("Enter your student ID: ");
        string studentId = Console.ReadLine();

        // Get directory path
        Console.Write("Enter the directory path to organize: ");
        string directoryPath = Console.ReadLine();

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Error: The directory does not exist.");
            return;
        }

        Console.WriteLine("Organizing files...");

        // Organize files and get summary
        Dictionary<string, List<FileDetails>> fileSummary = OrganizeFiles(directoryPath);

        // Generate summary report
        string summaryPath = GenerateSummaryReport(directoryPath, fileSummary, studentName, studentId);

        Console.WriteLine($"Summary report created: {summaryPath}");
    }

    static Dictionary<string, List<FileDetails>> OrganizeFiles(string directoryPath)
    {
        // Define subdirectories
        string wordDir = Path.Combine(directoryPath, "WordFiles");
        string excelDir = Path.Combine(directoryPath, "ExcelFiles");
        string pptDir = Path.Combine(directoryPath, "PPTFiles");

        // Create subdirectories if they don't exist
        Directory.CreateDirectory(wordDir);
        Directory.CreateDirectory(excelDir);
        Directory.CreateDirectory(pptDir);

        // File categories
        Dictionary<string, string> fileCategories = new Dictionary<string, string>
        {
            { ".docx", wordDir },
            { ".xlsx", excelDir },
            { ".pptx", pptDir }
        };

        Dictionary<string, List<FileDetails>> fileSummary = new Dictionary<string, List<FileDetails>>
        {
            { "WordFiles", new List<FileDetails>() },
            { "ExcelFiles", new List<FileDetails>() },
            { "PPTFiles", new List<FileDetails>() }
        };

        // Process each file
        foreach (string file in Directory.GetFiles(directoryPath))
        {
            string extension = Path.GetExtension(file);
            if (fileCategories.ContainsKey(extension))
            {
                string fileName = Path.GetFileName(file);
                string destinationDir = fileCategories[extension];
                string newFilePath = Path.Combine(destinationDir, fileName);

                // Move the file
                File.Move(file, newFilePath);
                Console.WriteLine($"Moved: {fileName} → {Path.GetFileName(destinationDir)}");

                // Get file details
                FileInfo fileInfo = new FileInfo(newFilePath);
                FileDetails details = new FileDetails
                {
                    Name = fileName,
                    Type = extension switch
                    {
                        ".docx" => "Word",
                        ".xlsx" => "Excel",
                        ".pptx" => "PowerPoint",
                        _ => "Unknown"
                    },
                    Size = fileInfo.Length / 1024, // Convert bytes to KB
                    CreationDate = fileInfo.CreationTime.ToString("yyyy-MM-dd")
                };

                // Add to summary
                fileSummary[Path.GetFileName(destinationDir)].Add(details);
            }
        }

        return fileSummary;
    }

    static string GenerateSummaryReport(string directoryPath, Dictionary<string, List<FileDetails>> fileSummary, string studentName, string studentId)
    {
        string summaryPath = Path.Combine(directoryPath, "SummaryReport.txt");

        using (StreamWriter writer = new StreamWriter(summaryPath))
        {
            writer.WriteLine($"Student: {studentName} (ID: {studentId})");
            writer.WriteLine("Organized Files:");
            
            foreach (var category in fileSummary)
            {
                foreach (var file in category.Value)
                {
                    writer.WriteLine($"{file.Name} ({file.Type}, {file.Size} KB, Created: {file.CreationDate})");
                }
            }
        }

        return summaryPath;
    }
}

class FileDetails
{
    public string Name { get; set; }
    public string Type { get; set; }
    public long Size { get; set; } // In KB
    public string CreationDate{get;set;}
}
