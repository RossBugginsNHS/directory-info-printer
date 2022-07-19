// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string dirStr = "/mnt/c/h";
string outputfile="output.txt";
if (args.Length == 1)
{
    dirStr = args[0];
}

var dir = new DirectoryInfo(dirStr);
await Findfiles(dir);

async Task Findfiles(DirectoryInfo directory)
{
    var oF = new FileInfo(outputfile);
    if(oF.Exists)
        oF.Delete();

    var infos = directory.GetFiles("*", SearchOption.AllDirectories);
    Console.WriteLine($"Got {infos.Length} files in {directory.FullName}");
    var longers = new List<FileInfo>();
    var renameFailed = new List<FileInfo>();
    foreach (var info in infos)
    {
        await PrintInfo(info);
    }


    Console.WriteLine("Finished");
}

async Task PrintInfo(FileInfo fileInfo)
{
    var withoutPrepend = fileInfo.DirectoryName.Replace(dirStr, "");
    Console.WriteLine($"{withoutPrepend}\t{fileInfo.Name}");
    await File.AppendAllTextAsync(outputfile, $"{withoutPrepend}\t{fileInfo.Name}\n");
}