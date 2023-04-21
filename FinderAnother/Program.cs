
CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(5000);
CancellationToken cancellationToken = cancellationTokenSource.Token;
ParallelOptions parallelOptions = new ParallelOptions(){MaxDegreeOfParallelism = -1};
List<DirectoryInfo> directoryInfos = new List<DirectoryInfo>() { new DirectoryInfo("C:\\Users\\Осипян Максим") };
await Parallel.ForEachAsync(directoryInfos, parallelOptions, async (dir, cancellationToken) => { await FinderFile(dir);});
async Task FinderFile(DirectoryInfo directory)
{
    List<DirectoryInfo> dirs = directory.GetDirectories().ToList();
    List<FileInfo> files = directory.GetFiles().ToList();
    if (dirs.Count == 0 && files.Count == 0) return;
    files.Where(file => file.Name.Contains(".txt")).ToList().ForEach(file => Console.WriteLine(file.FullName));
    await Parallel.ForEachAsync(dirs, parallelOptions, async (dir, cancellationToken) => { await FinderFile(dir); });
}